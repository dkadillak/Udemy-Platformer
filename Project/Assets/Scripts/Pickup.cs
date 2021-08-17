using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public bool isGem, isHealth;

    public GameObject pickupEffect;

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
        if (other.CompareTag("Player"))
        {
            if ( isGem )
            {

                LevelManager.instance.gemCounter += 1;
                UIController.instance.updateGemUI();
                Destroy(gameObject);
                Instantiate(pickupEffect, transform.position, transform.rotation);
                AudioManager.instance.playSfx(6);
            }

            else if ( isHealth )
            {
                if ( PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maximumHealth )
                {
                    PlayerHealthController.instance.Heal();
                    Destroy(gameObject);
                    Instantiate(pickupEffect, transform.position, transform.rotation);
                    AudioManager.instance.playSfx(7);
                }
            }

        }
        
    }
}
