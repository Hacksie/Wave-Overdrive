using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HackedDesign
{
    [RequireComponent(typeof(Collider))]
    public class Exploder : MonoBehaviour
    {
        [SerializeField] private bool ignoreObstacles = false;
        [SerializeField] private bool isBig = true;
        [SerializeField] private UnityEvent<Vector3> explosionEvent;
        [SerializeField] private UnityEvent<Vector3, int> hitEvent;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            // Just blow up
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Logger.Log(name, "Obstacle collision");
                explosionEvent.Invoke(collision.GetContact(0).point);
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Logger.Log(name, "Enemy collision");
                
                explosionEvent.Invoke(collision.GetContact(0).point);
            }

            if(collision.gameObject.CompareTag("Bullet"))
            {
                Logger.Log(name, "Bullet hit");
                //FIXME: Test for health
                Bullet b = collision.gameObject.GetComponent<Bullet>();

                if(b == null)
                {
                    Logger.LogError(name, "Bullet with no Bullet component!");
                    return;
                }

                hitEvent.Invoke(collision.GetContact(0).point, b.GetDamageAmount());
            }

        }
    }
}
