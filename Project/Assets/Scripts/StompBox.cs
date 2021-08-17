using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{

    public GameObject deathEffect;

    public GameObject collectable;

    [Range(0,100)] public float dropChance;

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
        if (other.tag == "Enemy")
        {
            other.transform.parent.gameObject.SetActive(false);

            Instantiate(deathEffect, other.transform.position, other.transform.rotation);


            // decide whether enemy should drop health
            float shouldDrop = Random.Range(0, 100f);

            if (shouldDrop <= dropChance)
            {
                Instantiate(collectable, other.transform.position, other.transform.rotation);
            }

            // player should bounce off enemy when he bops it
            PlayerController.instance.Bounce();

            // play enemy killed sfx
            AudioManager.instance.playSfx(3);
        }
        
    }
}
