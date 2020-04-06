using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoney : MonoBehaviour
{
    // Get its own text
    Text text;

    // on start take value on Game Manager
    private void Start()
    {
        text = this.GetComponent<Text>();
        text.text = "Money = " + (int)GameManager.instance.money;
    }

    // change the money if there is a change
    private void Update()
    {
        if (GameManager.instance.moneyChanged)
        {
            text.text = "Money = " + (int)GameManager.instance.money;
            GameManager.instance.moneyChanged = false;
        }
    }
}
