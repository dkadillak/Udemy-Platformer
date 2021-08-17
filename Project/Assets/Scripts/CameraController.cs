using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public Transform target;

    public Transform farBackground;

    public Transform middleBackground;

    private float lastXposition, lastYposition;
    public float minCamHeight, maxCamHeight;

    public bool stopTracking;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastXposition = transform.position.x;
        lastYposition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopTracking)
        {
            // keep camera centered on the player using the player's x position
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minCamHeight, maxCamHeight), transform.position.z);


            // using this to control how much to move background layers by
            float amountToMoveXBy = transform.position.x - lastXposition;
            float amountToMoveYBy = transform.position.y - lastYposition;


            farBackground.position += new Vector3(amountToMoveXBy, amountToMoveYBy * .75f, 0f);

            middleBackground.position += new Vector3(amountToMoveXBy * .5f, -amountToMoveYBy * .025f, 0f);

            lastXposition = transform.position.x;
            lastYposition = transform.position.y;
        }
    }
}
