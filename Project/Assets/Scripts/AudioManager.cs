using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sounds;

    public AudioSource mainLevelMusic, endLevelMusic, bossMusic;

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

    public void playSfx(int soundToPlay)
    {
        // stop the sound if it's already playing (useful for multiple enemy kills in a row, unity says this sound is already playing so i'm not gonna play it
        sounds[soundToPlay].Stop();
        sounds[soundToPlay].Play();
    }

    public void playEndLevel()
    {
        mainLevelMusic.Stop();
        endLevelMusic.Play();
    }

    public void playBossMusic()
    {
        mainLevelMusic.Stop();
        bossMusic.Play();
    }

    public void stopBossMusic()
    {
        bossMusic.Stop();
        mainLevelMusic.Play();
    }
}
