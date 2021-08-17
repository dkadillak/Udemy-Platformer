using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public float moveSpeed;

    public float waitTime, moveTime;

    private float waitCounter, moveCounter;

    //used to decide what two points the enemy should patrol
    public Transform leftPoint, rightPoint;

    public SpriteRenderer spriteRenderer; 

    private bool movingRight;

    private Rigidbody2D rigidBody;

    private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
         /*
         * left and right points are kept as children of enemy for organization
         * if left and right point are kept as children then their positions update relative to where the parent enemy is, which we don't want
         * therefore make left and right point's parent = null at run time
         */
         leftPoint.parent = null;
         rightPoint.parent = null;

        rigidBody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        movingRight = true;
        spriteRenderer.flipX = true;

        moveCounter = moveTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (moveCounter > 0)
        {
            moveCounter -= Time.deltaTime;

            if (movingRight)
            {
                rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);

                if (transform.position.x >= rightPoint.position.x)
                {
                    movingRight = false;
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);

                if (transform.position.x <= leftPoint.position.x)
                {
                    movingRight = true;
                    spriteRenderer.flipX = true;
                }
            }

            if( moveCounter <= 0)
            {
                waitCounter = Random.Range(0.5f * waitTime, 1.5f * waitTime);
//                Debug.Log("waitCounter: " + waitCounter);

            }
            animator.SetBool("isMoving", true);
        }


        else if ( waitCounter > 0 )
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
            waitCounter -= Time.deltaTime;

            if(waitCounter <= 0)
            {
                moveCounter = Random.Range(.3f * moveTime, 1.5f * moveTime);
//                Debug.Log("moveCounter: " + moveCounter);
                
            }
            animator.SetBool("isMoving", false);
        }
        
        
    }
}
