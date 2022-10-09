using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBody : MonoBehaviour
{
    /// <summary>
    /// Boss��������ƶ�
    /// </summary>
    private Queue<Vector3> _historyPoints;
    private Queue<Quaternion> _historyRotas;
    public GameObject previousBody;
    private float _twoBodyDistance;

    private void Start()
    {
        _historyPoints = new Queue<Vector3>();
        _historyRotas = new Queue<Quaternion>();
        _twoBodyDistance = 2f;
    }

    private void FixedUpdate()
    {
        MoveBodyByPrevious();
    }

    public void MoveBodyByPrevious()
    {
        _historyPoints.Enqueue(previousBody.transform.position);
        _historyRotas.Enqueue(previousBody.transform.rotation);

        Vector3 NextPoint = transform.position;
        Quaternion NextRota = transform.rotation;

        while((previousBody.transform.position - NextPoint).sqrMagnitude > _twoBodyDistance && _historyPoints.Count > 0 && _historyRotas.Count > 0)
        {
             NextPoint = _historyPoints.Dequeue();
             NextRota = _historyRotas.Dequeue();
        }

        transform.position = NextPoint;
        transform.rotation = NextRota;
    }
}
