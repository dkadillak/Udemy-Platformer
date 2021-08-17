using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //singleton instance
    public static PlayerController instance;

    public float moveSpeed;

    public Rigidbody2D rigidBody;

    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask ground;

    private bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    public float knockBackLength, knockBackForce, bounceForce;

    private float knockBackCounter;

    public bool stopInput, jumped, doubleJumped;

    private int offStageJumpCount;



    //singelton initialization
    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stopInput = false;
        offStageJumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseScreen.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            {
                anim.ResetTrigger("hurt");

                //get movement input from player
                rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigidBody.velocity.y);

                //check if player is touching the "ground" layer or not
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .1f, ground);
                
                if (rigidBody.velocity.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (rigidBody.velocity.x > 0)
                {
                    spriteRenderer.flipX = false;
                }


                if (Input.GetButtonDown("Jump"))
                {

                    if (isGrounded)
                    {
                        canDoubleJump = true;
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                        AudioManager.instance.playSfx(10);
                        jumped = true;
                    }

                    else 
                    {
                        if (canDoubleJump)
                        {
                            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                            canDoubleJump = false;
                            AudioManager.instance.playSfx(10);
                            doubleJumped = true;
                        }

                        else if (!isGrounded  && offStageJumpCount < 2 && !jumped && !doubleJumped)
                        {
                            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                            AudioManager.instance.playSfx(10);
                            offStageJumpCount += 1;
                        }
                    }
                }


                anim.SetBool("isGrounded", isGrounded);
                anim.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
                if( isGrounded ) {
                    offStageJumpCount = 0;
                    jumped = false;
                    doubleJumped = false;
                }

            }
            else
            {
                knockBackCounter -= Time.deltaTime;

                if (spriteRenderer.flipX)
                {
                    rigidBody.velocity = new Vector2(knockBackForce, rigidBody.velocity.y);
                }
                else
                {
                    rigidBody.velocity = new Vector2(-knockBackForce, rigidBody.velocity.y);
                }
            }
        }

        // even when we want to stop input (like end of level), we still want animations to update appropriately while we fade out
        else if (stopInput)
        {
            //check if player is touching the "ground" layer or not
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, ground);
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
        }
    }


    public void knockBack()
    {
        knockBackCounter = knockBackLength;
        rigidBody.velocity = new Vector2(0f, knockBackForce);
        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);
        canDoubleJump = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
       if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
