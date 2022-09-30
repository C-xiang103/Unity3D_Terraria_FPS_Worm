using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_head : MonoBehaviour
{
    private Vector3 run;
    private float wait_time;
    public float speed;

    public float angleSpeed = 0.01f;

    void Start()
    {
        wait_time = 0f;
        speed = 5f;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Quaternion rotate = Quaternion.LookRotation(run);
        if (Vector3.Angle(run, transform.forward) > 0.1f)
            transform.rotation = Quaternion.Slerp(transform.localRotation, rotate, angleSpeed);

        this.transform.position += this.transform.forward * Time.deltaTime * speed;

        if(wait_time>=0)
        {
            wait_time-=Time.fixedDeltaTime;
            return;
        }
        wait_time = Random.Range(2f, 8f);
        speed = Random.Range(1f, 10f);
        run = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        //this.transform.LookAt(run+this.transform.position);

    }

}
