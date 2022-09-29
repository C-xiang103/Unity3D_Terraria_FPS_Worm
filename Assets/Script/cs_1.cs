using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cs_1 : MonoBehaviour
{
    // Start is called before the first frame update
    string name1;

    private void Awake()
    {
        name1 = this.name + "_";
        Debug.Log(name1 + "Awake");
    }


    private void OnEnable()
    {
        //Debug.Log(name1 + "OnEnable");
    }

    private void Reset()
    {
        //Debug.Log(name1 + "Reset");
    }

    private void OnValidate()
    {
        //Debug.Log(name1 + "OnValidate");
    }

    void Start()
    {
        //Debug.Log(name1 + "Start");
    }


    private void FixedUpdate()
    {
        //Debug.Log(name1 + "FixedUpdate");
    }

    void Update()
    {
        //Debug.Log(name1 + "Update");
    }

    private void LateUpdate()
    {
        //Debug.Log(name1 + "LateUpdate");
    }

    private void OnDestroy()
    {
        //Debug.Log(name1 + "OnDestroy");
    }

    private void OnDisable()
    {
        //Debug.Log(name1 + "OnDisable");
    }

    










}
