using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBody : MonoBehaviour
{
    /// <summary>
    /// 初始化创建Boss身体
    /// </summary>
    public GameObject bodyPreform;
    public int allBodyLength;
    public GameObject newBodyHead;
    void Start()
    {
        allBodyLength = GameObject.Find("Division").GetComponent<Division>().allBody.Length;
        for(int i=0;i<allBodyLength;i++)
        {
            GameObject nextBody = Instantiate(bodyPreform);
            nextBody.GetComponent<MoveBody>().previousBody = newBodyHead;
            newBodyHead = nextBody;
            GameObject.Find("Division").GetComponent<Division>().allBody[i] = nextBody;
        }
    }
}
