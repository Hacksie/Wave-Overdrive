using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HackedDesign
{
    public class MainMenu : MonoBehaviour
    {
        [Header("State")]
        [SerializeField] private MainMenuState state = MainMenuState.None;
        [Header("Referenced GameObjects")]
        [SerializeField] private GameObject scoresPanel = null;
        [SerializeField] private GameObject optionsPanel = null;
        [SerializeField] private GameObject creditsPanel = null;


        public void NewGameClick()
        {
            SceneManager.LoadScene("Game");
        }
        public void TopScoresClick()
        {
            state = MainMenuState.TopScores;
            Refresh();
        }
        public void OptionsClick()
        {
            state = MainMenuState.Options;
            Refresh();
        }
        public void CreditsClick()
        {
            state = MainMenuState.Credits;
            Refresh();
        }

        public void QuitGameClick()
        {
            Application.Quit();
        }

        void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            switch (state)
            {
                case MainMenuState.None:
                    scoresPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    break;
                case MainMenuState.NewGame:
                    scoresPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    break;
                case MainMenuState.TopScores:
                    scoresPanel.SetActive(true);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    break;
                case MainMenuState.Options:
                    scoresPanel.SetActive(false);
                    optionsPanel.SetActive(true);
                    creditsPanel.SetActive(false);
                    break;
                case MainMenuState.Credits:
                    scoresPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(true);
                    break;
                case MainMenuState.Quit:
                    scoresPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    break;
            }
        }
    }

    public enum MainMenuState
    {
        None,
        NewGame,
        TopScores,
        Options,
        Credits,
        Quit
    }
}
