using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlammerController : MonoBehaviour
{

    public Transform neutralPosition, slamPosition;

    public float resetSpeed, slamSpeed;

    // Start is called before the first frame update
    void Start()
    {
        neutralPosition.parent = null;
        slamPosition.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void slam()
    {
        transform.position = Vector3.MoveTowards(transform.position, slamPosition.position, slamSpeed * Time.deltaTime);
    }

    public void resetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, neutralPosition.position, resetSpeed * Time.deltaTime);
    }
}
