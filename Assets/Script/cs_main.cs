using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_main : MonoBehaviour
{
    public int speed=10;
    // Start is called before the first frame update
    void Start()
    {
        speed = 30;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,Time.deltaTime*speed,0);
    }

    private void FixedUpdate()
    {

    }
}
