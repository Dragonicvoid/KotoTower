using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarTesting : MonoBehaviour
{
    // variables
    int currentAnswered;
    Slider slider;
    Text progressText;

    // start the slider at 0
    private void Start()
    {
        progressText = this.gameObject.GetComponentInChildren<Text>();
        progressText.text = "0 / " + GameManager.maxCharged;
        currentAnswered = 0;
        slider = this.gameObject.GetComponent<Slider>();
        slider.value = 0;
    }

    // update the slider value if there is changed
    private void Update()
    {
        if (currentAnswered != GameManager.totalAnsweredQuestion)
        {
            slider.value = (float)GameManager.totalAnsweredQuestion / GameManager.maxCharged;
            currentAnswered = GameManager.totalAnsweredQuestion;
            progressText.text = currentAnswered + " / " + GameManager.maxCharged;
        }       
    }
}
