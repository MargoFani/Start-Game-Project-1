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
    private bool CanMove = true;
    public class OnHitEventArgs : EventArgs
    {
        public int points;

    }
    private float timer = -1;

    private void Awake()
    {
        Game.OnEndGame += StopMovement_OnEndGame;
    }
    private void Start()
    {
        StartLifeLifetime();
    }
    private void Update()
    {
        if (CanMove)
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

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - distance), speed * Time.deltaTime);

        }
    }

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

    private void StopMovement_OnEndGame(object sener, Game.OnEndGameEventArgs e)
    {
        CanMove = false;
        Debug.Log("StopMovement_OnEndGame");
    }
    public enum EnemyType { Green, Orange }
}
