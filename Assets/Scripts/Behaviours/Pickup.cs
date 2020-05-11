using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Collider))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private int score = 0;
        [SerializeField] private int health = 0;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Logger.Log(name, "Collect!");
                Game.instance.IncreaseScore(score);
                Game.instance.IncreaseHealth(health);
                Game.instance.IncreasePickup(1);
                gameObject.SetActive(false);

                Game.instance.PickupSound();

            }
        }
    }
}
