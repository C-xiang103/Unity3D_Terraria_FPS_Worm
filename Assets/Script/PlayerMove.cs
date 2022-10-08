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

        //当前垂直角度
        double VerticalAngle = playerView.transform.eulerAngles.x;

        //通过鼠标获取竖直、水平轴的值，范围在-1到1
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y") * -1;

        //角色水平旋转
        transform.Rotate(Vector3.up * h * Time.deltaTime * angularSpeed * horizontalRotateSensitivity);

        //计算本次旋转后，竖直方向上的欧拉角
        double targetAngle = VerticalAngle + v * Time.deltaTime * angularSpeed * verticalRotateSensitivity;

        //竖直方向视角限制
        if (targetAngle > maxDepressionAngle && targetAngle < 360 - maxElevationAngle) return;

        //摄像机竖直方向上旋转
        playerView.transform.Rotate(Vector3.right * v * Time.deltaTime * angularSpeed * verticalRotateSensitivity);
    }

    void SetCursorToCentre()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
