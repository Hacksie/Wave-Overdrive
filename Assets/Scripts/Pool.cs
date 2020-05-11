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
        [SerializeField] private GameObject bulletPrefab = null;
        [SerializeField] private GameObject enemyBulletPrefab = null;
        [SerializeField] private GameObject bigExplosionPrefab = null;
        [SerializeField] private GameObject smallExplosionPrefab = null;
        [Header("Parents")]
        [SerializeField] private Transform playerBulletParent = null;
        [SerializeField] private Transform enemyBulletParent = null;
        [SerializeField] private Transform bigExplosionParent = null;
        [SerializeField] private Transform smallExplosionParent = null;
        [Header("State")]
        [SerializeField] private List<Bullet> enemyBullets = new List<Bullet>();
        [SerializeField] private List<Bullet> playerBullets = new List<Bullet>();
        [SerializeField] private List<Explosion> bigExplosions = new List<Explosion>();
        [SerializeField] private List<Explosion> smallExplosions = new List<Explosion>();
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
            var explosion = smallExplosions.FirstOrDefault(e => !e.exploded);

            if (!explosion)
            {
                var go = Instantiate(smallExplosionPrefab, smallExplosionParent);
                explosion = go.GetComponent<Explosion>();
                smallExplosions.Add(explosion);
            }

            return explosion;
        }
    }
}
