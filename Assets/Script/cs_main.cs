using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_main : MonoBehaviour
{
    public float speed;
    private float rotatemax = 500f;
    private float rotatemin = 10f;
    private float booltime;
    // Start is called before the first frame update
    void Start()
    {
        speed = rotatemax;
        booltime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(booltime<=3)
        {
            booltime += Time.deltaTime;
        }
        else
        {
            booltime = 0;
            speed = Random.Range(rotatemin, rotatemax);
        }
        this.transform.Rotate(0,Time.deltaTime*speed,0);
    }
}
