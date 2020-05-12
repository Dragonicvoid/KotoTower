using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAudio : MonoBehaviour
{
    bool playSound;
    AudioSource audioSource;

    // initialization
    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // stop main menu song
    public void stop()
    {
        audioSource.Stop();
    }

    // start main menu song
    public void start()
    {
        audioSource.Stop();
        audioSource.Play();
    }

    // check if is playing
    public bool isNowPlaying()
    {
        return audioSource.isPlaying;
    }
}
