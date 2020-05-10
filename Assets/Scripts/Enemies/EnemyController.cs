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
        [SerializeField] private float range = 100.0f;
        [SerializeField] private float zDistance = 25f;

        [Header("Referenced GameObjects")]
        [SerializeField] private Transform[] firingPoints = null;
        [SerializeField] private AudioSource fireAudioSource = null;

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

            if (Game.instance.player.transform.position.z <= (transform.position.z + zDistance) && Vector3.Distance(Game.instance.player.transform.position, transform.position) <= range)
            {
                firing = true;
                UpdateFiring();
            }
            else
            {
                firing = false;
            }



        }

        private void UpdateFiring()
        {
            if ((offset + Time.time - lastFireTime) >= fireRate)
            {
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

                fireAudioSource.Play();
            }
        }

        public void Hit(Vector3 position, int amount)
        {
            SmallExplode(position);
            health -= amount;
            
            if (health <= 0)
            {
                Explode(position);
            }

        }

        public void SmallExplode(Vector3 position)
        {
            Explosion explosion = Game.instance.pool.GetSmallExplosion();

            explosion.transform.position = position;
            explosion.Explode();
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
