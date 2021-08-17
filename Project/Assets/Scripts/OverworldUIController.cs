using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUIController : MonoBehaviour
{


    public Image fadeScreen;

    public float fadeSpeed;

    private bool fadeToBlack, fadeToWhite;

    public GameObject levelInfoPanel, lockedLevelInfoPanel;

    public Text levelInfoText, gemCountText, bestTimeText;

    // Start is called before the first frame update
    void Start()
    {
        FadeToWhite();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, (fadeSpeed * Time.deltaTime)));
            
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        else if (fadeToWhite)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, (fadeSpeed * Time.deltaTime)));
            
            if (fadeScreen.color.a == 0f)
            {
                fadeToWhite = false;
            }
        }

    }

    public void FadeToBlack()
    {
        fadeToBlack = true;
        fadeToWhite = false;
    }
    public void FadeToWhite()
    {
        fadeToBlack = false;

        fadeToWhite =  true;   
    }

    public void DisplayLevelInfo(MapPoint mp)
    {
        if (!mp.isLocked)
        {
            levelInfoText.text = mp.levelName + " : " + mp.levelDescr;
            bestTimeText.text =  mp.bestTime == 0 ? "BEST: ---s" : "BEST: " + mp.bestTime.ToString("F3")+"s";

            gemCountText.text = "FOUND: " + mp.gemCount;

            levelInfoPanel.SetActive(true);
            lockedLevelInfoPanel.SetActive(false);
        } else
        {
            levelInfoPanel.SetActive(false);
            lockedLevelInfoPanel.SetActive(true);
        }

    }

    public void HideLevelInfo()
    {
        levelInfoPanel.SetActive(false);
        lockedLevelInfoPanel.SetActive(false);
    }

}
