using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Division : MonoBehaviour
{
    /// <summary>
    /// Boss∑÷¡—
    /// </summary>
    public GameObject HeadPreform;
    public GameObject[] AllBody;
    private int _bodyMaxLength;
    private float _waitTime;

    private void Awake()
    {
        AllBody = new GameObject[30];
        _bodyMaxLength = AllBody.Length;
    }

    private void Start()
    {
        _waitTime = 2f;
    }

    private void FixedUpdate()
    {
        _waitTime -= 0.02f;
        if (_waitTime < 0 && _bodyMaxLength > 0)
        {
            _waitTime = Random.Range(1f, 5f);
            int i = Random.Range(0, _bodyMaxLength);
            GameObject newHead = Instantiate(HeadPreform, AllBody[i].transform.position, AllBody[i].transform.rotation);
            AllBody[i].GetComponent<MoveBody>().PreviousBody = newHead;
            _bodyMaxLength--;
            AllBody[i] = AllBody[_bodyMaxLength];
        }
    }
}
