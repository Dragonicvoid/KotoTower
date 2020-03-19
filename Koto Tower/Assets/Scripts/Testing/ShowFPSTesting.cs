using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPSTesting : MonoBehaviour
{
    //size of the font base on division of width resolution
    [SerializeField] int division = 25;
    // Get its own text
    Text text;
    //timer so the fps counter only show FPS every second
    float timer;
    // count of frame
    int count;

    // Get text component from this and start the timer from 0s
    private void Awake()
    {
        text = this.GetComponent<Text>();
        timer = 0f;
        text.text = "FPS = " + (int)(1f / Time.unscaledDeltaTime);
        text.fontSize = Screen.currentResolution.width / division;
    }

    // change text fps
    void Update()
    {
        timer += Time.deltaTime;
        count++;
        if (timer >= 1f)
        {
            text.text = "FPS = " + count;
            count = 0;
            timer = 0;
        }
    }
}
