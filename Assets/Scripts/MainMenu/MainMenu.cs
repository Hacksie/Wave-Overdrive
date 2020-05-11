using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;
using TMPro;

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
        [SerializeField] private Slider sfxSlider = null;
        [SerializeField] private Slider musicSlider = null;
        [SerializeField] private AudioMixer audioMixer = null;
        [SerializeField] private List<Text> codeTextList = null;
        [SerializeField] private List<Text> scoreTextList = null;
        [SerializeField] private List<Text> killTextList = null;
        [SerializeField] private List<Text> pickupTextList = null;


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
            SetPreferences();
            PopulateOptionsValues();
            PopulateScoreValues();
            Refresh();
        }

        public void SetPreferences()
        {
            audioMixer.SetFloat("SFXVolume", preferences.sfxVolume);
            audioMixer.SetFloat("MusicVolume", preferences.musicVolume);

            Resolution scr = Screen.resolutions.FirstOrDefault(r=>r.width == preferences.resolutionWidth && r.height == preferences.resolutionHeight && r.refreshRate == preferences.resolutionRefresh);

            Logger.Log(name, scr.width.ToString(), " x ", scr.height.ToString());
            Screen.SetResolution(scr.width,scr.height, (FullScreenMode)preferences.fullScreen, scr.refreshRate);
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

        public void ResolutionsDropdownValueChanged()
        {
            //preferences.resolutionHash = Screen.resolutions[resolutionsDropdown.value].GetHashCode();
            var res = Screen.resolutions[resolutionsDropdown.value];
            preferences.resolutionWidth = res.width;
            preferences.resolutionHeight = res.height;
            preferences.resolutionRefresh = res.refreshRate;
            preferences.Save();

        }

        public void FullScreenDropdownValueChanged()
        {
            preferences.fullScreen = fullScreenDropdown.value;
            preferences.Save();
        }

        public void SFXVolumeChanged()
        {
            audioMixer.SetFloat("SFXVolume", sfxSlider.value);
            preferences.sfxVolume = sfxSlider.value;
            preferences.Save();
        }

        public void MusicVolumeChanged()
        {
            audioMixer.SetFloat("MusicVolume", musicSlider.value);
            preferences.musicVolume = musicSlider.value;
            preferences.Save();
        }

        private void PopulateOptionsValues()
        {
            resolutionsDropdown.ClearOptions();
            resolutionsDropdown.AddOptions(Screen.resolutions.ToList().ConvertAll(r => new Dropdown.OptionData(r.width + "x" + r.height + " " + r.refreshRate + "Hz")));
            resolutionsDropdown.value = Screen.resolutions.ToList().IndexOf(Screen.currentResolution);

            fullScreenDropdown.ClearOptions();
            fullScreenDropdown.AddOptions(new List<Dropdown.OptionData>() { new Dropdown.OptionData("Exclusive Fullscreen"), new Dropdown.OptionData("Fullscreen Window"), new Dropdown.OptionData("Maximised Window"), new Dropdown.OptionData("Windowed")});
            fullScreenDropdown.value = (int)Screen.fullScreenMode;

            invertYToggle.isOn = preferences.invertY;
            invertXToggle.isOn = preferences.invertX;
            

            sfxSlider.value = preferences.sfxVolume;
            musicSlider.value = preferences.musicVolume;
        }

        private void PopulateScoreValues()
        {
            var sortedList = preferences.topScoreList.scoresList.OrderByDescending(score => score.score).ThenByDescending(score=>score.kills).ThenByDescending(score=>score.pickups).ToList();

            for(int i=0; i < sortedList.Count();i++)
            {
                if(codeTextList.Count > i)
                {
                    codeTextList[i].text = sortedList[i].code;
                }

                if (scoreTextList.Count > i)
                {
                    scoreTextList[i].text = sortedList[i].score.ToString();
                }

                if(killTextList.Count > i)
                {
                    killTextList[i].text = sortedList[i].kills.ToString();
                }

                if (pickupTextList.Count > i)
                {
                    pickupTextList[i].text = sortedList[i].pickups.ToString();
                }
            }
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
