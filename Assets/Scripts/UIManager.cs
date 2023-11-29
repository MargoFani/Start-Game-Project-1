using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas startUI;
    [SerializeField] Text startText;
    [SerializeField] Canvas gameUI;
    [SerializeField] Canvas endUI;
    [SerializeField] Text endText;

    private float startTimer = 3;
    public static EventHandler OnGameStarted;
    // Start is called before the first frame update

    private void Awake()
    {
        Game.OnEndGame += ShowEndUI_OnEndGame;
    }

    private void ShowEndUI_OnEndGame(object sender, Game.OnEndGameEventArgs e)
    {
        gameUI.enabled = false;
        endUI.enabled = true;
        endText.text = e.points.ToString();
    }

    private void Start()
    {
        startUI.enabled = true;
        gameUI.enabled = false;
        endUI.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        startTimer -= Time.deltaTime;
        if(startTimer > 0)
        {
           startText.text = Math.Ceiling(startTimer).ToString();
        }else if(startTimer > -1.5f)
        {
            startText.text = "Start!";
            OnGameStarted?.Invoke(this, EventArgs.Empty);
            gameUI.enabled = true;
        }
        else
        {
            startUI.enabled = false;
        }        

    }

    public void OnClickNewGame()
    {
        endUI.enabled = false; 
        startUI.enabled = true;
        startTimer = 3;
        Debug.Log("OnClickNewGame");
    }
}
