using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// 玩家移动及视角
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

        //当前垂直角度
        double VerticalAngle = playerView.transform.eulerAngles.x;

        //通过鼠标获取竖直、水平轴的值，范围在-1到1
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y") * -1;

        //角色水平旋转
        transform.Rotate(Vector3.up * h * Time.deltaTime * _angularSpeed * _horizontalRotateSensitivity);

        //计算本次旋转后，竖直方向上的欧拉角
        double targetAngle = VerticalAngle + v * Time.deltaTime * _angularSpeed * _verticalRotateSensitivity;

        //竖直方向视角限制
        if (targetAngle > _maxDepressionAngle && targetAngle < 360 - _maxElevationAngle) return;

        //摄像机竖直方向上旋转
        playerView.transform.Rotate(Vector3.right * v * Time.deltaTime * _angularSpeed * _verticalRotateSensitivity);
    }

    void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //按照矢量移动一段距离
        transform.Translate(Vector3.forward * v * 0.02f * _moveSpeed);
        transform.Translate(Vector3.right * h * 0.02f * _moveSpeed);
    }

    void SetCursorToCentre()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
