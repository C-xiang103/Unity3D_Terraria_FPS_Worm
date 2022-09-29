using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_head : MonoBehaviour
{
    private Transform head;
    private float go_x, go_y, go_z;
    private Vector3 run;
    private float wait_time;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        head = this.transform;
        go_x = 0;
        go_y = 0;
        go_z = 0;
        run = new Vector3(go_x, go_y, go_z);
        wait_time = 0f;
        speed = 1f;
    }

    // Update is called once per frame
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
        go_x = Random.Range(-1f, 1f);
        go_y = Random.Range(-1f, 1f);
        go_z = Random.Range(-1f, 1f);
        run = new Vector3(go_x, go_y, go_z).normalized;
    }
}
