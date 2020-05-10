using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace HackedDesign
{
    public class Pool : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject enemyBulletPrefab;
        [SerializeField] private GameObject bigExplosionPrefab;
        [Header("Parents")]
        [SerializeField] private Transform playerBulletParent;
        [SerializeField] private Transform enemyBulletParent;
        [SerializeField] private Transform bigExplosionParent;
        [Header("State")]
        [SerializeField] private List<Bullet> enemyBullets;
        [SerializeField] private List<Bullet> playerBullets;
        [SerializeField] private List<Explosion> bigExplosions;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Bullet GetEnemyBullet()
        {
            var bullet = enemyBullets.FirstOrDefault(e => !e.fired);

            if (!bullet)
            {
                var go = Instantiate(enemyBulletPrefab, enemyBulletParent);
                bullet = go.GetComponent<Bullet>();
                enemyBullets.Add(bullet);
            }

            return bullet;
        }

        public Bullet GetPlayerBullet()
        {
            var bullet = playerBullets.FirstOrDefault(e => !e.fired);

            if(!bullet)
            {
                var go = Instantiate(bulletPrefab, playerBulletParent);
                bullet = go.GetComponent<Bullet>();
                playerBullets.Add(bullet);
            }

            return bullet;
        }

        public Explosion GetBigExplosion()
        {
            var explosion = bigExplosions.FirstOrDefault(e => !e.exploded);
            
            if(!explosion)
            {
                var go = Instantiate(bigExplosionPrefab, bigExplosionParent);
                explosion = go.GetComponent<Explosion>();
                bigExplosions.Add(explosion);
            }

            return explosion;
        }

        public Explosion GetSmallExplosion()
        {
            var explosion = bigExplosions.FirstOrDefault(e => !e.exploded);

            if (!explosion)
            {
                var go = Instantiate(bigExplosionPrefab, bigExplosionParent);
                explosion = go.GetComponent<Explosion>();
                bigExplosions.Add(explosion);
            }

            return explosion;
        }
    }
}
