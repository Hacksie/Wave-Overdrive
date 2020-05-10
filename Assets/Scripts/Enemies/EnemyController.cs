using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int health = 50;
        [SerializeField] private float fireRate = 1f;

        [Header("Referenced GameObjects")]
        [SerializeField] private Transform[] firingPoints = null;

        [Header("State")]
        [SerializeField] private bool firing = false;
        [SerializeField] private float lastFireTime = 0;
        [SerializeField] private int firingPointIndex = 0;

        private float offset;
        // Start is called before the first frame update
        void Start()
        {
            offset = Random.Range(0.0f, 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
            firing = true;
            UpdateFiring();
        }

        private void UpdateFiring()
        {
            if (firing && (offset + Time.time - lastFireTime) >= fireRate)
            {
                //Logger.Log(name, "Firing!");
                var firingPoint = firingPoints[firingPointIndex];

                var bullet = Game.instance.pool.GetEnemyBullet();
                
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bullet.gameObject.SetActive(true);
                }
                
                bullet.Fire(firingPoint.position, firingPoint.forward, 0);
                
                lastFireTime = Time.time + offset;
                

                firingPointIndex++;
                if (firingPointIndex >= firingPoints.Length)
                {
                    firingPointIndex = 0;
                }
            }
        }

        public void Hit(Vector3 position, int amount)
        {
            health -= amount;
            Logger.Log(name, "is now at health: ", health.ToString());
            if (health <= 0)
            {
                Explode(position);
            }
            else
            {
                // Small Explosion
            }
        }

        

        public void Explode(Vector3 position)
        {
            Logger.Log(name, "Enemy controller explode");
            Explosion explosion = Game.instance.pool.GetBigExplosion();

            explosion.transform.position = position;
            explosion.Explode();
            gameObject.SetActive(false);
        }
    }
}
