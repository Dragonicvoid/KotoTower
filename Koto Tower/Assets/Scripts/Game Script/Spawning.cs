using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    #region Serialized Field Variables definition
    [SerializeField] EnemiesPooling inactiveObject = null;
    [SerializeField] Transform activeObject = null;
    [SerializeField] int cost = 5;
    [SerializeField] List<float> spawnTimer = new List<float>();
    // Change diffculty to next timer after 100s
    [SerializeField] float forceChangeDiffCountdown = 100f;
    #endregion

    #region Private Variables definition
    List<Point> spawnPoints;
    float timer = 0f;
    float spawnNow = 1f;
    float changeTimer = 0f;
    int diffCount = 0;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isChangedDifficulty || changeTimer >= forceChangeDiffCountdown)
        {
            diffCount++;
            if(diffCount >= spawnTimer.Count)
                spawnNow = spawnTimer[spawnTimer.Count - 1];
            else
                spawnNow = spawnTimer[diffCount];
            GameManager.instance.isChangedDifficulty = false;

            // if the change timer is longer force change and diffculty is at the end of diff then just spawn a bunch
            if (diffCount >= spawnTimer.Count + 5)
                spawnNow = 0.5f;

            changeTimer = 0f;
        }

        // Every "spawnNow" second spawn someone (might change how it works during development)
        if (timer >= spawnNow)
        {
            // Getting random spawn location 
            int randIdx = Random.Range(0, spawnPoints.Count);
            Point point = spawnPoints[(randIdx < 0 ? 0 : randIdx)];
            Vector2 spawnLocation = point.getCurrPosition();

            // Get enemies from pool according to the cost
            List<EnemyBehaviour> enemies = enemiesPool(cost, spawnLocation, point);

            // Resetting the timer
            timer = 0f;
        }

        timer += Time.deltaTime;
        changeTimer += Time.deltaTime;
    }
    #endregion

    #region Private Method
    // Adding cost for creating power for each wave
    List<EnemyBehaviour> enemiesPool(int cost, Vector2 spawnLocation, Point target)
    {
        List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();

        // haven't added cost function

        //Initializing all agent attribute
        GameObject enemyObj = inactiveObject.pooling();
        EnemyBehaviour enemy = enemyObj.GetComponent<EnemyBehaviour>();
        enemy.spawn(spawnLocation);
        enemy.changeParent(activeObject);
        enemy.changeTargetFromCurrPoint(target);
        enemies.Add(enemy);

        return enemies;
    }
    #endregion
}
