using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBody : MonoBehaviour
{
    /// <summary>
    /// 初始化创建Boss身体
    /// </summary>
    public GameObject bodyPreform;
    public int bodyLength;
    public GameObject theHead;
    void Start()
    {
        bodyLength = 30;
        for(int i=0;i<bodyLength;i++)
        {
            GameObject nextBody = Instantiate(bodyPreform);
            nextBody.GetComponent<C_body>().FrontBody = theHead;
            theHead = nextBody;
            GameObject.Find("Division").GetComponent<Division>()._allBodys[i] = nextBody;
        }
    }
}
