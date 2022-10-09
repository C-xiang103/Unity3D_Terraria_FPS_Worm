using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveHead : MonoBehaviour
{
    /// <summary>
    /// BossÍ·²¿ÒÆ¶¯
    /// </summary>
    [SerializeField] 
    private Vector3 _runDirection;
    private float _changeDirectionWaitTime;
    private float _headSpeed;
    private float _angleSpeed;
    private float _minJudgeRotate;
    private float _maxSpeed;
    private float _minSpeed;
    private float _maxAngleSpeed;
    private float _minAngleSpeed;
    private float _keepRunMinTime;
    private float _keepRunMaxTime;
    private Vector3 _headTarget;

    void Start()
    {
        _changeDirectionWaitTime = 0f;
        _headSpeed = 5f;
        _angleSpeed = 0.1f;
        _minJudgeRotate = 0.1f;
        _maxSpeed = 15f;
        _minSpeed = 5f;
        _maxAngleSpeed = 0.2f;
        _minAngleSpeed = 0.01f;
        _keepRunMaxTime = 8f;
        _keepRunMinTime = 2f;
        _headTarget = new Vector3(5.3f, 12.2f, 8.5f);
    }


    private void FixedUpdate()
    {
        RotateHead();
        RunHead();
        HasNewHeadMoveVariable();
        OverRange();
        _headTarget=GameObject.Find("Player").transform.position;
    }

    public void RotateHead()
    {
        Quaternion rotate = Quaternion.LookRotation(_runDirection);
        if (Vector3.Angle(_runDirection, transform.forward) > _minJudgeRotate)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _angleSpeed);
    }

    public void RunHead()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * _headSpeed;
        _headSpeed += _headSpeed > _minSpeed ? (-1f) * transform.forward.y * 0.1f : 0f;
    }

    public void HasNewHeadMoveVariable()
    {
        if (_changeDirectionWaitTime >= 0)
        {
            _changeDirectionWaitTime -= Time.fixedDeltaTime;
            return;
        }
        _changeDirectionWaitTime = Random.Range(_keepRunMinTime, _keepRunMaxTime);
        _headSpeed = Random.Range(_minSpeed, _maxSpeed);
        _angleSpeed = Random.Range(_minAngleSpeed, _maxAngleSpeed);
        //_runDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _runDirection = (_headTarget - transform.position).normalized;
        _runDirection = (_runDirection + new Vector3(0f,Random.Range(-0.8f,0.8f),0f)).normalized;
    }

    public void OverRange()
    {
        if((transform.position-_headTarget).sqrMagnitude>2500f)
        {
            _runDirection = (_headTarget - transform.position).normalized;
        }    
    }

    private void OnTriggerEnter()
    {
        _runDirection = (_headTarget-transform.position).normalized;
        _changeDirectionWaitTime = (_keepRunMaxTime+ _keepRunMinTime)/2;
    }
}
