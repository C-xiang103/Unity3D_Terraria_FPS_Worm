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
        private float _minAngleSpeed = 0.005f;
        private float _keepRunMinTime = 2f;
        private float _keepRunMaxTime = 8f;
        private Vector3 _headTarget = new Vector3(5.3f, 12.2f, 8.5f);//Ŀ���λ
        private float _maxRange = 2500f;//�������Զ�����ƽ��
        private bool _needChangeDirection = false;//��ǰ��Ҫת��
        private Transform _playerTransform;//���λ��

        public GameObject _nextBody { get; set; }//ͷ����һ������
        private int _headHp = 8;
        public bool BeShoot { get; set; }
        private const float AllChangeColor = 0.3f;//ɫ�ʱ仯����
        private float _changeColor;//ɫ�ʱ仯��λ��


        private void Start()
        {
            _playerTransform = PlayerMove.GetPlayerMove.transform;
            BeShoot = false;
            _changeColor = AllChangeColor / _headHp;
        }

        private void Update()
        {
            ReduceHp();
            RotateHead();
            RunHead();
            HasNewHeadMoveVariable();
            JustChangeDirection();
            _headTarget = _playerTransform.position;//����Ŀ��
        }
        private void OnTriggerEnter()
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
            if (_changeDirectionWaitTime >= 0)
            {
                _changeDirectionWaitTime -= Time.deltaTime;
                return;
            }
            _changeDirectionWaitTime = Random.Range(_keepRunMinTime, _keepRunMaxTime);
            _headSpeed = Random.Range(_minSpeed, _maxSpeed);
            _angleSpeed = Random.Range(_minAngleSpeed, _maxAngleSpeed);
            _runDirection = (_headTarget - transform.position).normalized;//��һ����ǰ����
            _runDirection = (_runDirection + new Vector3(0f, Random.Range(-0.8f, 0.8f), 0f)).normalized;//��y�����������ֵ���ٹ�һ��
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


        private void ReduceHp()//����ֵ����
        {
            if(BeShoot)//������
            {
                BeShoot = false;
                _headHp--;
                ChangeColor();
                if (_headHp<=0)
                {
                    Destroy(_nextBody);//������һ�����塣��һ�������λ�û��Զ�����ͷ
                    Destroy(gameObject);//����ͷ
                }
            }
            if(_nextBody==null)//��һ�����屻��Ҵݻ١�ͷ�Զ�����.��Ҵݻٵ�λ�û������µ�ͷ
            {
                Destroy(gameObject);
            }
        }
        private void ChangeColor()//���
        {
            MeshRenderer[] SonsColor = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < SonsColor.Length; i++)
                SonsColor[i].material.color += new Color(_changeColor, -_changeColor, -_changeColor);
        }
    }

}