using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject playerView;
    private float angularSpeed;
    private float horizontalRotateSensitivity;
    private float verticalRotateSensitivity;
    private float maxDepressionAngle;
    private float maxElevationAngle;

    private void Start()
    {
        SetCursorToCentre();
        angularSpeed = 1f;
        horizontalRotateSensitivity = 100f;
        verticalRotateSensitivity = 100f;
        maxDepressionAngle = 30f;
        maxElevationAngle = 80f;
    }
    private void FixedUpdate()
    {
        View();
    }

    void View()
    {

        //��ǰ��ֱ�Ƕ�
        double VerticalAngle = playerView.transform.eulerAngles.x;

        //ͨ������ȡ��ֱ��ˮƽ���ֵ����Χ��-1��1
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y") * -1;

        //��ɫˮƽ��ת
        transform.Rotate(Vector3.up * h * Time.deltaTime * angularSpeed * horizontalRotateSensitivity);

        //���㱾����ת����ֱ�����ϵ�ŷ����
        double targetAngle = VerticalAngle + v * Time.deltaTime * angularSpeed * verticalRotateSensitivity;

        //��ֱ�����ӽ�����
        if (targetAngle > maxDepressionAngle && targetAngle < 360 - maxElevationAngle) return;

        //�������ֱ��������ת
        playerView.transform.Rotate(Vector3.right * v * Time.deltaTime * angularSpeed * verticalRotateSensitivity);
    }

    void SetCursorToCentre()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
