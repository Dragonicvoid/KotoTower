using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManagerTesting : MonoBehaviour
{
    // Attribute for answer and question
    public QuestionsAnswersTesting[] easyQuestions;
    public QuestionsAnswersTesting[] mediumQuestions;
    public QuestionsAnswersTesting[] hardQuestions;

    // UI attribute
    public Text[] possibleAnswers;
    public Text questionUI;

    // how many times answer shuffled
    [SerializeField] int epoch;

    // Truck property
    [SerializeField] TruckBehaviourTesting truck;
    [SerializeField] TextMesh charCharge;
    [SerializeField] PointTesting truckSpawnPoint; // Koto Tower

    // Current question and answers
    QuestionsAnswersTesting currQuestion;

    // Are the trucks on the way, prevent any other truck to spawn
    public static bool isSendingTruck = false;

    // Shuffling the answer
    private void Start()
    {
        isSendingTruck = false;

        foreach (QuestionsAnswersTesting question in easyQuestions)
            question.setAnswers(OtherMethodTesting.shuffle<AnswerTesting>(question.getAnswers(), epoch));

        foreach (QuestionsAnswersTesting question in mediumQuestions)
            question.setAnswers(OtherMethodTesting.shuffle<AnswerTesting>(question.getAnswers(), epoch));

        foreach (QuestionsAnswersTesting question in hardQuestions)
            question.setAnswers(OtherMethodTesting.shuffle<AnswerTesting>(question.getAnswers(), epoch));

        getNewQuestion();
    }

    // Function to get the question with answers
    public void getNewQuestion()
    {
        // get random answer for this test
        int randomQuestionIdx = Random.Range(0, easyQuestions.Length);
        QuestionsAnswersTesting question = easyQuestions[randomQuestionIdx];

        currQuestion = question;
        questionUI.text = question.getQuestion();

        // put Audio clip to the question if there is one or clear the audio clip if doesn't
        AudioSource audioSrc = questionUI.gameObject.GetComponent<AudioSource>();
        audioSrc.clip = question.getAudio();

        for (int i = 0; i < question.getAnswerCount(); i++)
            possibleAnswers[i].text = question.getAnswerAtIndex(i).answer;
    }

    // Sending the answer with a truck
    public void sendAnswer(int idx)
    {
        if (!isSendingTruck)
        {
            AnswerTesting answer = currQuestion.getAnswerAtIndex(idx);
            charCharge.text = answer.answer;
            Debug.Log(isSendingTruck);
            // Truck property
            truck.setCharCharge(answer);
            truck.spawn(truckSpawnPoint.getCurrPosition());
            truck.changeTargetFromCurrPoint(truckSpawnPoint.getSecondIndexPoint());
            isSendingTruck = true;
        }
    }
}
