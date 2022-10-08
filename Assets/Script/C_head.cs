using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class C_head : MonoBehaviour
{
    /// <summary>
    /// Bossͷ���ƶ�
    /// </summary>
    [SerializeField] 
    private Vector3 _runDirection;
    private float _waitTime;
    private float _headSpeed;
    private float _angleSpeed;
    private float _minRotate;
    private float _maxSpeed;
    private float _minSpeed;
    private float _keeprunminTime;
    private float _keeprunmaxTime;
    private Vector3 _centerPoint;

    void Start()
    {
        _waitTime = 0f;
        _headSpeed = 5f;
        _angleSpeed = 0.01f;
        _minRotate = 0.1f;
        _maxSpeed = 15f;
        _minSpeed = 5f;
        _keeprunmaxTime = 8f;
        _keeprunminTime = 2f;
        _centerPoint = new Vector3(5.3f, 12.2f, 8.5f);
    }


    private void FixedUpdate()
    {
        MoveHead();
    }

    public void MoveHead()
    {
        Quaternion rotate = Quaternion.LookRotation(_runDirection);
        if (Vector3.Angle(_runDirection, transform.forward) > _minRotate)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _angleSpeed);

        transform.position += transform.forward * Time.deltaTime * _headSpeed;
        
        if (_waitTime >= 0)
        {
            _waitTime -= Time.fixedDeltaTime;
            return;
        }
        _waitTime = Random.Range(_keeprunminTime, _keeprunmaxTime);
        _headSpeed = Random.Range(_minSpeed, _maxSpeed);
        _runDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void OnTriggerEnter()
    {
        _runDirection = (_centerPoint-transform.position).normalized;
        _waitTime = 5;
    }
}
