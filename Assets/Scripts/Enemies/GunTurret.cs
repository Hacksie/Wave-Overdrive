using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class GunTurret : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool predictive = false;

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.state.gameState == GameState.Playing)
            {
                Vector3 position = Game.instance.player.playerModel.transform.position + (predictive ? (Game.instance.player.playerModel.transform.forward.normalized * Game.instance.player.movementSpeed) : Vector3.zero);

                transform.rotation = Quaternion.LookRotation(position  - transform.position, Vector3.up);
            }
            
        }
    }
}
