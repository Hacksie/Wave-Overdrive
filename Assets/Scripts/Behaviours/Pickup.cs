using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private int score;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Logger.Log(name, "Collect!");
                Game.instance.IncreaseScore(score);
                Game.instance.IncreasePickup(1);
                gameObject.SetActive(false);
            }
        }
    }
}
