using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BigBoss
{

    public class MoveHead : MonoBehaviour
    {
        /// <summary>
        /// BossÍ·²¿ÒÆ¶¯
        /// </summary>
        [SerializeField]
        private Vector3 _runDirection;
        private float _changeDirectionWaitTime = 0f;
        private float _headSpeed = 5f;
        private float _angleSpeed = 0.1f;
        private float _minJudgeRotate = 0.1f;
        private float _maxSpeed = 15f;
        private float _minSpeed = 5f;
        private float _maxAngleSpeed = 0.2f;
        private float _minAngleSpeed = 0.01f;
        private float _keepRunMinTime = 2f;
        private float _keepRunMaxTime = 8f;
        private Vector3 _headTarget = new Vector3(5.3f, 12.2f, 8.5f);
        private float _maxRange = 2500f;
        private Transform _playerTransform;

        private void Start()
        {
            _playerTransform = PlayerMove.GetPlayerMove.transform;
        }

        private void Update()
        {
            RotateHead();
            RunHead();
            HasNewHeadMoveVariable();
            OverRange();
            _headTarget = _playerTransform.position;
        }

        private void RotateHead()
        {
            Quaternion rotate = Quaternion.LookRotation(_runDirection);
            if (Vector3.Angle(_runDirection, transform.forward) > _minJudgeRotate)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _angleSpeed);
        }

        private void RunHead()
        {
            transform.position += transform.forward * Time.deltaTime * _headSpeed;
            _headSpeed += _headSpeed > _minSpeed ? (-1f) * transform.forward.y * 0.1f : 0f;
        }

        private void HasNewHeadMoveVariable()
        {
            if (_changeDirectionWaitTime >= 0)
            {
                _changeDirectionWaitTime -= Time.deltaTime;
                return;
            }
            _changeDirectionWaitTime = Random.Range(_keepRunMinTime, _keepRunMaxTime);
            _headSpeed = Random.Range(_minSpeed, _maxSpeed);
            _angleSpeed = Random.Range(_minAngleSpeed, _maxAngleSpeed);
            _runDirection = (_headTarget - transform.position).normalized;
            _runDirection = (_runDirection + new Vector3(0f, Random.Range(-0.8f, 0.8f), 0f)).normalized;
        }

        private void OverRange()
        {
            if ((transform.position - _headTarget).sqrMagnitude > _maxRange)
            {
                _runDirection = (_headTarget - transform.position).normalized;
            }
        }

        private void OnTriggerEnter()
        {
            _runDirection = (_headTarget - transform.position).normalized;
            _changeDirectionWaitTime = (_keepRunMaxTime + _keepRunMinTime) / 2;
        }
    }

}