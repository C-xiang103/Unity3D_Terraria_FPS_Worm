using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoss
{

    public class MoveBody : MonoBehaviour
    {
        /// <summary>
        /// Boss…ÌÃÂ∏˙ÀÊ“∆∂Ø
        /// </summary>
        private Queue<Vector3> _historyPoints = new Queue<Vector3>();
        private Queue<Quaternion> _historyRotas = new Queue<Quaternion>();
        [NonSerialized]
        public GameObject PreviousBody;
        private float _twoBodyDistance;
        private Transform _transform;

        private void Start()
        {
            _twoBodyDistance = 2.7f;
            _transform = transform;
        }

        private void FixedUpdate()
        {
            MoveBodyByPrevious();
        }

        private void MoveBodyByPrevious()
        {
            _historyPoints.Enqueue(PreviousBody.transform.position);
            _historyRotas.Enqueue(PreviousBody.transform.rotation);

            Vector3 NextPoint = _transform.position;
            Quaternion NextRota = _transform.rotation;

            while (NeedUpDatePoint(NextPoint))
            {
                NextPoint = _historyPoints.Dequeue();
                NextRota = _historyRotas.Dequeue();
            }

            _transform.position = NextPoint;
            _transform.rotation = NextRota;
        }

        bool NeedUpDatePoint(Vector3 NextPoint)
        {
            return (PreviousBody.transform.position - NextPoint).sqrMagnitude > _twoBodyDistance && _historyPoints.Count > 0 && _historyRotas.Count > 0;
        }
    }

}