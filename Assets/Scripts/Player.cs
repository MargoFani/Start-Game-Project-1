using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerPoints { get; private set; } = 0;

    private void Awake()
    {
        Enemy.OnLifeTimeEnd += Fine_OnLifeTimeEnd;
        Enemy.OnHit += GetPoint_OnHit;
    }

        private void GetPoint_OnHit(object sender, Enemy.OnHitEventArgs e)
    {
        PlayerPoints += e.points;
    }

    private void Fine_OnLifeTimeEnd(object sender, EventArgs e)
    {
        //Debug.Log("Fine_OnLifeTimeEnd");
        if (PlayerPoints - 1 != 0)
        {
            PlayerPoints--;
        }
    }
}
