using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint Up, Down, Left, Right;
    public bool isLevel, isLocked;
    public string levelName, previousLevel;
    public string levelDescr = "LOCKED";

    public float bestTime;

    public int gemCount, maxGems;


    public GameObject badgeGem;
    

    // Start is called before the first frame update
    void Start()
    {
        // since after completion of a level we load the overworld, this is the only place we 
        // need to put logic for unlocking subsequent levels
        if (isLevel)
        {
            if (levelName == previousLevel)
            {
                isLocked = false;
            }
            else
            {
                if (levelName != "")
                {
                    if (previousLevel != "")
                    {
                        if (PlayerPrefs.GetInt(previousLevel) == 1)
                        {
                            isLocked = false;
                        }
                        else
                        {
                            isLocked = true;
                        }

                    }
                }
                else
                {
                    isLocked = true;
                }
            }
        }

        if (PlayerPrefs.HasKey(levelName+"_gems") && PlayerPrefs.HasKey(levelName + "_levelTime"))
        {
            bestTime = PlayerPrefs.GetFloat(levelName + "_levelTime");
            gemCount = PlayerPrefs.GetInt(levelName + "_gems");

            if (gemCount == maxGems)
            {
                badgeGem.SetActive(true);
            } else
            {
                badgeGem.SetActive(false);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
