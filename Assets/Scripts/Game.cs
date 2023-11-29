using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    private int playerPoints = 0;
    [SerializeField] private Transform[] startPoints;
    [SerializeField] private Transform[] enemyPrefabs;
    //7 троекторий, по которым могут перемещаться враги
    private Enemy[] enemiesPlaces = new Enemy[7];
    //очередь появления врагов
    private Queue<Enemy.EnemyType> spownQueue;

    private int greenEnemyCount = 21;
    private int orangeEnemyCount = 9;
    private int allEnemyCount = 30;
    //враги появляются каждую секунду, игра длится 30 секунд => появляется 30 врагов
    //30% - оранжевых, 70% зеленых 
    //или 9 оранжевых и 21 зеленый
    private void Awake()
    {
        Enemy.OnLifeTimeEnd += Fine_OnLifeTimeEnd;
    }
    private void Start()
    {

        enemiesPlaces[2] = Instantiate(enemyPrefabs[0], startPoints[2].position, Quaternion.identity).GetComponent<Enemy>();

        enemiesPlaces[5] = Instantiate(enemyPrefabs[1], startPoints[5].position, Quaternion.identity).GetComponent<Enemy>();

    }
    private void Fine_OnLifeTimeEnd(object sender, EventArgs e)
    {
        Debug.Log("Fine_OnLifeTimeEnd");
    }

    private void GenerateSpownQueue()
    {
        Enemy.EnemyType[] tempEnemies = new Enemy.EnemyType[allEnemyCount];
        for(int i = 0; i<greenEnemyCount; i++)
        {
            tempEnemies[i] = Enemy.EnemyType.Green;
        }
        for (int i = greenEnemyCount; i < orangeEnemyCount + greenEnemyCount; i++)
        {
            tempEnemies[i] = Enemy.EnemyType.Orange;
        }
        System.Random rand = new System.Random();
        //перемешиваем созданный массив
        for (int i = tempEnemies.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            Enemy.EnemyType tmp = tempEnemies[j];
            tempEnemies[j] = tempEnemies[i];
            tempEnemies[i] = tmp;
        }

        //заносим в очередь
        for (int i = 0; i< tempEnemies.Length; i++)
        {
            spownQueue.Enqueue(tempEnemies[i]);
        }
    }


}
