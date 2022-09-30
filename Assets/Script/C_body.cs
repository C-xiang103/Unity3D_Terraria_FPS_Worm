using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_body : MonoBehaviour
{
    private Queue<Vector3> StoragePoint;
    private Queue<Quaternion> StorageRota;
    public GameObject FrontBody;
    private Transform GoToNextPoint;

    private void Start()
    {
        StoragePoint = new Queue<Vector3>();
        StorageRota = new Queue<Quaternion>();
        StoragePoint.Clear();
        StorageRota.Clear();
    }

    private void FixedUpdate()
    {
        StoragePoint.Enqueue(FrontBody.transform.position);
        StorageRota.Enqueue(FrontBody.transform.rotation);

        Vector3 NextPoint = this.transform.position;
        Quaternion NextRota = this.transform.rotation;

        while((FrontBody.transform.position - NextPoint).sqrMagnitude>1.2f && StoragePoint.Count > 0 && StorageRota.Count > 0)
        {
             NextPoint = StoragePoint.Dequeue();
             NextRota = StorageRota.Dequeue();
        }

        this.transform.position = NextPoint;
        this.transform.rotation = NextRota;
    }
}
