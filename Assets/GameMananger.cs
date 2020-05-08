using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameMananger : MonoBehaviour
{

    public static GameMananger Ins;

    public bool isGamePause = true;
    public bool isGameOver = true;

    public delegate void eventStartGame();
    public event eventStartGame event_Start_Game;

    private void Awake()
    {
        if (Ins != null)
        {
            Destroy(gameObject);

        }
        else
        {
            Ins = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        isGamePause = false;
        isGamePause = false;
    }

    public void OverGame()
    {
        isGamePause = true;
        isGamePause = true;
    }

}
