using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

namespace HackedDesign
{
    public class MainMenu : MonoBehaviour
    {
        [Header("State")]
        [SerializeField] private MainMenuState state = MainMenuState.None;
        [SerializeField] private PlayerPreferences preferences = new PlayerPreferences();
        [Header("Referenced GameObjects")]
        [SerializeField] private GameObject scoresPanel = null;
        [SerializeField] private GameObject optionsPanel = null;
        [SerializeField] private GameObject creditsPanel = null;
        [SerializeField] private Dropdown resolutionsDropdown = null;
        [SerializeField] private Dropdown fullScreenDropdown = null;
        [SerializeField] private Toggle invertXToggle = null;
        [SerializeField] private Toggle invertYToggle = null;

        
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
            preferences.Load();
            PopulateOptionsValues();
            Refresh();
        }

        public void InvertYToggleClick()
        {
            preferences.invertY = invertYToggle.isOn;
            preferences.Save();
        }

        public void InvertXToggleClick()
        {
            preferences.invertX = invertXToggle.isOn;
            preferences.Save();
        }

        private void PopulateOptionsValues()
        {
            resolutionsDropdown.ClearOptions();
            resolutionsDropdown.AddOptions(Screen.resolutions.ToList().ConvertAll(r => new Dropdown.OptionData(r.width + "x" + r.height + " " + r.refreshRate + "Hz")));
            resolutionsDropdown.value = Screen.resolutions.ToList().IndexOf(Screen.currentResolution);

            fullScreenDropdown.ClearOptions();
            fullScreenDropdown.AddOptions(new List<Dropdown.OptionData>() { new Dropdown.OptionData("Exclusive Fullscreen"), new Dropdown.OptionData("Fullscreen Window"), new Dropdown.OptionData("Maximised Window"), new Dropdown.OptionData("Windowed")});
            resolutionsDropdown.value = (int)Screen.fullScreenMode;

            invertYToggle.isOn = preferences.invertY;
            invertXToggle.isOn = preferences.invertX;
            //fullScreenToggle.isOn = Screen.fullScreen;

            //masterMixer.GetFloat("FXVolume", out float fxVolume);
            //masterMixer.GetFloat("MusicVolume", out float musicVolume);
            //fxSlider.value = (fxVolume + 80) / 100;
            //musicSlider.value = (musicVolume + 80) / 100;
        }

        private void SaveOptions()
        {

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
