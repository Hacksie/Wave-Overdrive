using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace HackedDesign
{
    public class PlayerPreferences
    {
        public int resolutionWidth;
        public int resolutionHeight;
        public int resolutionRefresh;
        public int fullScreen;
        public bool invertX;
        public bool invertY;
        public float sfxVolume;
        public float musicVolume;
        public TopScoreList topScoreList;

        public PlayerPreferences()
        {
        }


        public void ClearAll()
        {
            PlayerPrefs.DeleteAll();
            Defaults();
        }

        public void Save()
        {
            PlayerPrefs.SetInt("ResolutionWidth", resolutionWidth);
            PlayerPrefs.SetInt("ResolutionHeight", resolutionHeight);
            PlayerPrefs.SetInt("ResolutionRefresh", resolutionRefresh);

            PlayerPrefs.SetInt("FullScreen", fullScreen);
            PlayerPrefs.SetInt("InvertX", invertX ? 1 : 0);
            PlayerPrefs.SetInt("InvertY", invertY ? 1 : 0);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);

            topScoreList.scoresList = topScoreList.scoresList.OrderByDescending(score => score.score).ThenByDescending(score => score.kills).ThenByDescending(score => score.pickups).Take(10).ToList();
            var scoresString = JsonUtility.ToJson(topScoreList);
            PlayerPrefs.SetString("Scores", scoresString);
        }

        public void Load()
        {
            resolutionWidth = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
            resolutionHeight = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);
            resolutionRefresh = PlayerPrefs.GetInt("ResolutionRefresh", Screen.currentResolution.refreshRate);
            fullScreen = PlayerPrefs.GetInt("FullScreen", (int)Screen.fullScreenMode);
            invertX = PlayerPrefs.GetInt("InvertX", 0) != 0 ? true : false;
            invertY = PlayerPrefs.GetInt("InvertY", 0) != 0 ? true : false;
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0);
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
            var scoresString = PlayerPrefs.GetString("Scores", "");
            if (!string.IsNullOrWhiteSpace(scoresString))
            {
                topScoreList = JsonUtility.FromJson<TopScoreList>(scoresString);
            }
        }

        public void Defaults()
        {
            fullScreen = (int)Screen.fullScreenMode;
            resolutionWidth = Screen.currentResolution.width;
            resolutionHeight = Screen.currentResolution.height;
            resolutionRefresh = Screen.currentResolution.refreshRate;
            invertX = false;
            invertY = false;
            sfxVolume = 0;
            musicVolume = 0;
            topScoreList = new TopScoreList();
            topScoreList.scoresList = new List<TopScore>()
                {
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 500
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 600
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 700
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 800
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 1000
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 1200
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 1400
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 1600
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 1800
                    },
                    new TopScore()
                    {
                        code = "Ben",
                        kills = 20,
                        pickups = 100,
                        score = 2000
                    }
                };

        }
    }

    [Serializable]
    public class TopScore
    {
        public string code;
        public int score;
        public int kills;
        public int pickups;
    }

    [Serializable]
    public class TopScoreList
    {
        public List<TopScore> scoresList;
    }


}
