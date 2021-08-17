using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string startLevel, continueLevel;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(startLevel))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(startLevel);
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(continueLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game :(");
    }
}
