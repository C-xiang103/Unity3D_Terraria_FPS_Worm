using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_head : MonoBehaviour
{
    private Vector3 _RunDirection;
    private float _WaitTime;
    public float HeadSpeed;
    public float AngleSpeed = 0.01f;

    void Start()
    {
        _WaitTime = 0f;
        HeadSpeed = 5f;
    }


    private void FixedUpdate()
    {
        Quaternion rotate = Quaternion.LookRotation(_RunDirection);
        if (Vector3.Angle(_RunDirection, transform.forward) > 0.1f)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, AngleSpeed);

        transform.position += transform.forward * Time.deltaTime * HeadSpeed;

        if(_WaitTime>=0)
        {
            _WaitTime-=Time.fixedDeltaTime;
            return;
        }
        _WaitTime = Random.Range(2f, 8f);
        HeadSpeed = Random.Range(1f, 10f);
        _RunDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

    }

}
