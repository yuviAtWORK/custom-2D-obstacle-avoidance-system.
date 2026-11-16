# custom-2D-obstacle-avoidance-system.
Made a custom 2D obstacle avoidance system in unity C# without A* algorithm. 
shooting ray casts directly from the source object to all the possible directions in x and z axis.
We then record the hit rays and take determine one possible solution by the rays which were not hit.
Sin and Cos are cached by every degree so, faster runtime.
