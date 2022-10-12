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
        public event Action OnNotifyDead;//���������
        private Queue<Vector3> _historyPoints = new Queue<Vector3>();//���ڵ���ʷ��λ����
        private Queue<Quaternion> _historyRotas = new Queue<Quaternion>();//���ڵ���ʷ�������
        [NonSerialized] public Head ParentHead;//�ý������ͷ
        [NonSerialized] public Body ParentBody;
        [NonSerialized] public Body NextBody;//��һ������

        private float _twoBodyDistance = 2.7f;//ǰ��ڵ�ļ������
        private Transform _transform;//�������

        [NonSerialized] public bool BeShoot;
        [SerializeField] private Head _headPrefab;//ͷ��Ԥ���壬���ڵ㱻�ݻ٣�����ͷΪ���ڵ�

        private const float _changeColor = 0.1f;//ɫ�ʱ仯��С��λ��

        public Transform GetParentTransform()
        {
            if(ParentHead != null)
            {
                return ParentHead.transform;
            }
            else if(ParentBody != null)
            {
                return ParentBody.transform;
            }
            return null;
            
        }
        private void Start()
        {
            _transform = transform;
            BeShoot = false;
        }

        private void Update()
        {
            MoveBodyByPrevious();
        }
        private void OnDestroy()
        {
            CreateBody.BossLength--;
            
        }

        private void MoveBodyByPrevious()//�����ƶ�
        {
            //���ڵ�λ����Ϣ���
            _historyPoints.Enqueue(GetParentTransform().position);
            _historyRotas.Enqueue(GetParentTransform().rotation);

            while (NeedUpDatePoint(_transform.position))//�ж��Ƿ���Ҫ�����ӽڵ�λ��
            {
                //�����ӽڵ�λ��
                _transform.position = _historyPoints.Dequeue();
                _transform.rotation = _historyRotas.Dequeue();
            }
        }

        private bool NeedUpDatePoint(Vector3 NextPoint)//��Ҫ����ȫ��������Ϣ
        {
            return (GetParentTransform().position - NextPoint).sqrMagnitude > _twoBodyDistance && _historyPoints.Count > 0 && _historyRotas.Count > 0;
        }

        public void NotifyDead()//����ֵС��0���Լ����ݻ�
        {
            //if(OnNotifyDead != null)
            //{
            //    OnNotifyDead.Invoke();
            //}
            if (NextBody != null)//�����¸����壬�����ϸ����屻�ݻ�
            {
                NextBody.LastBodyBeDestroy();
            }
            if (ParentHead != null)//�����ϸ�ͷ�������¸����屻�ݻ�
            {
                ParentHead.NextBodyDestroy();
            }
            Destroy(gameObject);//�ݻ�����
        }

        public void NotifyDamange()
        {
            MeshRenderer[] SonsColor = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < SonsColor.Length; i++)
                SonsColor[i].material.color += new Color(_changeColor, -_changeColor, -_changeColor);
        }

        public void HeadBeDestry()//�ϸ�ͷ���ݻ�
        {
            Destroy(gameObject);
        }

        public void LastBodyBeDestroy()//�ϸ����屻�ݻ�
        {
            Head NewHead = Instantiate(_headPrefab, _transform.position, _transform.rotation);
            ParentHead = NewHead;
            var HeadScript = NewHead;
            if (HeadScript != null)
            {
                HeadScript.NextBody = gameObject.GetComponent<Body>();
            }
        }
    }

}