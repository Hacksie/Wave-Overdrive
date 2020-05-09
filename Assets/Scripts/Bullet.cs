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
        [SerializeField] private float damage = 50;
        
        [SerializeField] private float timeOut = 5.0f;
        [Header("Referenced GameObjects")]
        [SerializeField] private TrailRenderer trailRenderer;

        private new Rigidbody rigidbody;

        [Header("State")]
        [SerializeField] public bool fired = false;
        private float startTimer = 0;

        
        
        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(fired)
            {
                if(Time.time - startTimer >= timeOut)
                {
                    
                    fired = false;
                    transform.position = Vector3.zero;
                    rigidbody.isKinematic = true;
                    rigidbody.velocity = Vector3.zero;
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
            transform.position = position;
            transform.forward = forward;
            rigidbody.isKinematic = false;
            rigidbody.velocity = transform.forward * (momentum + speed);
            trailRenderer.Clear();
        }
    }
}
