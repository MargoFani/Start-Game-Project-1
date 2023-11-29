using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    private float spownTimer = 0;
    //���� ������ 30 ������
    private float gameTimer = 30f;

    private int playerPoints = 0;
    [SerializeField] private Transform[] startPoints;
    [SerializeField] private Transform[] enemyPrefabs;
    //7 ����������, �� ������� ����� ������������ �����
    private Enemy[] enemiesPlaces = new Enemy[7];
    //������� ��������� ������
    private Queue<Enemy.EnemyType> spownQueue = new Queue<Enemy.EnemyType>();

    private int greenEnemyCount = 21;
    private int orangeEnemyCount = 9;
    private int allEnemyCount = 30;
    //����� ���������� ������ �������, ���� ������ 30 ������ => ���������� 30 ������
    //30% - ���������, 70% ������� 
    //��� 9 ��������� � 21 �������

    private void Awake()
    {
        Enemy.OnLifeTimeEnd += Fine_OnLifeTimeEnd;
        Enemy.OnHit += GetPoint_OnHit;
    }

    private void GetPoint_OnHit(object sender, Enemy.OnHitEventArgs e)
    {
        playerPoints += e.points;
    }

    private void Start()
    {
        GenerateSpownQueue();
        var queue = string.Concat(spownQueue.Select(e => (int) e));
        Debug.Log(queue);
        //enemiesPlaces[2] = Instantiate(enemyPrefabs[0], startPoints[2].position, Quaternion.identity).GetComponent<Enemy>();

        //enemiesPlaces[5] = Instantiate(enemyPrefabs[1], startPoints[5].position, Quaternion.identity).GetComponent<Enemy>();

    }
    private void Update()
    {
        Debug.Log("points: " + playerPoints);
        spownTimer += Time.deltaTime;
        if(1 - spownTimer < 0)
        {
            GenerateEnemy();
            spownTimer -= 1;
        }
    }
    private void Fine_OnLifeTimeEnd(object sender, EventArgs e)
    {
        //Debug.Log("Fine_OnLifeTimeEnd");
        if (playerPoints - 1 != 0)
        {
            playerPoints--;
        }
    }
    private void GenerateEnemy()
    {
        System.Random rand = new System.Random();
        //���������� ��������� �����
        int index = rand.Next(enemiesPlaces.Length);
        
        if (enemiesPlaces[index] == null)
        {
            //���� �� ����� ������� ��� �����, �� ������� ���
            enemiesPlaces[index] = Instantiate(enemyPrefabs[(int)spownQueue.Dequeue()], startPoints[index].position, Quaternion.identity).GetComponent<Enemy>();
                       
        }
        else
        {
            // ���� �� ������� ���� ����, ���������� ��� ��������� �����, � ������� ����� � ������ ��������� �����
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
            Debug.Log(i + " : " + tempEnemies[i]);
        }
        for (int i = greenEnemyCount; i < orangeEnemyCount + greenEnemyCount; i++)
        {
            tempEnemies[i] = Enemy.EnemyType.Orange;
            Debug.Log(i + " : " + tempEnemies[i]);
        }

        tempEnemies = Shuffle(tempEnemies);
        //������� � �������
        for (int i = 0; i < tempEnemies.Length; i++)
        {
            spownQueue.Enqueue(tempEnemies[i]);
        }
    }

    private Enemy.EnemyType[] Shuffle(Enemy.EnemyType[] array)
    {
        System.Random rand = new System.Random();
       
        //������������ ��������� ������
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
