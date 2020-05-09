using System.Collections;
using System.Collections.Generic;
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
                gameObject.SetActive(true);
                scoreText.text = Game.instance.state.score.ToString();
                killText.text = Game.instance.state.kills.ToString();
                pickupText.text = Game.instance.state.pickups.ToString();
                //EventSystem.current.SetSelectedGameObject(code1Text.gameObject);

            }
            else
            {
                gameObject.SetActive(false);
                return;
            }
        }

        public void QuitClicked()
        {
            Game.instance.state.code = code1Text.text + code2Text.text + code3Text.text;
            Game.instance.Quit();
            //Save score

        }

        public void Code1Finished()
        {
            Logger.Log(name, "c1 Finished");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(code2Text.gameObject);
        }
        public void Code2Finished()
        {
            Logger.Log(name, "c2 Finished");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(code3Text.gameObject);
        }

        public void Code3Finished()
        {
            Logger.Log(name, "c3 Finished");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(quitButton.gameObject);
        }
    }
}
