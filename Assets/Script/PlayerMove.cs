using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// ����ƶ����ӽ�
    /// </summary>
    public GameObject playerView;
    private float _angularSpeed;
    private float _horizontalRotateSensitivity;
    private float _verticalRotateSensitivity;
    private float _maxDepressionAngle;
    private float _maxElevationAngle;
    private float _moveSpeed;

    private void Start()
    {
        SetCursorToCentre();
        _angularSpeed = 1f;
        _horizontalRotateSensitivity = 100f;
        _verticalRotateSensitivity = 100f;
        _maxDepressionAngle = 60f;
        _maxElevationAngle = 80f;
        _moveSpeed = 5f;
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
        double VerticalAngle = playerView.transform.eulerAngles.x;

        //ͨ������ȡ��ֱ��ˮƽ���ֵ����Χ��-1��1
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y") * -1;

        //��ɫˮƽ��ת
        transform.Rotate(Vector3.up * h * Time.deltaTime * _angularSpeed * _horizontalRotateSensitivity);

        //���㱾����ת����ֱ�����ϵ�ŷ����
        double targetAngle = VerticalAngle + v * Time.deltaTime * _angularSpeed * _verticalRotateSensitivity;

        //��ֱ�����ӽ�����
        if (targetAngle > _maxDepressionAngle && targetAngle < 360 - _maxElevationAngle) return;

        //�������ֱ��������ת
        playerView.transform.Rotate(Vector3.right * v * Time.deltaTime * _angularSpeed * _verticalRotateSensitivity);
    }

    void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //����ʸ���ƶ�һ�ξ���
        transform.Translate(Vector3.forward * v * 0.02f * _moveSpeed);
        transform.Translate(Vector3.right * h * 0.02f * _moveSpeed);
    }

    void SetCursorToCentre()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
