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
        text.text = "" + (int)GameManager.instance.money;
        Transform parent = this.transform.parent;
        parent.gameObject.SetActive(!GameManager.instance.isPractice);
    }

    // change the money if there is a change
    private void Update()
    {
        if (GameManager.instance.moneyChanged && !GameManager.instance.isPaused)
        {
            text.text = "" + (int)GameManager.instance.money;
            GameManager.instance.moneyChanged = false;
        }
    }
}
