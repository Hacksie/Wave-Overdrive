using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class GunTurret : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float rotateSpeed = 100;



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.state.gameState == GameState.Playing)
            {
                //firing = true;
                transform.rotation = Quaternion.LookRotation(Game.instance.player.playerModel.transform.position - transform.position, Vector3.up);

                    
                    //Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Game.instance.player.transform.position, Vector3.up), Mathf.Deg2Rad * rotateSpeed * Time.deltaTime);

                //transform.forward = Game.instance.player.transform.position.normalized;
                //UpdateFiring();
            }
            
        }

        /*
        private void UpdateFiring()
        {
            if (firing && Time.time - lastFireTime >= fireRate)
            {
                var firingPoint = firingPoints[firingPointIndex];

                var bullet = Game.instance.pool.GetEnemyBullet();
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bullet.gameObject.SetActive(true);
                }

                bullet.Fire(firingPoint.position, firingPoint.forward, 0);
                lastFireTime = Time.time;
                firingPointIndex++;
                if (firingPointIndex >= firingPoints.Length)
                {
                    firingPointIndex = 0;
                }
            }
        }*/
    }
}
