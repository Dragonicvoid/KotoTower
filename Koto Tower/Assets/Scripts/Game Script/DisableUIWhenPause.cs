using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableUIWhenPause : MonoBehaviour
{
    Button currButton;

    // start
    private void Start()
    {
        currButton = this.GetComponent<Button>();
    }

    // if the game is paused, disable the UI
    void Update()
    {
        if (GameManager.instance.isPaused)
            currButton.interactable = false;
        else
            currButton.interactable = true;
    }
}
