using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_2 : MonoBehaviour
{
    public GameObject GameObject1;
    // Start is called before the first frame update
    string name1;

    private void Awake()
    {
        name1 = this.name + "_";
        Debug.Log(name + "Awake2");
    }

    private void OnEnable()
    {
        Debug.Log(name + "OnEnable2");
    }

    private void Reset()
    {
        //Debug.Log(name + "Reset2");
    }

    void Start()
    {
        Debug.Log(name1 + "Start2");
        //for(int i=0;i<GameObject1.Length;i++)
        //{
        //    Destroy(GameObject1[i], 0.1f);
        //}
    }
}
