using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Division : MonoBehaviour
{
    /// <summary>
    /// Boss∑÷¡—
    /// </summary>
    public GameObject newheadPreform;
    public GameObject[] _allBodys;
    private int _maxLength;
    private float _waitTime;

    private void Awake()
    {
        _allBodys = new GameObject[30];
        _maxLength = _allBodys.Length;
    }

    private void Start()
    {
        _waitTime = 2f;
    }

    private void FixedUpdate()
    {
        _waitTime -= 0.02f;
        if (_waitTime < 0 && _maxLength > 0)
        {
            _waitTime = Random.Range(1f, 5f);
            int i = (int)Random.Range(-0.5f, _maxLength-0.5f);
            i = i == -1 ? 0 : i;
            GameObject newHead = Instantiate(newheadPreform, _allBodys[i].transform.position, _allBodys[i].transform.rotation);
            _allBodys[i].GetComponent<MoveBody>().FrontBody = newHead;
            _maxLength--;
            _allBodys[i] = _allBodys[_maxLength];
        }
    }
}
