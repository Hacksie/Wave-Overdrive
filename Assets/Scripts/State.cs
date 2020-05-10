using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [System.Serializable]
    public class State 
    {
        public GameState gameState;
        public int score;
        public int level;
        public int health;
        public int maxHealth;
        //public float damage;
        //public float shield;
        public int pickups;
        public int kills;
        public string code;
        public Stats playerStats;

        public State()
        {
            Reset();
        }
        public void Reset()
        {
            score = 0;
            level = 1;
            health = 50;
            maxHealth = 100;
            //damage = 0;
            //shield = 0;
            gameState = GameState.Staging;
        }

        public void SoftReset()
        {
            health = 100;
            gameState = GameState.Staging;
        }
    }

    [System.Serializable]
    public class Stats
    {
        public float damage;
        public float shield;
    }

    public enum GameState
    {
        Menu,
        Staging,
        Playing,
        EndLevel,
        Crash,
        Win
    }
}
