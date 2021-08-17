using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Image heart1, heart2, heart3;

    public Sprite heartFull, heartHalf, heartEmpty;

    public Text gemCountText;

    public Image fadeScreen;

    public float fadeSpeed;

    private bool fadeToBlack, fadeToWhite;

    public GameObject endLevelText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update void Start()
    public void Start(){
        gemCountText.text = "0";
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


    public void updateHealthUI()
    {
        switch (PlayerHealthController.instance.currentHealth)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty; 
                heart3.sprite = heartEmpty;
                break;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;


            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    public void updateGemUI()
    {
        gemCountText.text = LevelManager.instance.gemCounter.ToString();
    } 


    public void FadeToBlack()
    {
        fadeToBlack = true;
        fadeToWhite = false;
    }

    public void FadeToWhite()
    {
        fadeToBlack = false;
        fadeToWhite = true;
    }

    public void toggleEndLevelText()
    {
        endLevelText.SetActive(true);
    }


} 