using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerController : MonoBehaviour
{
    public float bounceForce;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController.instance.rigidBody.velocity = new Vector2(PlayerController.instance.rigidBody.velocity.x, bounceForce);

            animator.SetTrigger("bounce");
            AudioManager.instance.playSfx(10);
        }
        
    }
}
