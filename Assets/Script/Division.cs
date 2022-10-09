using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Division : MonoBehaviour
{
    /// <summary>
    /// Boss∑÷¡—
    /// </summary>
    public GameObject headPreform;
    public GameObject[] allBody;
    private int _bodyMaxLength;
    private float _waitTime;

    private void Awake()
    {
        allBody = new GameObject[30];
        _bodyMaxLength = allBody.Length;
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
            GameObject newHead = Instantiate(headPreform, allBody[i].transform.position, allBody[i].transform.rotation);
            allBody[i].GetComponent<MoveBody>().previousBody = newHead;
            _bodyMaxLength--;
            allBody[i] = allBody[_bodyMaxLength];
        }
    }
}
