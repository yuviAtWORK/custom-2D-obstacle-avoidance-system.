using UnityEngine;

public class AI_HANDLER : MonoBehaviour
{
    //calcualtion of sin and cos functions would be done here and would BE SAVED HERE.
    //misc alterations would also be done here. Such as player position, enemy TYPE SPEED, spawning etc.
    // FIND THE AI_HANDLER BY TAGS OR MAKING IT THE PARENT OF ALL ENEMY TYPE GAMEOBJECTS.
    public float[] sin, cos;
    public Transform player;
    public Collider2D A_collider;
    private void Start()//SIN AND COS CALCULATIONS.
    {
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            //Calculate Sin and cos values here.
            for (int i = 1; i < 361; i++)
            {
                sin[i] = Mathf.Sin(i * Mathf.Deg2Rad);
                cos[i] = Mathf.Cos(i * Mathf.Deg2Rad);
                PlayerPrefs.SetFloat("SIN" + i, sin[i]);
                PlayerPrefs.SetFloat("COS" + i, cos[i]);
            }
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.Save();
        }
        else if (PlayerPrefs.GetInt("FirstTime") != 0)
        {
            //get sin and cos values here.
            for (int i = 1; i < 361; i++)
            {
                sin[i] = PlayerPrefs.GetFloat("SIN" + i);
                cos[i] = PlayerPrefs.GetFloat("COS" + i);
            }
        }

    }
}
