using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace BigBoss
{
    public class Hitable : MonoBehaviour, IHitable
    {
        [SerializeField] private UnityEvent _onHit;
        public void Hit()
        {
            _onHit.Invoke();


        }
    }
}   