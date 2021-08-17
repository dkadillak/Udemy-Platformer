using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("in on trigger enter 2D this.tag = "+this.tag+" other.tag = "+other.tag);

        if (other.CompareTag("Player"))
        {
            //inefficient way of doing things
            //FindObjectOfType<PlayerHealthController>().doDamage();

            PlayerHealthController.instance.doDamage();
        
        }
        
    }
}
