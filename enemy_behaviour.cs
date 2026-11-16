using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class enemy_behaviour : MonoBehaviour
{
    //NOW THE HARD PART, MAKE IT MORE HUMAN LIKE.
    [Header("AI CALCUATIONS USAGE.")]
    public Vector3[] vectors;
    [SerializeField] private float[] AllDist;
    public Vector3 minVect;
    private int minIndex;
    private Vector2 distanceVect;
    private Vector3 lastRecVect;
    [Header("Enemy characteristics")]
    [SerializeField] private float speed;
    private Transform A;
    [SerializeField] private Collider2D A_Collider;
    public float minDistance, MaxDistance;
    public float rotationTime;
    private AI_HANDLER Myhandler;

    //use last recorded LOCATION OF A *********
    //calculate the best possible rotation angle and speed for a desired speed.
    // use kinematics to add acceleration to the object.
    private void Start()
    {
        Myhandler = transform.parent.GetComponent<AI_HANDLER>();
        A = Myhandler.player;
        A_Collider = Myhandler.A_collider;
    }
    private void FixedUpdate()
    {

        minVect = checkCollisions();
        transform.position += minVect * Time.deltaTime * speed;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, (Mathf.Atan2(minVect.y, minVect.x) * Mathf.Rad2Deg) - 90), rotationTime);
    }
    private Vector3 checkCollisions()
    {//main ai program loop.
     // We want the last Found location to be used instead of always usage. (IMPORTANT NOTE.)
        distanceVect = new Vector2(A.position.x - transform.position.x, A.position.y - transform.position.y);
        for (int i = 1; i < 361; i++)
        {
            float Distance = Mathf.Pow(distanceVect.x, 2) + Mathf.Pow(distanceVect.y, 2);
            Vector2 vector = new Vector2(Myhandler.sin[i], Myhandler.cos[i]);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vector, 100);
            if (hit.collider == A_Collider)
            {
                if (Distance >= minDistance)
                {
                    lastRecVect = A.position;
                    return distanceVect.normalized;
                }

                else
                {//add transform rotation here instead of on the top.
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, (Mathf.Atan2(distanceVect.y, distanceVect.x) * Mathf.Rad2Deg) - 90f);
                    return Vector2.zero;
                }
            }
            if (!hit.collider)
            {
                AllDist[i] = Mathf.Pow(A.position.x - vector.x, 2) + Mathf.Pow(A.position.y - vector.y, 2);
                vectors[i] = vector;
            }
            else
            {//could be a flashpoint.(FINd another impossible value for such.)
                AllDist[i] = 1000000;
            }
        }
        //optimize the below code for best performance.
        AllDist[0] = 100000000;
        float minDis = AllDist.Min();
        for (int i = 0; i < AllDist.Length; i++)
        {
            if (minDis == AllDist[i])
            {
                minIndex = i;
                break;
            }
        }
        return vectors[minIndex];//returning the most viable solution (wothout considering angular length.)
    }
}
