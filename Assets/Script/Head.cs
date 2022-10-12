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
        private Vector3 _runDirection;//移动向量
        private float _changeDirectionWaitTime = 0f;//获取新移动变量倒计时
        private float _headSpeed = 5f;
        private float _angleSpeed = 0.1f;
        private float _minJudgeRotate = 0.1f;//旋转角度最小判定弧度
        private float _maxSpeed = 20f;
        private float _minSpeed = 8f;
        private float _maxAngleSpeed = 0.15f;
        private float _minAngleSpeed = 0.001f;
        private float _keepRunMinTime = 2f;
        private float _keepRunMaxTime = 8f;
        private Vector3 _headTarget = new Vector3(5.3f, 12.2f, 8.5f);//目标点位
        private float _maxRange = 2500f;//与玩家最远距离的平方
        private bool _needChangeDirection = false;//当前需要转向
        private Transform _playerTransform;//玩家位置

        public Body NextBody { get; set; }//头的下一个身子
        private const float ChangeColor = 0.05f;//色彩变化最小单位量

        public void Init()
        {
            
        }
        private void Start()
        {
            _playerTransform = PlayerMove.GetPlayerTransform;
        }

        private void Update()
        {
            RotateHead();//转
            RunHead();//动
            HasNewHeadMoveVariable();//更新所有运动
            JustChangeDirection();//只更新方向
            _headTarget = _playerTransform.position;//更新目标
        }

        private void OnTriggerStay()
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
            _runDirection = (_runDirection + new Vector3(0f, Random.Range(-20f, 20f)*Time.deltaTime, 0f)).normalized;//对y方向增加随机值，再归一化
            if (_changeDirectionWaitTime >= 0)
            {
                _changeDirectionWaitTime -= Time.deltaTime;
                return;
            }
            _changeDirectionWaitTime = Random.Range(_keepRunMinTime, _keepRunMaxTime);
            _headSpeed = Random.Range(_minSpeed, _maxSpeed);
            _angleSpeed = Random.Range(_minAngleSpeed, _maxAngleSpeed);
            _runDirection = (_headTarget - transform.position).normalized;//归一化当前方向
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


        public void NextBodyDestroy()//下一个身体被玩家摧毁
        {
            Destroy(gameObject);
        }

        public void NotifyDead()//生命值小于0，自身被摧毁
        {
            if(NextBody!=null)//告诉下个身体，它的头被摧毁
            {
                NextBody.HeadBeDestry();
            }
            Destroy(gameObject);//摧毁头
        }
    }

}