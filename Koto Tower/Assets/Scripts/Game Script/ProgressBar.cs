using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    // variables
    int currentAnswered;
    Slider slider;
    Text progressText;
    float changeTime;
    float timer;
    bool isDone;
    float width, height;
    RectTransform rectTrans;

    // start the slider at 0 and some initialization
    private void Start()
    {
        isDone = false;
        timer = 1.1f;
        changeTime = 1f;
        progressText = this.gameObject.GetComponentInChildren<Text>();
        progressText.text = "0 / " + GameManager.instance.maxCharged;
        currentAnswered = 0;
        slider = this.gameObject.GetComponent<Slider>();
        slider.value = 0;
        rectTrans = this.GetComponent<RectTransform>();
        width = rectTrans.rect.width;
        height = rectTrans.rect.height;

        this.gameObject.SetActive(!GameManager.instance.isPractice);
    }

    // update the slider value if there is changed
    private void Update()
    {
        if (!GameManager.instance.isPaused && !GameManager.instance.isPractice)
        {
            if (timer < changeTime)
                timer += Time.deltaTime;

            if (currentAnswered != GameManager.instance.totalAnsweredQuestion)
            {
                currentAnswered = GameManager.instance.totalAnsweredQuestion;
                progressText.text = currentAnswered + " / " + GameManager.instance.maxCharged;

                timer = 0;
                isDone = false;
            }

            if (!isDone)
                slider.value = (float)((float)currentAnswered - (1 - deltaTime())) / GameManager.instance.maxCharged;
        }
    }

    // ease in delta time
    private float deltaTime()
    {
        if (timer < changeTime)
            return Mathf.Pow(timer / changeTime, 2);
        else
        {
            isDone = true;
            return 1;
        }
    }
}
