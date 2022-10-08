using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_body : MonoBehaviour
{
    private Queue<Vector3> _HistoryPoints;
    private Queue<Quaternion> _HistoryRotas;
    public GameObject FrontBody;
    private float _BodysDistance;

    private void Start()
    {
        _HistoryPoints = new Queue<Vector3>();
        _HistoryRotas = new Queue<Quaternion>();
        _HistoryPoints.Clear();
        _HistoryRotas.Clear();
        _BodysDistance = 1.5f;
    }

    private void FixedUpdate()
    {
        MoveBodyByPrevious();
    }

    private void Update()
    {
        
    }

    public void MoveBodyByPrevious()
    {
        _HistoryPoints.Enqueue(FrontBody.transform.position);
        _HistoryRotas.Enqueue(FrontBody.transform.rotation);

        Vector3 NextPoint = transform.position;
        Quaternion NextRota = transform.rotation;

        while((FrontBody.transform.position - NextPoint).sqrMagnitude > _BodysDistance && _HistoryPoints.Count > 0 && _HistoryRotas.Count > 0)
        {
             NextPoint = _HistoryPoints.Dequeue();
             NextRota = _HistoryRotas.Dequeue();
        }

        transform.position = NextPoint;
        transform.rotation = NextRota;
    }
}
