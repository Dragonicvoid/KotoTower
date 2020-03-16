using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionsAnswersTesting
{
    [SerializeField] string question = null;
    [SerializeField] List<AnswerTesting> answers = null;
    [SerializeField] AudioClip audio = null;

    // set the question
    public void setQuestion(string question)
    {
        this.question = question;
    }

    // set an answer at certain index
    public void setAnswerAtIndex(string answer, int index, bool isTheRightAnswer)
    {
        this.answers[index].setAnswer(answer, isTheRightAnswer);
    }

    // set a whole list
    public void setAnswers(List<AnswerTesting> answers)
    {
        this.answers = new List<AnswerTesting>(answers);
    }

    // to Get how much is possible answer
    public int getAnswerCount()
    {
        return answers.Count;
    }

    // get answer at certain index
    public AnswerTesting getAnswerAtIndex(int idx)
    { 
        return this.answers[idx];
    }

    // get ther question
    public string getQuestion()
    {
        return this.question;
    }

    // Get all the answers
    public List<AnswerTesting> getAnswers()
    {
        return this.answers;
    }

    // Get audio
    public AudioClip getAudio()
    {
        return this.audio;
    }
}
