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
        [SerializeField] private GameObject explosionPrefab;
        [Header("Parents")]
        [SerializeField] private Transform playerBulletParent;
        [SerializeField] private Transform enemyBulletParent;
        [SerializeField] private Transform explosionParent;
        [Header("State")]
        [SerializeField] private List<Bullet> enemyBullets;
        [SerializeField] private List<Bullet> playerBullets;
        [SerializeField] private List<Explosion> explosions;
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

        public Explosion GetExplosion()
        {
            var explosion = explosions.FirstOrDefault(e => !e.exploded);
            
            if(!explosion)
            {
                var go = Instantiate(explosionPrefab, explosionParent);
                explosion = go.GetComponent<Explosion>();
                explosions.Add(explosion);
            }

            return explosion;
        }
    }
}
