using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class PlayerPreferences
    {
        public int resolutionHash;
        public int fullScreen;
        public bool invertX;
        public bool invertY;
        public float sfxVolume;
        public float musicVolume;
        public TopScoreList scoresList;
        

        public void Save()
        {
            PlayerPrefs.SetInt("Resolution", resolutionHash);
            PlayerPrefs.SetInt("FullScreen", fullScreen);
            PlayerPrefs.SetInt("InvertX", invertX ? 1 : 0);
            PlayerPrefs.SetInt("InvertY", invertY ? 1 : 0);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            
        }

        public void Load()
        {
            resolutionHash = PlayerPrefs.GetInt("Resolution", Screen.currentResolution.GetHashCode());
            fullScreen = PlayerPrefs.GetInt("FullScreen", (int)Screen.fullScreenMode);
            invertX = PlayerPrefs.GetInt("InvertX",0) != 0? true: false;
            invertY = PlayerPrefs.GetInt("InvertY", 0) != 0 ? true : false;
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0);
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
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
