using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionsAnswers
{
    [SerializeField] string question = null;
    [SerializeField] List<Answer> answers = null;
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
    public void setAnswers(List<Answer> answers)
    {
        this.answers = new List<Answer>(answers);
    }

    // to Get how much is possible answer
    public int getAnswerCount()
    {
        return answers.Count;
    }

    // get answer at certain index
    public Answer getAnswerAtIndex(int idx)
    { 
        return this.answers[idx];
    }

    // get ther question
    public string getQuestion()
    {
        return this.question;
    }

    // Get all the answers
    public List<Answer> getAnswers()
    {
        return this.answers;
    }

    // Get audio
    public AudioClip getAudio()
    {
        return this.audio;
    }
}
