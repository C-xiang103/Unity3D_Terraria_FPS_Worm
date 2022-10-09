using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoss
{

    public class Division : MonoBehaviour
    {
        /// <summary>
        /// Boss∑÷¡—
        /// </summary>

        [SerializeField] private GameObject HeadPreform;
        // private GameObject[] CrateBody.AllBody;;
        private int _bodyMaxLength;
        private float _waitTime;


        private void Start()
        {
            _waitTime = 2f;
            _bodyMaxLength = CrateBody.AllBody.Length;
        }

        private void Update()
        {
            DoDivision();
        }

        private void DoDivision()
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime < 0 && _bodyMaxLength > 0)
            {
                _waitTime = Random.Range(1f, 5f);
                int i = Random.Range(1, _bodyMaxLength);
                GameObject newHead = Instantiate(HeadPreform, CrateBody.AllBody[i].transform.position, CrateBody.AllBody[i].transform.rotation);
                CrateBody.AllBody[i].GetComponent<MoveBody>().PreviousBody = newHead;
                _bodyMaxLength--;
                CrateBody.AllBody[i] = CrateBody.AllBody[_bodyMaxLength];
            }
        }
    }

}