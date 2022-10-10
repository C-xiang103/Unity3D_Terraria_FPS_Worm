using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
        private float _horizontalRotateSensitivity = 1000f;
        private float _verticalRotateSensitivity = 600f;
        private float _maxDepressionAngle = 60f;
        private float _maxElevationAngle = 80f;
        private float _moveSpeed = 5f;
        private Rigidbody _rigidbody;

        private float _jumpPower = 8f;
        private float _jumpWaitTime = 1.4f;
        private float _actualJumpTime;

        private void Awake()
        {
            _playerMove = this;
        }
        private void Start()
        {
            SetCursorToCentre();
            _rigidbody = GetComponent<Rigidbody>();
            _actualJumpTime = _jumpWaitTime;
        }
        private void Update()
        {
            View();
            Jump();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void View()
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

        private void Move()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.forward * v * 0.02f * _moveSpeed);
            transform.Translate(Vector3.right * h * 0.02f * _moveSpeed);
        }

        private void Jump()
        {
            _actualJumpTime = _actualJumpTime > 0 ? _actualJumpTime - Time.deltaTime : _actualJumpTime;
            if (Input.GetKey(KeyCode.Space)&& _actualJumpTime<0f)
            {
                _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _actualJumpTime = _jumpWaitTime;
            }
        }

        private void SetCursorToCentre()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private bool OverViewRange(double targetAngle)
        {
            return targetAngle > _maxDepressionAngle && targetAngle < 360 - _maxElevationAngle;
        }
    }

}