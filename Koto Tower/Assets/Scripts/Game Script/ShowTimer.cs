using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTimer : MonoBehaviour
{
    Text ownText;

    // initialization
    private void Start()
    {
        ownText = this.gameObject.GetComponent<Text>();    
    }

    // change timer text everytime
    private void Update()
    {
        if (GameManager.instance.scoreboard.time != 0)
        {
            float minute = GameManager.instance.scoreboard.time / 60f;
            float second = GameManager.instance.scoreboard.time - (60f * Mathf.FloorToInt(minute));

            if (minute >= 10)
                if(minute >= 99)
                    ownText.text = "99";
                else
                    ownText.text = Mathf.FloorToInt(minute).ToString();
            else
                ownText.text = "0" + Mathf.FloorToInt(minute).ToString();

            if(second >= 10)
                if (minute >= 99)
                    ownText.text += ":00";
                else
                    ownText.text += ":" + Mathf.FloorToInt(second).ToString();
            else
                ownText.text += ":0" + Mathf.FloorToInt(second).ToString();
        }
        else
            ownText.text = "00:00";

    }
}
