using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCamera : MonoBehaviour
{
    public Vector2 minPos, maxPos;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate is called after the update loops, useful to not get jittery effect updating camera and player position both in update loop
    void LateUpdate()
    {
        float clampedX = Mathf.Clamp(target.transform.position.x, minPos.x, maxPos.x);
        float clampedY = Mathf.Clamp(target.transform.position.y, minPos.y, maxPos.y);

        transform.position = new Vector3(clampedX, clampedY, -10);
        
    }
}
