using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer
{
    public string answer;
    public bool isRightAnswer;

    // construtor
    Answer()
    {
        answer = "";
        isRightAnswer = false;
    }

    // a method to change the answer string and is right property
    public void setAnswer(string answer, bool isRightAnswer)
    {
        this.answer = answer;
        this.isRightAnswer = isRightAnswer;
    }
}
