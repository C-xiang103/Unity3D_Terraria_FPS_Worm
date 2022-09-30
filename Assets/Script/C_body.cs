using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_body : MonoBehaviour
{
    private Queue<Vector3> _StoragePoints;
    private Queue<Quaternion> _StorageRotas;
    public GameObject FrontBody;

    private void Start()
    {
        _StoragePoints = new Queue<Vector3>();
        _StorageRotas = new Queue<Quaternion>();
        _StoragePoints.Clear();
        _StorageRotas.Clear();
    }

    private void FixedUpdate()
    {
        _StoragePoints.Enqueue(FrontBody.transform.position);
        _StorageRotas.Enqueue(FrontBody.transform.rotation);

        Vector3 NextPoint = transform.position;
        Quaternion NextRota = transform.rotation;

        while((FrontBody.transform.position - NextPoint).sqrMagnitude>1.2f && _StoragePoints.Count > 0 && _StorageRotas.Count > 0)
        {
             NextPoint = _StoragePoints.Dequeue();
             NextRota = _StorageRotas.Dequeue();
        }

        transform.position = NextPoint;
        transform.rotation = NextRota;
    }
}
