using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{

    public static CheckPointController instance;

    private CheckPoint[] checkPoints;

    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();

        //initialize spawn point to wherever player starts level from
        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deactivateCheckpoints()
    {
        foreach (CheckPoint cp in checkPoints)
        {
            cp.resetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
