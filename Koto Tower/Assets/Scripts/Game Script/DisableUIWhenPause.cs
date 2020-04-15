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

    // if the game is paused, disable the UI, with exception
    void Update()
    {
        if (GameManager.instance.isPaused && !"Boosted Button".Equals(this.gameObject.name))
            currButton.interactable = false;
        else if (!"Boosted Button".Equals(this.gameObject.name))
            currButton.interactable = true;
    }
}
