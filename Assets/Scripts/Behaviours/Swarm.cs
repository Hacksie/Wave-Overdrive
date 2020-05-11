using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HackedDesign
{
    public class Swarm : MonoBehaviour
    {
        [SerializeField] private GameObject patrol;
        [SerializeField] private int speed = 2;
        [SerializeField] private bool setLoops = true;
        [SerializeField] private int loops = -1;
        [SerializeField] private LoopType loopType = LoopType.Yoyo;

        private Sequence patrolSequence;

        // Start is called before the first frame update
        void Start()
        {
            patrolSequence = DOTween.Sequence();
            for(int i=0;i<patrol.transform.childCount;i++)
            {
                var go = patrol.transform.GetChild(i);
                if (go != null)
                {
                    patrolSequence.Append(transform.DOMove(go.transform.position, speed));
                }
            }

            if (setLoops)
            {
                patrolSequence.SetLoops(loops, loopType);
            }
            patrolSequence.Pause();
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                patrolSequence.Play();
            }
        }
    }
}
