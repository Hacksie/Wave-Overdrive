using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 50.0f;
        [SerializeField] private int damage = 50;
        
        [SerializeField] private float timeOut = 5.0f;
        [Header("Referenced GameObjects")]
        [SerializeField] private TrailRenderer trailRenderer;

        private new Rigidbody rigidbody;

        [Header("State")]
        [SerializeField] public bool fired = false;
        private float startTimer = 0;

        
        public int GetDamageAmount()
        {
            return damage;
        }
        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(fired)
            {
                if(Time.time - startTimer >= timeOut)
                {
                    fired = false;

                    //rigidbody.isKinematic = true;
                    trailRenderer.Clear();
                    rigidbody.MovePosition(Vector3.zero);
                    trailRenderer.Clear();
                    rigidbody.Sleep();
                    
                }
            }
        }

        private void UpdatePosition()
        {
            
        }

        public void Fire(Vector3 position, Vector3 forward,float momentum)
        {
            Logger.Log(name, "Fire!");
            fired = true;
            startTimer = Time.time;
            trailRenderer.Clear();
            rigidbody.isKinematic = false;
            rigidbody.MovePosition(position);
            transform.forward = forward;
            trailRenderer.Clear();

            //rigidbody.AddForce(transform.forward * (20 * speed));

            rigidbody.velocity = transform.forward * (momentum + speed);

        }
    }
}
