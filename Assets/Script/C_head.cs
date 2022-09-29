using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_head : MonoBehaviour
{
    private Vector3 run;
    private float wait_time;
    private float speed;
    void Start()
    {
        wait_time = 0f;
        speed = 1f;
    }

    void Update()
    {
        this.transform.position += run * Time.deltaTime * speed;
    }

    private void FixedUpdate()
    {
        if(wait_time>=0)
        {
            wait_time-=Time.fixedDeltaTime;
            return;
        }
        wait_time = Random.Range(2f, 8f);
        //speed = Random.Range(1f, 10f);
        run = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        this.transform.LookAt(run+this.transform.position);
    }
}
