using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoneyTesting : MonoBehaviour
{
    //size of the font base on division of width resolution
    [SerializeField] int division = 25;
    // Get its own text
    Text text;

    // on start take value on Game Manager
    private void Start()
    {
        text = this.GetComponent<Text>();
        text.text = "Money = " + (int)GameManager.money;
        text.fontSize = Screen.width / division;
    }

    // change the money if there is a change
    private void Update()
    {
        if (GameManager.moneyChanged)
        {
            text.text = "Money = " + (int)GameManager.money;
            GameManager.moneyChanged = false;
        }
    }
}
