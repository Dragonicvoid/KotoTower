using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPoolingTesting : MonoBehaviour
{
    [SerializeField] GameObject prefEnemy;
    List<EnemyBehaviourTesting> nonActiveEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        // Get all of the child so we dont have to use transform everytime we called it
        // Using the class instead of GameObject to avoid usage of GameObject class
        nonActiveEnemies = new List<EnemyBehaviourTesting>();

        foreach (Transform child in this.transform)
            nonActiveEnemies.Add(child.GetComponent<EnemyBehaviourTesting>());
    }

    public EnemyBehaviourTesting pooling()
    {
        //pooling a nonactive object, if there is a data in the list
        if (nonActiveEnemies.Count != 0)
        {
            EnemyBehaviourTesting enemy = nonActiveEnemies[0];
            nonActiveEnemies.RemoveAt(0);
            return enemy;
        }

        // Pooling a new instantiated object since the list is empty and added the pooler, so it knows its original parent
        EnemyBehaviourTesting instantiatedEnemy = Instantiate(prefEnemy).GetComponent<EnemyBehaviourTesting>();
        instantiatedEnemy.changePooler(this);
        return instantiatedEnemy;
    }

    // Despawn the enemy, and put it back to the nonActive list
    public void insertBack(EnemyBehaviourTesting enemy)
    {
        enemy.despawn();
        nonActiveEnemies.Add(enemy);
        enemy.changeParent(this.transform);
    }
}
