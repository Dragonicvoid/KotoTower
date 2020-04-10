using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseWinController : MonoBehaviour
{
    [SerializeField] Text lostWinText; 
    // change Text to Lose or Win
    public void changeText(bool isWin)
    {
        if (isWin)
            lostWinText.text = "MENANG!";
        else
            lostWinText.text = "KALAH!";
    }
}
