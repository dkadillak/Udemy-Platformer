using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWorldLevelManager : MonoBehaviour
{
    public static OverWorldLevelManager instance;
    public OverWorldPlayer player;
    public OverworldUIController UIController;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    private IEnumerator LoadLevelCo()
    {
        UIController.FadeToBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(player.currentPoint.levelName);
        UIController.FadeToWhite();
    }
}
