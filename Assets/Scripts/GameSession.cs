﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {
    [SerializeField] int score = 0;

    void Awake() {
        SetUpSingleton();
    }

    void SetUpSingleton() {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() {
        return score;
    }

    public void AddToScore(int sc) {
        score += sc;
    }

    public void ResetGame() {
        score = 0;
    }
}