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
    public GameObject headPreform;
    void Start()
    {
        bodyLength = GameObject.Find("Division").GetComponent<Division>()._allBodys.Length;
        for(int i=0;i<bodyLength;i++)
        {
            GameObject nextBody = Instantiate(bodyPreform);
            nextBody.GetComponent<MoveBody>().FrontBody = headPreform;
            headPreform = nextBody;
            GameObject.Find("Division").GetComponent<Division>()._allBodys[i] = nextBody;
        }
    }
}
