using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HackedDesign
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText = null;
        [SerializeField] private TextMeshProUGUI levelText = null;
        [SerializeField] private TextMeshProUGUI speedText = null;

        
        public void Repaint()
        {
            scoreText.text = Game.instance.state.score.ToString();
            levelText.text = Game.instance.state.level.ToString();
            speedText.text = Game.instance.player.currentSpeed.ToString("N1");
        }
    }
}
