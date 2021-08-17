using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldPlayer : MonoBehaviour
{

    public MapPoint currentPoint;

    public OverworldUIController overworldUIController;

    public float moveSpeed;

    private bool blockInput;

    private MapPoint[] mapPoints;


    // Start is called before the first frame update
    void Start()
    {
        blockInput = false;

        mapPoints = FindObjectsOfType<MapPoint>();

        if ( PlayerPrefs.HasKey("CurrentLevel") )
        {
            string curLevelName = PlayerPrefs.GetString("CurrentLevel");
            foreach(MapPoint mp in mapPoints){
                if (curLevelName.Equals(mp.levelName))
                {
                    transform.position = mp.transform.position;
                    currentPoint = mp;
                    break;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        // Gradually move the player sprite towards the MapPoint of the desired input (if not null)
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        // only allow inputs if we're at a MapPoint
        if (Vector3.Distance(transform.position, currentPoint.transform.position) == 0f)
        {
            blockInput = false;
        }

        if (!blockInput) {
            if (Input.GetAxisRaw("Horizontal") > .5f)
            {
                if (currentPoint.Right != null)
                {
                    AudioManager.instance.playSfx(5);
                    setCurrentPoint(currentPoint.Right);
                    blockInput = true;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if (currentPoint.Left != null)
                {
                    AudioManager.instance.playSfx(5);
                    setCurrentPoint(currentPoint.Left);
                    blockInput = true;
                }
            }
            else if (Input.GetAxisRaw("Vertical") > .5f)
            {
                if (currentPoint.Up != null)
                {
                    AudioManager.instance.playSfx(5);
                    setCurrentPoint(currentPoint.Up);
                    blockInput = true;
                }
            }
            else if (Input.GetAxisRaw("Vertical") < -.5f)
            {
                if (currentPoint.Down != null)
                {
                    AudioManager.instance.playSfx(5);
                    setCurrentPoint(currentPoint.Down);
                    blockInput = true;
                }
            }
            else if (currentPoint.isLevel && currentPoint.levelName != "" )
            {
                overworldUIController.DisplayLevelInfo(currentPoint);

                if (Input.GetButtonDown("Jump") && ! currentPoint.isLocked)
                {
                    AudioManager.instance.playSfx(4);
                    OverWorldLevelManager.instance.LoadLevel();
                }
            }
        }

        
    }

    private void setCurrentPoint(MapPoint newPoint)
    {
        currentPoint = newPoint;
        overworldUIController.HideLevelInfo();
    }
}
