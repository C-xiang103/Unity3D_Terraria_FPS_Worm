using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_head : MonoBehaviour
{
    private Transform head;
    private Vector3 run;
    private float wait_time;
    private float speed;
    void Start()
    {
        head = this.transform;
        wait_time = 0f;
        speed = 1f;
    }

    void Update()
    {
        head.position += run * Time.deltaTime * speed;
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
    }
}
