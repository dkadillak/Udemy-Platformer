using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlammerTrigger : MonoBehaviour
{

    public SlammerController slammer;

    private bool canSlam, isSlamming;

    // Start is called before the first frame update
    void Start()
    {
        canSlam = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if( isSlamming)
        {
            slammer.slam();
            
            if(Vector3.Distance(slammer.transform.position, slammer.slamPosition.position) < .1f)
            {
                isSlamming = false;
            }

        } else {

            slammer.resetPosition();

            if(Vector3.Distance(slammer.transform.position, slammer.neutralPosition.position) < .1f)
            {
                canSlam = true;
            }

        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && canSlam)
        {
            isSlamming = true;
            canSlam = false;
        } 
    }
}
