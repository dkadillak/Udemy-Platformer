using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int currentHealth, maximumHealth;

    public float invincibleLength;
    private float invincibleCounter;

    public SpriteRenderer spriteRenderer;

    public GameObject deathEffect;

    // for instantiating singleton instance
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            
            if (invincibleCounter <= 0)
            {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
        
    }

    public void doDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth -= 1;

            // player ded :(
            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Instantiate(deathEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                // play hurt sfx
                AudioManager.instance.playSfx(9);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, .5f);
                PlayerController.instance.knockBack();
            }

            UIController.instance.updateHealthUI();
        }
    }


    // return true if player needed health, false otherwise. Used to determine if we should keep health pickup active or not
    public void Heal()
    {
            currentHealth += 1;
            UIController.instance.updateHealthUI();
    }
}
