using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTesting : MonoBehaviour
{
    #region Serialized Field Variables definition
    [SerializeField] EnemiesPoolingTesting inactiveObject;
    [SerializeField] Transform activeObject;
    [SerializeField] int cost = 5;
    [SerializeField] float spawnNow = 1f;
    #endregion

    #region Private Variables definition
    List<PointTesting> spawnPoints;
    float timer = 0f;
    #endregion

    #region Monobehaviour Method
    // Start is called before the first frame update
    void Start()
    {
        // Putting every position of spawn points of the Level to a list, so we dont have to use
        // transform everytime we want it.
        spawnPoints = new List<PointTesting>();

        foreach (Transform child in this.transform)
            spawnPoints.Add(child.GetComponent<PointTesting>());
    }

    // Update is called once per frame
    void Update()
    {
        // Every "spawnNow" second spawn someone (might change how it works during development)
        if (timer >= spawnNow)
        {
            // Getting random spawn location 
            int randIdx = Random.Range(0, spawnPoints.Count);
            PointTesting point = spawnPoints[(randIdx < 0 ? 0 : randIdx)];
            Vector2 spawnLocation = point.getCurrPosition();

            // Get enemies from pool according to the cost
            List<EnemyBehaviourTesting> enemies = enemiesPool(cost, spawnLocation, point);

            // Resetting the timer
            timer = 0f;
        }

        timer += Time.deltaTime;
    }
    #endregion

    #region Private Method
    // Adding cost for creating power for each wave
    List<EnemyBehaviourTesting> enemiesPool(int cost, Vector2 spawnLocation, PointTesting target)
    {
        List<EnemyBehaviourTesting> enemies = new List<EnemyBehaviourTesting>();

        // haven't added cost function

        //Initializing all agent attribute
        EnemyBehaviourTesting enemy = inactiveObject.pooling();
        enemy.spawn(spawnLocation);
        enemy.changeParent(activeObject);
        enemy.changeTargetFromCurrPoint(target);
        enemies.Add(enemy);

        return enemies;
    }
    #endregion
}
