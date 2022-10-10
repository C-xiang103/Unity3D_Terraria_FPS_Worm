using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BigBoss
{

    public class Head : MonoBehaviour
    {
        /// <summary>
        /// Boss头部
        /// </summary>
        private Vector3 _runDirection;//移动参数
        private float _changeDirectionWaitTime = 0f;//获取新移动变量倒计时
        private float _headSpeed = 5f;
        private float _angleSpeed = 0.1f;
        private float _minJudgeRotate = 0.1f;//旋转角度最小判定弧度
        private float _maxSpeed = 20f;
        private float _minSpeed = 8f;
        private float _maxAngleSpeed = 0.15f;
        private float _minAngleSpeed = 0.005f;
        private float _keepRunMinTime = 2f;
        private float _keepRunMaxTime = 8f;
        private Vector3 _headTarget = new Vector3(5.3f, 12.2f, 8.5f);//目标点位
        private float _maxRange = 2500f;//与玩家最远距离的平方
        private bool _needChangeDirection = false;//当前需要转向
        private Transform _playerTransform;//玩家位置

        public GameObject _nextBody { get; set; }//头的下一个身子
        private int _headHp = 8;
        public bool BeShoot { get; set; }
        private const float AllChangeColor = 0.3f;//色彩变化总量
        private float _changeColor;//色彩变化单位量


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
            _headTarget = _playerTransform.position;//更新目标
        }
        private void OnTriggerEnter()
        {
            _needChangeDirection = true;
        }

        private void RotateHead()//转弯
        {
            Quaternion rotate = Quaternion.LookRotation(_runDirection);
            if (Vector3.Angle(_runDirection, transform.forward) > _minJudgeRotate)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _angleSpeed);
        }

        private void RunHead()//移动
        {
            transform.position += transform.forward * Time.deltaTime * _headSpeed;
            //上升减速,下降加速,控制在设定速度直之间
            _headSpeed += _headSpeed > _minSpeed && _headSpeed < _maxSpeed ? (-1f) * transform.forward.y * 0.1f : 0f;
        }

        private void HasNewHeadMoveVariable()//获取新运动参数
        {
            if (_changeDirectionWaitTime >= 0)
            {
                _changeDirectionWaitTime -= Time.deltaTime;
                return;
            }
            _changeDirectionWaitTime = Random.Range(_keepRunMinTime, _keepRunMaxTime);
            _headSpeed = Random.Range(_minSpeed, _maxSpeed);
            _angleSpeed = Random.Range(_minAngleSpeed, _maxAngleSpeed);
            _runDirection = (_headTarget - transform.position).normalized;//归一化当前方向
            _runDirection = (_runDirection + new Vector3(0f, Random.Range(-0.8f, 0.8f), 0f)).normalized;//对y方向增加随机值，再归一化
        }

        private void JustChangeDirection()//只进行旋转
        {
            //超出范围或需要旋转时调用
            if ((transform.position - _headTarget).sqrMagnitude > _maxRange||_needChangeDirection)
            {
                _runDirection = (_headTarget - transform.position).normalized;
                _needChangeDirection = false;
            }
        }


        private void ReduceHp()//生命值降低
        {
            if(BeShoot)//被射中
            {
                BeShoot = false;
                _headHp--;
                ChangeColor();
                if (_headHp<=0)
                {
                    Destroy(_nextBody);//销毁下一个身体。下一个身体的位置会自动创建头
                    Destroy(gameObject);//销毁头
                }
            }
            if(_nextBody==null)//下一个身体被玩家摧毁。头自动销毁.玩家摧毁的位置会生成新的头
            {
                Destroy(gameObject);
            }
        }
        private void ChangeColor()//变红
        {
            MeshRenderer[] SonsColor = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < SonsColor.Length; i++)
                SonsColor[i].material.color += new Color(_changeColor, -_changeColor, -_changeColor);
        }
    }

}