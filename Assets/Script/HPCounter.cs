using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script
{
    public class HPCounter : MonoBehaviour
    {
        [SerializeField] private int _hp = 3;
        [SerializeField] private UnityEvent _onDead;
        [SerializeField] private UnityEvent _onDamage;

        public void ReduceHP()
        {
            --_hp;
            _onDamage.Invoke();
            if(_hp <= 0)
            {
                _onDead.Invoke();
            }
        }
    }
}