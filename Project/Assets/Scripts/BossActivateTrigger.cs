using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivateTrigger : MonoBehaviour
{
    public GameObject boss;

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
            AudioManager.instance.playBossMusic();
            boss.SetActive(true);
            gameObject.SetActive(false);
        } 
    }
}
