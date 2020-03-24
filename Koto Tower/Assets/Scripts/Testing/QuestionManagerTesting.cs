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
    List<Button> possibleAnswers;
    List<Text> possibleAnswersText;

    public Text questionUI;

    // how many times answer shuffled
    [SerializeField] int epoch = 3;

    // Truck property
    TruckBehaviourTesting truck = null;
    TextMesh charCharge = null;
    PointTesting truckSpawnPoint = null; // Koto Tower
    ClickOnKotoTowerTesting kotoTower = null; // Koto Tower

    // Current question and answers
    QuestionsAnswersTesting currQuestion;

    // Are the trucks on the way, prevent any other truck to spawn
    public static bool isSendingTruck = false;

    // variable initialization
    private void Awake()
    {
        // find all object
        truck = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckBehaviourTesting>();
        charCharge = GameObject.FindGameObjectWithTag("Char Charge").GetComponent<TextMesh>();
        truck.gameObject.SetActive(false);

        // for koto Tower
        truckSpawnPoint = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<PointTesting>();
        kotoTower = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<ClickOnKotoTowerTesting>();

        // possible answer's button
        possibleAnswers = new List<Button>();
        possibleAnswers.AddRange(kotoTower.gameObject.GetComponentsInChildren<Button>());

        // possible anwer
        possibleAnswersText = new List<Text>();
        foreach (Button answer in possibleAnswers)
            possibleAnswersText.Add(answer.GetComponentInChildren<Text>());
    }

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

        // game event controller
        GameEventsTesting.current.onTruckDestroyedEnter += OnTruckDestroyed;
    }

    void OnTruckDestroyed(bool isTruckDestroyed)
    {
        StartCoroutine(getNewQuestion(isTruckDestroyed));
    }

    IEnumerator getNewQuestion(bool isTruckDestroyed)
    {
        yield return new WaitForSeconds(isTruckDestroyed ? 10f : 0f);
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
            possibleAnswersText[i].text = question.getAnswerAtIndex(i).answer;

        disableOrEnabledAnswers(true);
        kotoTower.StartCoroutine(kotoTower.openKotoTower());
    }

    // Sending the answer with a truck
    public void sendAnswer(int idx)
    {
        if (!isSendingTruck)
        {
            AnswerTesting answer = currQuestion.getAnswerAtIndex(idx);
            charCharge.text = answer.answer;
            // Truck property
            truck.setCharCharge(answer);
            truck.spawn(truckSpawnPoint.getCurrPosition());
            truck.changeTargetFromCurrPoint(truckSpawnPoint);
            isSendingTruck = true;
            kotoTower.StartCoroutine(kotoTower.closeKotoTower());
            disableOrEnabledAnswers(false);
            GameEventsTesting.current.TruckSentEnter();
        }
    }

    // Disable button if the answer is sent
    void disableOrEnabledAnswers(bool status)
    {
        foreach (Button button in possibleAnswers)
            button.interactable = status;
    }

    private void OnDestroy()
    {
        // game event controller
        GameEventsTesting.current.onTruckDestroyedEnter -= OnTruckDestroyed;
    }
}
