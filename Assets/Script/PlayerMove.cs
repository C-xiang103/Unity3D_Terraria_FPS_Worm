using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoss
{

    public class PlayerMove : MonoBehaviour
    {
        /// <summary>
        /// ����ƶ����ӽ�
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

            //��ǰ��ֱ�Ƕ�
            double verticalAngle = PlayerView.transform.eulerAngles.x;

            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y") * -1;

            //��ɫˮƽ��ת
            transform.Rotate(Vector3.up * h * Time.deltaTime * _angularSpeed * _horizontalRotateSensitivity);

            //���㱾����ת����ֱ�����ϵ�ŷ����
            double targetAngle = verticalAngle + v * Time.deltaTime * _angularSpeed * _verticalRotateSensitivity;

            //��ֱ�����ӽ�����
            if (OverViewRange(targetAngle)) return;

            //�������ֱ��������ת
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