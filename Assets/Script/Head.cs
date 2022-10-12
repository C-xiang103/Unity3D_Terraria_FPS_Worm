using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BigBoss
{

    public class Head : MonoBehaviour
    {
        /// <summary>
        /// Bossͷ��
        /// </summary>
        private Vector3 _runDirection;//�ƶ�����
        private float _changeDirectionWaitTime = 0f;//��ȡ���ƶ���������ʱ
        private float _headSpeed = 5f;
        private float _angleSpeed = 0.1f;
        private float _minJudgeRotate = 0.1f;//��ת�Ƕ���С�ж�����
        private float _maxSpeed = 20f;
        private float _minSpeed = 8f;
        private float _maxAngleSpeed = 0.15f;
        private float _minAngleSpeed = 0.001f;
        private float _keepRunMinTime = 2f;
        private float _keepRunMaxTime = 8f;
        private Vector3 _headTarget = new Vector3(5.3f, 12.2f, 8.5f);//Ŀ���λ
        private float _maxRange = 2500f;//�������Զ�����ƽ��
        private bool _needChangeDirection = false;//��ǰ��Ҫת��
        private Transform _playerTransform;//���λ��

        public Body NextBody { get; set; }//ͷ����һ������
        private const float ChangeColor = 0.05f;//ɫ�ʱ仯��С��λ��

        public void Init()
        {
            
        }
        private void Start()
        {
            _playerTransform = PlayerMove.GetPlayerTransform;
        }

        private void Update()
        {
            RotateHead();//ת
            RunHead();//��
            HasNewHeadMoveVariable();//���������˶�
            JustChangeDirection();//ֻ���·���
            _headTarget = _playerTransform.position;//����Ŀ��
        }

        private void OnTriggerStay()
        {
            _needChangeDirection = true;
        }

        private void RotateHead()//ת��
        {
            Quaternion rotate = Quaternion.LookRotation(_runDirection);
            if (Vector3.Angle(_runDirection, transform.forward) > _minJudgeRotate)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _angleSpeed);
        }

        private void RunHead()//�ƶ�
        {
            transform.position += transform.forward * Time.deltaTime * _headSpeed;
            //��������,�½�����,�������趨�ٶ�ֱ֮��
            _headSpeed += _headSpeed > _minSpeed && _headSpeed < _maxSpeed ? (-1f) * transform.forward.y * 0.1f : 0f;
        }

        private void HasNewHeadMoveVariable()//��ȡ���˶�����
        {
            _runDirection = (_runDirection + new Vector3(0f, Random.Range(-20f, 20f)*Time.deltaTime, 0f)).normalized;//��y�����������ֵ���ٹ�һ��
            if (_changeDirectionWaitTime >= 0)
            {
                _changeDirectionWaitTime -= Time.deltaTime;
                return;
            }
            _changeDirectionWaitTime = Random.Range(_keepRunMinTime, _keepRunMaxTime);
            _headSpeed = Random.Range(_minSpeed, _maxSpeed);
            _angleSpeed = Random.Range(_minAngleSpeed, _maxAngleSpeed);
            _runDirection = (_headTarget - transform.position).normalized;//��һ����ǰ����
        }

        private void JustChangeDirection()//ֻ������ת
        {
            //������Χ����Ҫ��תʱ����
            if ((transform.position - _headTarget).sqrMagnitude > _maxRange||_needChangeDirection)
            {
                _runDirection = (_headTarget - transform.position).normalized;
                _needChangeDirection = false;
            }
        }


        public void NextBodyDestroy()//��һ�����屻��Ҵݻ�
        {
            Destroy(gameObject);
        }

        public void NotifyDead()//����ֵС��0�������ݻ�
        {
            if(NextBody!=null)//�����¸����壬����ͷ���ݻ�
            {
                NextBody.HeadBeDestry();
            }
            Destroy(gameObject);//�ݻ�ͷ
        }
    }

}