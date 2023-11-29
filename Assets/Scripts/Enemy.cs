using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int points = 0;
    [SerializeField] private EnemyType type;
    [SerializeField] private float lifeTime = 0;
    [SerializeField] private float distance = 0;
    [SerializeField] private float speed = 0;

    public static EventHandler OnLifeTimeEnd;
    public static EventHandler<OnHitEventArgs> OnHit;
    public class OnHitEventArgs : EventArgs
    {
        public int points;

    }
    private float timer = -1;
    // зеленые - 5 очков, оранжевые - 7
    //private int[] pointsToType = new int[] { 5, 7 };
    // время жизни зеленые - 2,5 секунд, оранжевые - 2 секунды
    //private float[] lifeTimeToType = new float[] { 2.5f, 2f };

    private void Start()
    {
        StartLifeLifetime();
    }
    private void Update()
    {
        if (timer != -1)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer < 0)
            {
                EndLifeTime();
                //сделать ивент и вычесть у игрока 1 очко
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - distance), speed*Time.deltaTime);

    }
    //private void SetPointsAndLifeTime()
    //{        
    //    points = pointsToType[(int) type];
    //    lifeTime = lifeTimeToType[(int) type];

    //}

    //public void SetType(EnemyType t)
    //{
    //    type = t;
    //    //for test
    //    SetPointsAndLifeTime();
    //}
    //включается после создания объекта на сцене
    public void StartLifeLifetime()
    {
        timer = lifeTime;
    }

    public void SendPointsToPlayer()
    {
        OnHit?.Invoke(this, new OnHitEventArgs { points = this.points});
        Destroy(this.gameObject);
    }


    private void EndLifeTime()
    {
        OnLifeTimeEnd?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }
    public enum EnemyType { Green, Orange }
}
