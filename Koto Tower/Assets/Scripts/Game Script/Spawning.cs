using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    #region Serialized Field Variables definition
    [SerializeField] EnemiesPooling inactiveObject = null;
    [SerializeField] Transform activeObject = null;
    [SerializeField] List<float> spawnTimer = new List<float>();
    // Change diffculty to next timer after 100s
    [SerializeField] float forceChangeDiffCountdown = 30f;
    #endregion

    #region Private Variables definition
    List<Point> spawnPoints;
    Point point;
    Vector2 spawnLocation;
    float timer = 0f;
    float spawnNow = 1f;
    float changeTimer = 0f;
    int diffCount = 0;
    int groupSize = 1;
    bool startSpawning = false;
    bool currentlySpawning = false;
    #endregion

    #region Monobehaviour Method
    // Start is called before the first frame update
    void Awake()
    {
        // Putting every position of spawn points of the Level to a list, so we dont have to use
        // transform everytime we want it.
        spawnPoints = new List<Point>();

        foreach (Transform child in this.transform)
            spawnPoints.Add(child.GetComponent<Point>());
    }

    //initialization
    private void Start()
    {
        // get the respawn timer to the first of the spawn timer list
        spawnNow = spawnTimer[0];
        timer = 0f;
        changeTimer = 0f;
        diffCount = 0;
        startSpawning = false;
        currentlySpawning = false;

        if(GameManager.instance.isTutorial)
            StartCoroutine(spawnZombies());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPaused && !GameManager.instance.isPractice)
        {
            // for tutorial
            if (GameManager.instance.isTutorial)
            {
                if (GameManager.instance.isChangedEnemyGroupSize)
                {
                    groupSize++;
                    GameManager.instance.isChangedEnemyGroupSize = false;
                }

                int childCount = activeObject.childCount;
                if (childCount < groupSize)
                    timer += Time.deltaTime;

                if (timer >= 10f)
                    StartCoroutine(spawnZombies());

                return;
            }

            if (GameManager.instance.isChangedDifficulty || changeTimer >= forceChangeDiffCountdown)
            {
                diffCount++;
                if (diffCount >= spawnTimer.Count)
                    spawnNow = spawnTimer[spawnTimer.Count - 1];
                else
                    spawnNow = spawnTimer[diffCount];
                GameManager.instance.isChangedDifficulty = false;

                // if the change timer is longer force change and diffculty is at the end of diff then just spawn a bunch
                if (diffCount >= spawnTimer.Count + 3)
                    spawnNow = 0.5f;

                changeTimer = 0f;
            }

            if (GameManager.instance.isChangedEnemyGroupSize)
            {
                groupSize++;
                GameManager.instance.isChangedEnemyGroupSize = false;
            }

            // Every "spawnNow" second spawn group of Zombies
            if (timer >= spawnNow && !startSpawning)
                startSpawning = true;

            if (startSpawning && !currentlySpawning)
                StartCoroutine(spawnZombies());

            timer += Time.deltaTime;
            changeTimer += Time.deltaTime;
        }
    }
    #endregion

    #region Private Method
    // spawn a bunch zombie
    IEnumerator spawnZombies()
    {
        float groupSpawner = 0.3f;
        float groupTimer = 0f;
        int zombieCount = 0;
        timer = 0f;
        currentlySpawning = true;

        while (zombieCount < groupSize)
        {
            yield return null;
            if (groupTimer >= groupSpawner && !GameManager.instance.isPaused)
            {
                // Getting random spawn location 
                int randIdx = Random.Range(0, spawnPoints.Count);
                point = spawnPoints[(randIdx < 0 ? 0 : randIdx)];
                spawnLocation = point.getCurrPosition();

                // Spawn enemy from pool according to the cost
                enemiesPool(spawnLocation, point);
                groupTimer = 0;
                zombieCount++;
            }
            else if (!GameManager.instance.isPaused)
                groupTimer += Time.deltaTime;
        }

        // reset the timer
        startSpawning = false;
        currentlySpawning = false;
    }

    // Adding cost for creating power for each wave
    EnemyBehaviour enemiesPool(Vector2 spawnLocation, Point target)
    {
        //Initializing all agent attribute
        GameObject enemyObj = inactiveObject.pooling();
        EnemyBehaviour enemy = enemyObj.GetComponent<EnemyBehaviour>();
        enemy.changeParent(activeObject);
        enemy.changeTargetFromCurrPoint(target);
        enemy.spawn(spawnLocation);

        return enemy;
    }
    #endregion
}
