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
        [SerializeField] private AudioSource pickupSound = null;

        [Header("Audio")]
        [SerializeField] private AudioSource countdown = null;
        [SerializeField] private AudioSource loop = null;

        [Header("UI")]
        [SerializeField] private Hud hudPanel = null;
        [SerializeField] private Staging stagingPanel = null;
        [SerializeField] private GameOverPresenter gameOverPanel = null;

        [Header("Settings")]
        [SerializeField] private int numberofLevels = 1;
        [SerializeField] private int stagingTime = 5;
        [SerializeField] private float scoreTickTime = 1;
        [SerializeField] private bool skipStaging = false;
        [SerializeField] private bool invulnerable = true;
        [SerializeField] private bool clearPreferencesOnLoad = false;


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
            //preferences.Save();
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            
        }
        public void PickupSound()
        {
            if (pickupSound != null)
            {
                pickupSound.Play();
            }
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

        public int IncreaseHealth(int amount)
        {
            state.health = Mathf.Min(state.health + amount, state.maxHealth);
            return state.health;
        }

        public int DecreaseHealth(int amount)
        {
            state.health = Mathf.Max(state.health - amount, 0);
            return state.health;
        }

        public void GameEndCrash()
        {
            state.gameState = GameState.Crash;
        }

        // Start is called before the first frame update
        void Start()
        {
            preferences.Load();
            if (clearPreferencesOnLoad)
            {
                preferences.ClearAll();
            }
            
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
            StartCoroutine(LoadLevel(state.level));

            if (skipStaging)
            {
                Play();
            }
        }

        IEnumerator LoadLevel(int level)
        {
            if (!SceneManager.GetSceneByName(@"Level " + level).isLoaded)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(@"Level " + level, LoadSceneMode.Additive);

                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                if (countdown != null)
                {
                    countdown.Play();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            switch (state.gameState)
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
            if (Time.time - lastScoreTick >= scoreTickTime)
            {
                IncreaseScore(1);
                lastScoreTick = Time.time;
            }

        }

        private void UpdateStaging()
        {
            if (Time.time - stagingStartTime >= stagingTime)
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
            if (countdown != null)
            {
                countdown.Stop();
            }

            if (loop != null)
            {
                loop.Play();
            }
        }




    }
}
