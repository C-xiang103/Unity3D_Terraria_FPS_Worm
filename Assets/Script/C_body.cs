using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_body : MonoBehaviour
{
    private Queue<Vector3> _StoragePoint;
    private Queue<Quaternion> _StorageRota;
    public GameObject FrontBody;

    private void Start()
    {
        _StoragePoint = new Queue<Vector3>();
        _StorageRota = new Queue<Quaternion>();
        _StoragePoint.Clear();
        _StorageRota.Clear();
    }

    private void FixedUpdate()
    {
        _StoragePoint.Enqueue(FrontBody.transform.position);
        _StorageRota.Enqueue(FrontBody.transform.rotation);

        Vector3 NextPoint = transform.position;
        Quaternion NextRota = transform.rotation;

        while((FrontBody.transform.position - NextPoint).sqrMagnitude>1.2f && _StoragePoint.Count > 0 && _StorageRota.Count > 0)
        {
             NextPoint = _StoragePoint.Dequeue();
             NextRota = _StorageRota.Dequeue();
        }

        transform.position = NextPoint;
        transform.rotation = NextRota;
    }
}
