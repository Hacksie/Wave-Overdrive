using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HackedDesign
{
    public class GameOverPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText = null;
        [SerializeField] private TextMeshProUGUI killText = null;
        [SerializeField] private TextMeshProUGUI pickupText = null;
        [SerializeField] private TextMeshProUGUI highScoreLabel = null;
        [SerializeField] private TMP_InputField code1Text = null;
        [SerializeField] private TMP_InputField code2Text = null;
        [SerializeField] private TMP_InputField code3Text = null;
        [SerializeField] private Button quitButton = null;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Repaint()
        {
            if (Game.instance.state.gameState == GameState.Crash)
            {
                if (!gameObject.activeInHierarchy)
                {
                    gameObject.SetActive(true);
                    scoreText.text = Game.instance.state.score.ToString();
                    killText.text = Game.instance.state.kills.ToString();
                    pickupText.text = Game.instance.state.pickups.ToString();

                    if (Game.instance.preferences.topScoreList.scoresList.Any(s => s.score < Game.instance.state.score))
                    {
                        Logger.Log(name, "New High Score!");
                        highScoreLabel.gameObject.SetActive(true);
                        code1Text.gameObject.SetActive(true);
                        //code2Text.gameObject.SetActive(true);
                        //code3Text.gameObject.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(code1Text.gameObject);
                        //code1Text.Select();
                    }
                    else
                    {
                        Logger.Log(name, "Fail");
                        highScoreLabel.gameObject.SetActive(false);
                        code1Text.gameObject.SetActive(false);
                        code2Text.gameObject.SetActive(false);
                        code3Text.gameObject.SetActive(false);
                        EventSystem.current.SetSelectedGameObject(quitButton.gameObject);
                        //quitButton.Select();
                    }



                }

            }
            else
            {
                gameObject.SetActive(false);
                return;
            }
        }

        public void QuitClicked()
        {
            Game.instance.state.code = code1Text.text; // + code2Text.text + code3Text.text;
            if (Game.instance.preferences.topScoreList.scoresList.Any(s => s.score < Game.instance.state.score))
            {
                Game.instance.preferences.topScoreList.scoresList.Add(new TopScore()
                {
                    code = code1Text.text,
                    kills = Game.instance.state.kills,
                    pickups = Game.instance.state.pickups,
                    score = Game.instance.state.score
                });
            }
            Game.instance.preferences.Save();
            Game.instance.Quit();
            //Save score

        }

        public void Code1Finished()
        {
            Logger.Log(name, "c1 Finished");

            //EventSystem.current.SetSelectedGameObject(null);
            //EventSystem.current.SetSelectedGameObject(code2Text.gameObject);
            //EventSystem.current.SetSelectedGameObject(quitButton.gameObject);
            quitButton.Select();
        }
        public void Code2Finished()
        {
            Logger.Log(name, "c2 Finished");
            //EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(code3Text.gameObject);
        }

        public void Code3Finished()
        {
            Logger.Log(name, "c3 Finished");
            //EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(quitButton.gameObject);
            quitButton.Select();
        }
    }
}
