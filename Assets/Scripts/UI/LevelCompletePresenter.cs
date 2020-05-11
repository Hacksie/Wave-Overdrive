using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HackedDesign
{
    public class LevelCompletePresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText = null;
        [SerializeField] private TextMeshProUGUI killText = null;
        [SerializeField] private TextMeshProUGUI pickupText = null;
        [SerializeField] private Button quitButton = null;

        // Update is called once per frame
        public void Repaint()
        {
            if (Game.instance.state.gameState == GameState.EndLevel)
            {
                if (!gameObject.activeInHierarchy)
                {
                    gameObject.SetActive(true);
                    scoreText.text = Game.instance.state.score.ToString();
                    killText.text = Game.instance.state.kills.ToString();
                    pickupText.text = Game.instance.state.pickups.ToString();
                    EventSystem.current.SetSelectedGameObject(quitButton.gameObject);
                }
            }
            else
            {
                gameObject.SetActive(false);
                return;
            }
        }

        public void NextClicked()
        {
            Game.instance.state.level++;
            Game.instance.StartLevel();
        }
    }
}
