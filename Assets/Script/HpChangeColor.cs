using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpChangeColor : MonoBehaviour
{
    [Header("һ�ε��õ���ɫ�仯��")]
    [SerializeField] private float _rgbR = 0f;
    [SerializeField] private float _rgbG = 0f;
    [SerializeField] private float _rgbB = 0f;
    public void ChangeColor()
    {
        MeshRenderer[] SonsColor = gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < SonsColor.Length; i++)
            SonsColor[i].material.color += new Color(_rgbR, _rgbG, _rgbB);
    }
}
