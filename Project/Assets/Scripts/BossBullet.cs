using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-moveSpeed * transform.localScale.x * Time.deltaTime, 0f, 0f);
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.doDamage();
        }

        Destroy(gameObject);
        AudioManager.instance.playSfx(1);
    }
}
