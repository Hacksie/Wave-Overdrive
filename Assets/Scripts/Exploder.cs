using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HackedDesign
{
    [RequireComponent(typeof(Collider))]
    public class Exploder : MonoBehaviour
    {
        [SerializeField] private UnityEvent explosionEvent;

        private Explosion explosion;
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
            Logger.Log(name, "Collision!");
            //explosion = Game.instance.pool.GetExplosion();
            //explosion.transform.position = transform.position;
            //explosion.Explode();
            //explosionEvent.Invoke();           
        }
    }
}
