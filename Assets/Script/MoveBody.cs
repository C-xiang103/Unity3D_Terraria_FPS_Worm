using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBody : MonoBehaviour
{
    /// <summary>
    /// Boss…ÌÃÂ∏˙ÀÊ“∆∂Ø
    /// </summary>
    private Queue<Vector3> _historyPoints;
    private Queue<Quaternion> _historyRotas;
    public GameObject PreviousBody;
    private float _twoBodyDistance;

    private void Start()
    {
        _historyPoints = new Queue<Vector3>();
        _historyRotas = new Queue<Quaternion>();
        _twoBodyDistance = 2.7f;
    }

    private void FixedUpdate()
    {
        MoveBodyByPrevious();
    }

    public void MoveBodyByPrevious()
    {
        _historyPoints.Enqueue(PreviousBody.transform.position);
        _historyRotas.Enqueue(PreviousBody.transform.rotation);

        Vector3 NextPoint = transform.position;
        Quaternion NextRota = transform.rotation;

        while(NeedUpDatePoint(NextPoint))
        {
             NextPoint = _historyPoints.Dequeue();
             NextRota = _historyRotas.Dequeue();
        }

        transform.position = NextPoint;
        transform.rotation = NextRota;
    }

    bool NeedUpDatePoint(Vector3 NextPoint)
    {
        return (PreviousBody.transform.position - NextPoint).sqrMagnitude > _twoBodyDistance && _historyPoints.Count > 0 && _historyRotas.Count > 0;
    }
}
