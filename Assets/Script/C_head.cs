using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_head : MonoBehaviour
{
    private Vector3 _RunDirection;
    private float _WaitTime;
    private float _HeadSpeed;
    private float _AngleSpeed;
    private float _MinRotate;
    private float _SpeedMax;
    private float _SpeedMin;
    private float _KeepRunTimeMin;
    private float _KeepRunTimeMax;

    void Start()
    {
        _WaitTime = 0f;
        _HeadSpeed = 5f;
        _AngleSpeed = 0.01f;
        _MinRotate = 0.1f;
        _SpeedMax = 20f;
        _SpeedMin = 10f;
        _KeepRunTimeMax = 8f;
        _KeepRunTimeMin = 2f;
    }


    private void FixedUpdate()
    {
        MoveHead();
    }

    public void MoveHead()
    {
        Quaternion rotate = Quaternion.LookRotation(_RunDirection);
        if (Vector3.Angle(_RunDirection, transform.forward) > _MinRotate)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _AngleSpeed);

        transform.position += transform.forward * Time.deltaTime * _HeadSpeed;

        if(_WaitTime>=0)
        {
            _WaitTime-=Time.fixedDeltaTime;
            return;
        }
        _WaitTime = Random.Range(_KeepRunTimeMin, _KeepRunTimeMax);
        _HeadSpeed = Random.Range(_SpeedMin, _SpeedMax);
        _RunDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
