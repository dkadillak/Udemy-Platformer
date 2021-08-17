using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int index;

    public Transform platform;

    // Start is called before the first frame update
    void Start()
    {
        index = 0; 
        foreach(Transform point in points)
        {
            point.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
       platform.position = Vector3.MoveTowards(platform.position, points[index].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(platform.position, points[index].position) == 0f)
        {
            if (index == points.Length - 1)
            {
                index = 0;
            }
            else
            {
                index += 1;
            }
        }

        
    }
}
