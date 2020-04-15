using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum DifficultyType
{
    EASY,
    MEDIUM,
    HARD
}

public class QuestionManager : MonoBehaviour
{
    // Attribute for answer and question
    public List<QuestionsAnswers> easyQuestions;
    public List<QuestionsAnswers> mediumQuestions;
    public List<QuestionsAnswers> hardQuestions;

    // UI attribute
    List<Button> possibleAnswers;
    List<Text> possibleAnswersText;

    public Text questionUI;

    // how many times answer shuffled
    [SerializeField] int epoch = 3;
    int currentRightAnswer;

    // Truck property
    TruckBehaviour truck = null;
    TextMesh charCharge = null;
    Point truckSpawnPoint = null; // Koto Tower location

    // koto tower
    ClickOnKotoTower kotoTower = null; // Koto Tower

    // Current question and answers
    QuestionsAnswers currQuestion;

    // Queue of difficulty Question
    // easy : all 5 easy
    // medium : 3 easy + 5 medium
    // hard : 2 east + 3 medium + 5 hard
    Queue<DifficultyType> questions;
    DifficultyType currentDifficulty;

    // Are the trucks on the way, prevent any other truck to spawn
    public static bool isSendingTruck = false;

    // variable initialization
    private void Awake()
    {
        // find all object
        truck = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckBehaviour>();
        charCharge = GameObject.FindGameObjectWithTag("Char Charge").GetComponent<TextMesh>();
        truck.gameObject.SetActive(false);

        // for koto Tower
        truckSpawnPoint = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<Point>();
        kotoTower = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<ClickOnKotoTower>();

        // possible answer's button
        possibleAnswers = new List<Button>();
        possibleAnswers.AddRange(kotoTower.gameObject.GetComponentsInChildren<Button>());

        // possible anwer
        possibleAnswersText = new List<Text>();
        foreach (Button answer in possibleAnswers)
            possibleAnswersText.Add(answer.GetComponentInChildren<Text>());

        // easy : all 5 easy
        // medium : 3 easy + 5 medium
        // hard : 2 easy + 3 medium + 5 hard
        questions = new Queue<DifficultyType>();
        switch (GameManager.instance.difficultyIdx)
        {
            case 0:
                for (int i = 0; i < 5; i++)
                    questions.Enqueue(DifficultyType.EASY);
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                    questions.Enqueue(DifficultyType.EASY);
                for (int i = 0; i < 5; i++)
                    questions.Enqueue(DifficultyType.MEDIUM);
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                    questions.Enqueue(DifficultyType.EASY);
                for (int i = 0; i < 3; i++)
                    questions.Enqueue(DifficultyType.MEDIUM);
                for (int i = 0; i < 5; i++)
                    questions.Enqueue(DifficultyType.HARD);
                break;
            default:
                break;
        }
        GameManager.instance.isNewQuestion = true;
    }

    // Shuffling the answer
    private void Start()
    {
        isSendingTruck = false;

        foreach (QuestionsAnswers question in easyQuestions)
            question.setAnswers(OtherMethod.shuffle<Answer>(question.getAnswers(), epoch));

        foreach (QuestionsAnswers question in mediumQuestions)
            question.setAnswers(OtherMethod.shuffle<Answer>(question.getAnswers(), epoch));

        foreach (QuestionsAnswers question in hardQuestions)
            question.setAnswers(OtherMethod.shuffle<Answer>(question.getAnswers(), epoch));

        getNewQuestion(true);

        currentRightAnswer = 0;
        // game event controller
        GameEvents.current.onTruckDestroyedEnter += OnTruckDestroyed;
    }

    void OnTruckDestroyed(bool isTruckDestroyed)
    {
        StartCoroutine(getNewQuestionBasedOnTruck(isTruckDestroyed));
    }

    // if the truck is destroyed then get same difficulty question but if its not then the answer is right
    IEnumerator getNewQuestionBasedOnTruck(bool isTruckDestroyed)
    {
        yield return new WaitForSeconds(isTruckDestroyed ? 10f : 0f);
        if(!GameManager.instance.isPractice)
            getNewQuestion(GameManager.instance.totalAnsweredQuestion != currentRightAnswer ? true : false);
        else
            getNewQuestion(isTruckDestroyed ? false : true);
    }

    // Function to get the question with answers
    public void getNewQuestion(bool gotRight)
    {
        // get random answer for this test if its the start or the player got the answer right
        if (gotRight)
        {
            // remove the question so the player will not get the same one, only when playing
            if (!GameManager.instance.isPractice)
            {
                switch (currentDifficulty)
                {
                    case DifficultyType.EASY:
                        easyQuestions.Remove(currQuestion);
                        break;
                    case DifficultyType.MEDIUM:
                        mediumQuestions.Remove(currQuestion);
                        break;
                    case DifficultyType.HARD:
                        hardQuestions.Remove(currQuestion);
                        break;
                }
            }

            // get another difficulty
            currentDifficulty = questions.Dequeue();
            currentRightAnswer = GameManager.instance.totalAnsweredQuestion;
        }

        // find randomizer based on queue
        int randomQuestionIdx;
        QuestionsAnswers question;

        switch (currentDifficulty)
        {
            case DifficultyType.EASY:
                randomQuestionIdx = Random.Range(0, easyQuestions.Count);
                question = easyQuestions[randomQuestionIdx];
                break;
            case DifficultyType.MEDIUM:
                randomQuestionIdx = Random.Range(0, mediumQuestions.Count);
                question = mediumQuestions[randomQuestionIdx];
                break;
            case DifficultyType.HARD:
                randomQuestionIdx = Random.Range(0, hardQuestions.Count);
                question = hardQuestions[randomQuestionIdx];
                break;
            default:
                randomQuestionIdx = Random.Range(0, easyQuestions.Count);
                question = easyQuestions[randomQuestionIdx];
                break;
        }

        if (gotRight)
            // put back at the queue
            questions.Enqueue(currentDifficulty);

        currQuestion = question;
        questionUI.text = question.getQuestion();

        // put Audio clip to the question if there is one or clear the audio clip if doesn't
        AudioSource audioSrc = questionUI.gameObject.GetComponent<AudioSource>();
        audioSrc.clip = question.getAudio();

        for (int i = 0; i < question.getAnswerCount(); i++)
            possibleAnswersText[i].text = question.getAnswerAtIndex(i).answer;

        disableOrEnabledAnswers(true);
        GameManager.instance.isNewQuestion = true;
        kotoTower.StartCoroutine(kotoTower.openKotoTower());
    }

    // Sending the answer with a truck
    public void sendAnswer(int idx)
    {
        if (!isSendingTruck)
        {
            Answer answer = currQuestion.getAnswerAtIndex(idx);
            charCharge.text = answer.answer;
            // Truck property
            truck.setCharCharge(answer);
            truck.spawn(truckSpawnPoint.getCurrPosition());
            truck.changeTargetFromCurrPoint(truckSpawnPoint);
            isSendingTruck = true;
            disableOrEnabledAnswers(false);
            GameEvents.current.TruckSentEnter();
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
        GameEvents.current.onTruckDestroyedEnter -= OnTruckDestroyed;
    }
}
