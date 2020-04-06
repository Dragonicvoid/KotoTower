using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayQuestionSound : MonoBehaviour, IPointerClickHandler
{
    AudioSource audioSrc;
    Text questionText;

    // Is the player has listened it once
    bool hasListened;

    // Initialize audio source, has listened and text
    private void Start()
    {
        hasListened = false;
        questionText = this.GetComponent<Text>();
        audioSrc = this.GetComponent<AudioSource>();
    }

    // Change the text when the audio is ended
    private void Update()
    {
        if (!audioSrc.isPlaying && hasListened)
        {
            questionText.text = "Karakter tersebut adalah ?";
            hasListened = false;
        }
    }

    // When click play the sound
    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSrc.clip != null && !audioSrc.isPlaying)
        {
            audioSrc.Play();
            hasListened = true;
        }
    }
}
