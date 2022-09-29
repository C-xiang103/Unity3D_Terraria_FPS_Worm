using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_main : MonoBehaviour
{
    public float speed;
    private float rotatemax = 500f;
    private float rotatemin = 100f;
    private bool is_x_right;
    // Start is called before the first frame update
    void Start()
    {
        speed = rotatemax;
        is_x_right = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,Time.deltaTime*speed,0);
    }

    private void FixedUpdate()
    {
        if(is_x_right)
        {
            if (speed < rotatemax)
                speed += Time.fixedDeltaTime * 100;
            else
                is_x_right = false;
        }
        else
        {
            if (speed > rotatemin)
                speed -= Time.fixedDeltaTime * 100;
            else
                is_x_right = true;
        }
    }
}
