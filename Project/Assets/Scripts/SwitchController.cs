using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject objToToggle;

    public Sprite downSprite;

    private SpriteRenderer spriteRenderer;

    private bool switchActivated;

    public bool activateObject;


    // Start is called before the first frame update
    void Start()
    {
        switchActivated = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !switchActivated)
        {
            AudioManager.instance.playSfx(9);

            spriteRenderer.sprite = downSprite;

            if (!activateObject)
            {
                objToToggle.SetActive(false);
            } else
            {
                objToToggle.SetActive(true); 
            }

            switchActivated = true;
        }
        
    }
}
