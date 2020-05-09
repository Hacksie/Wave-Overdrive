using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HackedDesign
{
    public class Staging : MonoBehaviour
    {
        [Header("Referenced GameObjects")]
        [SerializeField] private TextMeshProUGUI label = null;
        [Header("Settings")]
        [SerializeField] private string[] messages = { "Ready?", "3", "2", "1", "Go!" };
        

        private int index = 0;
        private float startTime = 0;
        // Start is called before the first frame update
        public void Start()
        {
            startTime = Time.time;
        }

        public void Repaint()
        {
            if (Game.instance.state.gameState == GameState.Staging)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
                return;
            }
            label.text = messages[index];

            if(Time.time - startTime > 1)
            {
                index++;
                startTime = Time.time;
            }

            if (index >= messages.Length) index = 0;
        }
    }
}