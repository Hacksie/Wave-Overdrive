using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Animator))]
    public class Explosion : MonoBehaviour
    {
        public bool small = false;
        public bool exploded = false;
        public float explodeStartTime = 0;
        public float explodeTimeOut = 2;
        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (exploded && ((Time.time - explodeStartTime) > explodeTimeOut))
            {
                exploded = false;
            }

            if(small)
            {
                animator.SetBool("small explode", exploded);
            }
            else
            {
                animator.SetBool("explode", exploded);
            }
            
        }

        public void Explode()
        {
            exploded = true;
            explodeStartTime = Time.time;
        }
    }
}
