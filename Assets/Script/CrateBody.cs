using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBody : MonoBehaviour
{
    /// <summary>
    /// ��ʼ������Boss����
    /// </summary>
    public GameObject BodyPreform;
    public int AllBodyLength;
    public GameObject NewBodyHead;
    void Start()
    {
        AllBodyLength = GameObject.Find("Division").GetComponent<Division>().AllBody.Length;
        for(int i=0;i<AllBodyLength;i++)
        {
            GameObject nextBody = Instantiate(BodyPreform);
            nextBody.GetComponent<MoveBody>().PreviousBody = NewBodyHead;
            NewBodyHead = nextBody;
            GameObject.Find("Division").GetComponent<Division>().AllBody[i] = nextBody;
        }
    }
}
