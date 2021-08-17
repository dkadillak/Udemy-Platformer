using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int index;

    private float attackTimer;

    public float attackDistance, timeBetweenAttacks, attackSpeed;

    public SpriteRenderer spriteRenderer;

    private Vector3  snapshotPlayerPosition;


    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        attackTimer = 0f;

        foreach(Transform point in points)
        {
            point.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // not attacking
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > attackDistance || attackTimer > 0)
        {
            snapshotPlayerPosition = Vector3.zero;
            
            transform.position = Vector3.MoveTowards(transform.position, points[index].position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, points[index].position) == 0f)
            {
                if (index == points.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index += 1;
                }
            }

            if (transform.position.x < points[index].position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        } else
        // attacking
        {
            if (snapshotPlayerPosition == Vector3.zero)
            {
                snapshotPlayerPosition = PlayerController.instance.transform.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, snapshotPlayerPosition, attackSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, snapshotPlayerPosition) < .1f)
            {
                attackTimer = timeBetweenAttacks;
                snapshotPlayerPosition = Vector3.zero;
            }
        }
    }
}
