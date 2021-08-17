using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public int waitToRespawn;

    public int gemCounter;

    public float levelTime;

    public string nextLevelName;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gemCounter = 0;
        levelTime = 0f;
        PlayerController.instance.stopInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        levelTime += Time.deltaTime; 
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

   
    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
       
        // play death sound
        AudioManager.instance.playSfx(8);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(waitToRespawn);

        UIController.instance.FadeToWhite();

        PlayerController.instance.transform.position = CheckPointController.instance.spawnPoint;

        PlayerController.instance.gameObject.SetActive(true);

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maximumHealth;
        UIController.instance.updateHealthUI();

    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    private IEnumerator EndLevelCo()
    {

        //play end level music!
        AudioManager.instance.playEndLevel();

        // stop player movements
        PlayerController.instance.stopInput = true;

        // stop camera movements
        CameraController.instance.stopTracking = true;

        // set active end level text
        UIController.instance.toggleEndLevelText();

        //
        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(3.5f);

        // used for unlocking subsequent level in overworld
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);

        // record gems collected for level
        int prevGemCount = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems", 0);

        // check if we've broken our previous pr for gems collected in level 
        if (prevGemCount < gemCounter || prevGemCount == 0)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemCounter);
        }

        // record time for level
        float prevLevelTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_levelTime", float.MaxValue);

        // check if we've broken our previous pr for level time completion
        if (levelTime < prevLevelTime)
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_levelTime", levelTime);
        }

        // record level we just completed, useful when loading bacck to overworld
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        // load next level
        SceneManager.LoadScene(nextLevelName);

    }

}
