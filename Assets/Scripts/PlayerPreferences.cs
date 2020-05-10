using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class PlayerPreferences
    {
        public int resolutionWidth;
        public int resolutionHeight;
        public int resolutionRefresh;
        public int resolutionHash;
        public int fullScreen;
        public bool invertX;
        public bool invertY;
        public float sfxVolume;
        public float musicVolume;
        public List<TopScore> scoresList = new List<TopScore>();
        

        public void Save()
        {
            //PlayerPrefs.SetInt("Resolution", resolutionHash);
            PlayerPrefs.SetInt("ResolutionWidth", resolutionWidth);
            PlayerPrefs.SetInt("ResolutionHeight", resolutionHeight);
            PlayerPrefs.SetInt("ResolutionRefresh", resolutionRefresh);
            
            PlayerPrefs.SetInt("FullScreen", fullScreen);
            PlayerPrefs.SetInt("InvertX", invertX ? 1 : 0);
            PlayerPrefs.SetInt("InvertY", invertY ? 1 : 0);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);

            var scoresString = JsonUtility.ToJson(scoresList);
            Logger.Log(scoresString);
            PlayerPrefs.SetString("Scores",scoresString);
        }

        public void Load()
        {
            //resolutionHash = PlayerPrefs.GetInt("Resolution", Screen.currentResolution.GetHashCode());
            resolutionWidth = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
            resolutionHeight = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);
            resolutionRefresh = PlayerPrefs.GetInt("ResolutionRefresh", Screen.currentResolution.refreshRate);
            fullScreen = PlayerPrefs.GetInt("FullScreen", (int)Screen.fullScreenMode);
            invertX = PlayerPrefs.GetInt("InvertX",0) != 0? true: false;
            invertY = PlayerPrefs.GetInt("InvertY", 0) != 0 ? true : false;
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0);
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
            var scoresString = PlayerPrefs.GetString("Scores", "");
            if(string.IsNullOrWhiteSpace(scoresString))
            {
                scoresList = new List<TopScore>();
            }
            {
                scoresList = JsonUtility.FromJson<List<TopScore>>(scoresString);
            }
            
        }

        public void Defaults()
        {
            resolutionHash = Screen.currentResolution.GetHashCode();
            fullScreen = (int)Screen.fullScreenMode;
            invertX = false;
            invertY = false;

            
        }
    }

    [System.Serializable]
    public class TopScoreList
    {
        public List<TopScore> scores;
    }

    [System.Serializable]
    public class TopScore
    {
        public string code;
        public int score;
        public int kills;
        public int pickups;
    }
}
