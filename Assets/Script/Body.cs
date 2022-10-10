using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BigBoss
{

    public class Body : MonoBehaviour
    {
        /// <summary>
        /// Boss��������ƶ�
        /// ͨ������ʵ���Ӹ��游�ƶ��켣�˶�
        /// </summary>
        private Queue<Vector3> _historyPoints = new Queue<Vector3>();//���ڵ���ʷ��λ����
        private Queue<Quaternion> _historyRotas = new Queue<Quaternion>();//���ڵ���ʷ�������
        [NonSerialized] public GameObject ParentHead;//�ý������ͷ
        private float _twoBodyDistance = 2.7f;//ǰ��ڵ�ļ������
        private Transform _transform;//�������

        [NonSerialized] public bool BeShoot;
        private int _bodyHp = 4;
        [SerializeField] private GameObject _headPrefab;//ͷ��Ԥ���壬���ڵ㱻�ݻ٣�����ͷΪ���ڵ�

        private const float AllChangeColor = 0.3f;//ɫ�ʱ仯��ֵ
        private float _changeColor;//ɫ�ʸı䵥λ��

        private void Start()
        {
            _transform = transform;
            BeShoot = false;
            _changeColor = AllChangeColor / _bodyHp;
        }

        private void Update()
        {
            CreateNewHead();
            ReduceHp();
            MoveBodyByPrevious();
        }
        private void OnDestroy()
        {
            CreateBody.BossLength--;
        }

        private void MoveBodyByPrevious()//�����ƶ�
        {
            //���ڵ�λ����Ϣ���
            _historyPoints.Enqueue(ParentHead.transform.position);
            _historyRotas.Enqueue(ParentHead.transform.rotation);

            while (NeedUpDatePoint(_transform.position))//�ж��Ƿ���Ҫ�����ӽڵ�λ��
            {
                //�����ӽڵ�λ��
                _transform.position = _historyPoints.Dequeue();
                _transform.rotation = _historyRotas.Dequeue();
            }
        }

        private bool NeedUpDatePoint(Vector3 NextPoint)
        {
            return (ParentHead.transform.position - NextPoint).sqrMagnitude > _twoBodyDistance && _historyPoints.Count > 0 && _historyRotas.Count > 0;
        }

        private void ReduceHp()//����ֵ����
        {
            if(BeShoot)
            {
                _bodyHp--;
                BeShoot = false;
                ChangeColor();

            }
            if(_bodyHp<=0)
            {
                Destroy(gameObject);
            }
        }

        private void ChangeColor()//���
        {
            MeshRenderer[] SonsColor= gameObject.GetComponentsInChildren<MeshRenderer>();
            for(int i = 0; i < SonsColor.Length; i++)
                SonsColor[i].material.color += new Color(_changeColor, -_changeColor, -_changeColor);
        }

        private void CreateNewHead()//�Ͻڵ㱻���٣������µ�ͷ��˫���
        {
            if(ParentHead==null)
            {
                GameObject NewHead = Instantiate(_headPrefab, _transform.position, _transform.rotation);
                ParentHead = NewHead;
                var HeadScript = NewHead.GetComponent<Head>();
                if(HeadScript != null)
                {
                    HeadScript._nextBody = gameObject;
                }
            }
        }
    }

}