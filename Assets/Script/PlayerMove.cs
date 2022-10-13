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
        private static Transform _playerTransform;
        public static Transform GetPlayerTransform => _playerTransform;

        [SerializeField] private GameObject PlayerView;//摄像机
        [SerializeField] private float _horizontalRotateSensitivity = 100f;//鼠标灵敏度
        [SerializeField] private float _verticalRotateSensitivity = 80f;
        private float _maxDepressionAngle = 60f;//俯仰限制
        private float _maxElevationAngle = 80f;
        private float _moveSpeed = 5f;
        private Rigidbody _rigidbody;

        private float _jumpPower = 8f;
        private float _jumpWaitTime = 1.4f;
        private float _actualJumpTime;//当前跳跃冷却时间

        [SerializeField] private Transform _miniMap;//小地图位置

        private void Awake()
        {
            _playerTransform = transform;
        }
        private void Start()
        {
            SetCursorToCentre();
            _rigidbody = GetComponent<Rigidbody>();
            _actualJumpTime = _jumpWaitTime;
            UpDateMiniMapPosition();
        }
        private void Update()
        {
            View();
            Jump();
            UpDateMiniMapPosition();
            Move();
        }

        private void View()
        {

            //当前垂直角度
            double verticalAngle = PlayerView.transform.eulerAngles.x;

            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y") * -1;

            //角色水平旋转
            transform.Rotate(Vector3.up * h * Time.deltaTime * _horizontalRotateSensitivity);

            //计算本次旋转后，竖直方向上的欧拉角
            double targetAngle = verticalAngle + v * Time.deltaTime * _verticalRotateSensitivity;

            //竖直方向视角限制
            if (OverViewRange(targetAngle)) return;

            //摄像机竖直方向上旋转
            PlayerView.transform.Rotate(Vector3.right * v * Time.deltaTime * _verticalRotateSensitivity);
        }

        private void Move()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.forward * v * Time.deltaTime * _moveSpeed);
            transform.Translate(Vector3.right * h * Time.deltaTime * _moveSpeed);
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

        private bool OverViewRange(double targetAngle)//俯仰视角限制
        {
            return targetAngle > _maxDepressionAngle && targetAngle < 360 - _maxElevationAngle;
        }

        private void UpDateMiniMapPosition()
        {
            _miniMap.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

}