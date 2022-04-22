using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GameStart,
        PlayerTurnStart,
        PlayerTurn,
        EnemyTurnStart,
        EnemyTurn,
        GameWin,
        GameLose,
        Pause
    }

    public GameState CurrentState;

    public static GameManager Instance;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        CurrentState = GameState.GameStart;
        TurnManager_SP.Instance.GameStart();
    }
}
