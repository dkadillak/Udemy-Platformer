using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public Sprite checkPointOn, checkPointOff;

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
            //turn off all other checkpoints
            CheckPointController.instance.deactivateCheckpoints();

            spriteRenderer.sprite = checkPointOn;

            CheckPointController.instance.SetSpawnPoint(transform.position);
        } 
    }

    public void resetCheckpoint()
    {
        spriteRenderer.sprite = checkPointOff;
    }
}
