﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPooling : MonoBehaviour
{
    [SerializeField] GameObject prefEnemy = null;
    [SerializeField] GameObject prefEnemyArmored = null;
    [SerializeField] GameObject prefEnemyGiant = null;
    List<EnemyBehaviour> nonActiveEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        // Get all of the child so we dont have to use transform everytime we called it
        // Using the class instead of GameObject to avoid usage of GameObject class
        nonActiveEnemies = new List<EnemyBehaviour>();

        foreach (Transform child in this.transform)
            nonActiveEnemies.Add(child.GetComponent<EnemyBehaviour>());
    }

    public GameObject pooling()
    {
        //pooling a nonactive object, if there is a data in the list
        if (nonActiveEnemies.Count != 0)
        {
            GameObject enemy = nonActiveEnemies[0].gameObject;
            nonActiveEnemies.RemoveAt(0);
            return enemy;
        }

        // Spawn a random enemy
        GameObject instantiatedEnemy;
        int random = Random.Range(0, GameManager.instance.enemyVariation);
        Debug.Log(GameManager.instance.enemyVariation);
        switch (random)
        {
            case 0:
                instantiatedEnemy = Instantiate(prefEnemy);
                break;
            case 1:
                instantiatedEnemy = Instantiate(prefEnemyArmored);
                break;
            case 2:
                instantiatedEnemy = Instantiate(prefEnemyGiant);
                break;
            default:
                instantiatedEnemy = Instantiate(prefEnemy);
                break;
        }

        // Pooling a new instantiated object since the list is empty and added the pooler, so it knows its original parent
        instantiatedEnemy.GetComponent<EnemyBehaviour>().changePooler(this);
        instantiatedEnemy.gameObject.layer = 0;
        return instantiatedEnemy;
    }

    // Despawn the enemy, and put it back to the nonActive list
    public void insertBack(EnemyBehaviour enemy)
    {
        enemy.despawn();
        nonActiveEnemies.Add(enemy);
        enemy.changeParent(this.transform);
    }
}
