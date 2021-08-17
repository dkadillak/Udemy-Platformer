using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBossController : MonoBehaviour
{

    public enum bossStates {shooting, moving, hurt, ded };
    public bossStates currentState;
    public float mineResetTime;

    public Transform tankBoss;
    public SpriteRenderer tankRenderer;
    public Animator tankAnimator;
    public GameObject escapePlatforms;


    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool movingRight;
    public GameObject mine;
    public float timeBetweenMines;
    private float mineTimer;
    public Transform mineSpawnPoint;
    private bool activateMines;

    [Header("Shooting")]
    public GameObject bullet;
    public float fireRate;
    private float fireRateCounter;
    public Transform firePoint;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;


    [Header("Health")]
    public int health;
    public GameObject explosion;
    public float mineSpeedUp, shotSpeedUp;

    // Start is called before the first frame update
    void Start()
    {
        activateMines = false;
        movingRight = false;
        currentState = bossStates.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:

                fireRateCounter -= Time.deltaTime;

                if(fireRateCounter <= 0)
                {
                    fireRateCounter = fireRate;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.playSfx(2);
                    newBullet.transform.localScale = tankBoss.transform.localScale;
                }
                break;

            case bossStates.moving:

                if (activateMines)
                {
                    mineTimer -= Time.deltaTime;

                    if (mineTimer <= 0)
                    {
                        mineTimer = timeBetweenMines;
                        Instantiate(mine, mineSpawnPoint.position, mineSpawnPoint.rotation);
                    }
                }

                if (movingRight)
                {
                    tankBoss.position = Vector3.MoveTowards(tankBoss.position, rightPoint.position, moveSpeed * Time.deltaTime);
                    if (Vector3.Distance(tankBoss.position, rightPoint.position) < .1)
                    {
                        tankBoss.localScale = new Vector3(1f, 1f, 1f);
                        EndMovement();
                    }
                }
                else
                {
                    tankBoss.position = Vector3.MoveTowards(tankBoss.position, leftPoint.position, moveSpeed * Time.deltaTime);
                    if (Vector3.Distance(tankBoss.position, leftPoint.position) < .1)
                    {
                        tankBoss.localScale = new Vector3(-1f, 1f, 1f);
                        EndMovement();
                    }
                }

                break;

            case bossStates.hurt:


                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;
                    if(hurtCounter <= 0)
                    {
                        currentState = bossStates.moving;
                        mineTimer = timeBetweenMines;

                        timeBetweenMines /= mineSpeedUp;
                        fireRate /= shotSpeedUp;

                    }
                }

                break;
        }

# if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.H))
        {
            takeDamage();
        }
#endif

    }

    /*public void takeDamage()
    {
        StartCoroutine(takeDamageCo());
    }*/



    public void takeDamage() {

        AudioManager.instance.playSfx(0);
        health--;

        
        if (health <= 0)
        {
            escapePlatforms.SetActive(true);
            currentState = bossStates.ded;
            StartCoroutine(destroyMinesThenDie());
        } else 
        {
            if(health == 3)
            {
                activateMines = true;
            }
            currentState = bossStates.hurt;
            hurtCounter = hurtTime;
            tankAnimator.SetTrigger("Hit");
            StartCoroutine(destroyMines());
        }
    }

    private IEnumerator destroyMines()
    {
        // always clean up the mines
        TankBossMineController[] mines = FindObjectsOfType<TankBossMineController>();
        foreach (TankBossMineController mine in mines)
        {
            mine.triggerMine();
            yield return new WaitForSeconds(mineResetTime);

        }
    }
    private IEnumerator destroyMinesThenDie()
    {
        // always clean up the mines
        TankBossMineController[] mines = FindObjectsOfType<TankBossMineController>();
        foreach (TankBossMineController mine in mines)
        {
            Instantiate(explosion, tankBoss.position, tankBoss.rotation);
            mine.triggerMine();
            yield return new WaitForSeconds(mineResetTime);

        }
        Instantiate(explosion, tankBoss.position, tankBoss.rotation);
        AudioManager.instance.playSfx(3);
        Destroy(gameObject);
    }



    private void EndMovement()
    {
        currentState = bossStates.shooting;
        movingRight = !movingRight;
        fireRateCounter = fireRate;
        tankAnimator.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }
}
