using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnswerTesting
{
    public string answer;
    public bool isRightAnswer;

    // construtor
    AnswerTesting()
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
