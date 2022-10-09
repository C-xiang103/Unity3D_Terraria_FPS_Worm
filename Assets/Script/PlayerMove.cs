using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoss
{

    public class PlayerMove : MonoBehaviour
    {
        /// <summary>
        /// 玩家移动及视角
        /// </summary>
        private static PlayerMove _playerMove;
        public static PlayerMove GetPlayerMove => _playerMove;
        [SerializeField] private GameObject PlayerView;
        private float _angularSpeed = 1f;
        private float _horizontalRotateSensitivity = 150f;
        private float _verticalRotateSensitivity = 200f;
        private float _maxDepressionAngle = 60f;
        private float _maxElevationAngle = 80f;
        private float _moveSpeed = 5f;

        private void Start()
        {
            _playerMove = this;
            SetCursorToCentre();
        }
        private void Update()
        {
            View();
        }

        private void FixedUpdate()
        {
            Move();
        }

        void View()
        {

            //当前垂直角度
            double verticalAngle = PlayerView.transform.eulerAngles.x;

            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y") * -1;

            //角色水平旋转
            transform.Rotate(Vector3.up * h * Time.deltaTime * _angularSpeed * _horizontalRotateSensitivity);

            //计算本次旋转后，竖直方向上的欧拉角
            double targetAngle = verticalAngle + v * Time.deltaTime * _angularSpeed * _verticalRotateSensitivity;

            //竖直方向视角限制
            if (OverViewRange(targetAngle)) return;

            //摄像机竖直方向上旋转
            PlayerView.transform.Rotate(Vector3.right * v * Time.deltaTime * _angularSpeed * _verticalRotateSensitivity);
        }

        void Move()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.forward * v * 0.02f * _moveSpeed);
            transform.Translate(Vector3.right * h * 0.02f * _moveSpeed);
        }

        void SetCursorToCentre()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        bool OverViewRange(double targetAngle)
        {
            return targetAngle > _maxDepressionAngle && targetAngle < 360 - _maxElevationAngle;
        }
    }

}