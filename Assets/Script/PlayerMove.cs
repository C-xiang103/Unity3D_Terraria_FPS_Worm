using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace BigBoss
{

    public class PlayerMove : MonoBehaviour
    {
        /// <summary>
        /// ����ƶ����ӽ�
        /// </summary>
        private static Transform _playerTransform;
        public static Transform GetPlayerTransform => _playerTransform;

        [SerializeField] private GameObject PlayerView;//�����
        [SerializeField] private float _horizontalRotateSensitivity = 100f;//���������
        [SerializeField] private float _verticalRotateSensitivity = 80f;
        private float _maxDepressionAngle = 60f;//��������
        private float _maxElevationAngle = 80f;
        private float _moveSpeed = 5f;
        private Rigidbody _rigidbody;

        private float _jumpPower = 8f;
        private float _jumpWaitTime = 1.4f;
        private float _actualJumpTime;//��ǰ��Ծ��ȴʱ��

        [SerializeField] private Transform _miniMap;//С��ͼλ��

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

            //��ǰ��ֱ�Ƕ�
            double verticalAngle = PlayerView.transform.eulerAngles.x;

            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y") * -1;

            //��ɫˮƽ��ת
            transform.Rotate(Vector3.up * h * Time.deltaTime * _horizontalRotateSensitivity);

            //���㱾����ת����ֱ�����ϵ�ŷ����
            double targetAngle = verticalAngle + v * Time.deltaTime * _verticalRotateSensitivity;

            //��ֱ�����ӽ�����
            if (OverViewRange(targetAngle)) return;

            //�������ֱ��������ת
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

        private bool OverViewRange(double targetAngle)//�����ӽ�����
        {
            return targetAngle > _maxDepressionAngle && targetAngle < 360 - _maxElevationAngle;
        }

        private void UpDateMiniMapPosition()
        {
            _miniMap.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

}