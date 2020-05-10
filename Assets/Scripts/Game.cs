using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        public static Game instance;

        [Header("State")]
        [SerializeField] public State state = new State();
        [SerializeField] public PlayerPreferences preferences = new PlayerPreferences();
        [Header("Referenced GameObjects")]
        [SerializeField] public PlayerController player = null;
        [SerializeField] public Pool pool = null;

        [Header("UI")]
        [SerializeField] private Hud hudPanel = null;
        [SerializeField] private Staging stagingPanel = null;
        [SerializeField] private GameOverPresenter gameOverPanel = null;

        [Header("Settings")]
        [SerializeField] private int stagingTime = 5;
        [SerializeField] private float scoreTickTime = 1;
        [SerializeField] private bool skipStaging = false;
        [SerializeField] private bool invulnerable = true;

        private float stagingStartTime = 0;
        public float playingStartTime = 0;
        public float lastScoreTick = 0;

        Game()
        {
            instance = this;
        }

        public bool IsInvulnerable()
        {
            return invulnerable;
        }

        public void Quit()
        {
            SaveScores();
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

        public void IncreaseKills(int amount)
        {
            state.kills += amount;
        }

        public void IncreasePickup(int amount)
        {
            state.pickups += amount;
        }

        public void IncreaseScore(int amount)
        {
            state.score += amount;
        }

        public void IncreaseHealth(int amount)
        {
            state.health = Mathf.Min(state.health + amount, state.maxHealth);
        }

        public void SaveScores()
        {

        }

        public void GameEndCrash()
        {
            state.gameState = GameState.Crash;
        }

        // Start is called before the first frame update
        void Start()
        {
            preferences.Load();
            state.Reset();
            StartLevel();
        }

        public void EndLevel()
        {
            state.gameState = GameState.EndLevel;
            // FIXME: Kill off any badguys
        }

        void StartLevel()
        {
            state.gameState = GameState.Staging;
            stagingStartTime = Time.time;
            LoadLevel(state.level);
            if(skipStaging)
            {
                Play();
            }
        }

        void LoadLevel(int level)
        {
            if (!SceneManager.GetSceneByName(@"Level " + level).isLoaded)
            {
                SceneManager.LoadScene(@"Level " + level, LoadSceneMode.Additive);
            }
        }

        // Update is called once per frame
        void Update()
        {
            switch(state.gameState)
            {
                case GameState.Staging:
                    UpdateStaging();
                    break;
                case GameState.Playing:
                    UpdatePlaying();
                    break;
            }

            UpdateUI();
        }


        private void UpdatePlaying()
        {
            UpdateScoring();
        }

        private void UpdateScoring()
        {
            if(Time.time - lastScoreTick >= scoreTickTime)
            {
                IncreaseScore(1);
                lastScoreTick = Time.time;
            }
            
        }

        private void UpdateStaging()
        {
            if(Time.time - stagingStartTime >= stagingTime)
            {
                Play();
            }
        }

        private void UpdateUI()
        {
            hudPanel.Repaint();
            stagingPanel.Repaint();
            gameOverPanel.Repaint();
        }

        void Play()
        {
            state.gameState = GameState.Playing;
            playingStartTime = Time.time;
            lastScoreTick = Time.time;
        }




    }
}
