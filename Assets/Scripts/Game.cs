using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private float spownTimer = 0;
    //игра длится 30 секунд
    private float gameTimer = 30f;

    [SerializeField] private Text actualPlayerPoints;

    [SerializeField] private Player player;

    [SerializeField] private Transform[] startPoints;
    [SerializeField] private Transform[] enemyPrefabs;
    //7 троекторий, по которым могут перемещаться враги
    private Enemy[] enemiesPlaces = new Enemy[7];
    //очередь появления врагов
    private Queue<Enemy.EnemyType> spownQueue = new Queue<Enemy.EnemyType>();

    private int greenEnemyCount = 21;
    private int orangeEnemyCount = 9;
    private int allEnemyCount = 30;

    private bool IsGameStarted = false;


    public static EventHandler<OnEndGameEventArgs> OnEndGame;
    public class OnEndGameEventArgs : EventArgs
    {
        public int points;
    }
    private void Awake()
    {
        UIManager.OnGameStarted += StartGame_OnGameStarted;
    }

    private void StartGame_OnGameStarted(object sender, EventArgs e)
    {
        spownTimer = 0;
        gameTimer = 30f;
        player.PlayerPoints = 0;
        actualPlayerPoints.text = player.PlayerPoints.ToString();
        for (int i = 0; i < enemiesPlaces.Length; i++)
        {
            if (enemiesPlaces[i] != null)
            {
                Destroy(enemiesPlaces[i].gameObject);
            }
        }
        spownQueue = new Queue<Enemy.EnemyType>();
        GenerateSpownQueue();
        IsGameStarted = true;
        //var queue = string.Concat(spownQueue.Select(e => (int)e));
        //Debug.Log(queue);


    }

    private void Update()
    {
        if (IsGameStarted)
        {
            spownTimer += Time.deltaTime;
            gameTimer -= Time.deltaTime;
            actualPlayerPoints.text = player.PlayerPoints.ToString();
            if (gameTimer < 0)
            {
                Debug.Log("player.PlayerPoints: " + player.PlayerPoints);
                OnEndGame?.Invoke(this, new OnEndGameEventArgs { points = this.player.PlayerPoints });
                IsGameStarted = false;
                return;
            }
            if (1 - spownTimer < 0)
            {
                GenerateEnemy();
                spownTimer -= 1;
            }


        }

    }

    private void GenerateEnemy()
    {
        System.Random rand = new System.Random();
        //генерируем рандомный номер
        int index = rand.Next(enemiesPlaces.Length);
        
        if (enemiesPlaces[index] == null)
        {
            //если по этому индексу нет врага, то создаем его
            enemiesPlaces[index] = Instantiate(enemyPrefabs[(int)spownQueue.Dequeue()], startPoints[index].position, Quaternion.identity).GetComponent<Enemy>();
                       
        }
        else
        {
            // если по индексу есть враг, перебираем все свободные места, и создаем врага в первом свободном месте
            for (int i = 0; i < enemiesPlaces.Length; i++)
            {
                if (enemiesPlaces[i] == null)
                {
                    enemiesPlaces[i] = Instantiate(enemyPrefabs[(int)spownQueue.Dequeue()], startPoints[i].position, Quaternion.identity).GetComponent<Enemy>();
                    return;
                }

            }
        }

        
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

        tempEnemies = Shuffle(tempEnemies);
        //заносим в очередь
        for (int i = 0; i < tempEnemies.Length; i++)
        {
            spownQueue.Enqueue(tempEnemies[i]);
        }
    }

    private Enemy.EnemyType[] Shuffle(Enemy.EnemyType[] array)
    {
        System.Random rand = new System.Random();
       
        //перемешиваем созданный массив
        for (int i = array.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            Enemy.EnemyType tmp = array[j];
            array[j] = array[i];
            array[i] = tmp;
        }
        for (int i = array.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            Enemy.EnemyType tmp = array[j];
            array[j] = array[i];
            array[i] = tmp;
        }
        Enemy.EnemyType[] resultArray = array;
        return array;
    }

}
