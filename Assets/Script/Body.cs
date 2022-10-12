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
        /// Boss身体跟随移动
        /// 通过队列实现子跟随父移动轨迹运动
        /// </summary>
        public event Action OnNotifyDead;//被标记死亡
        private Queue<Vector3> _historyPoints = new Queue<Vector3>();//父节点历史点位队列
        private Queue<Quaternion> _historyRotas = new Queue<Quaternion>();//父节点历史方向队列
        [NonSerialized] public Head ParentHead;//该节身体的头
        [NonSerialized] public Body ParentBody;
        [NonSerialized] public Body NextBody;//下一个身体

        private float _twoBodyDistance = 2.7f;//前后节点的间隔距离
        private Transform _transform;//自身组件

        [NonSerialized] public bool BeShoot;
        [SerializeField] private Head _headPrefab;//头部预制体，父节点被摧毁，创建头为父节点

        private const float _changeColor = 0.1f;//色彩变化最小单位量

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

        private void MoveBodyByPrevious()//跟随移动
        {
            //父节点位置信息入队
            _historyPoints.Enqueue(GetParentTransform().position);
            _historyRotas.Enqueue(GetParentTransform().rotation);

            while (NeedUpDatePoint(_transform.position))//判断是否需要更新子节点位置
            {
                //更新子节点位置
                _transform.position = _historyPoints.Dequeue();
                _transform.rotation = _historyRotas.Dequeue();
            }
        }

        private bool NeedUpDatePoint(Vector3 NextPoint)//需要更新全部运行信息
        {
            return (GetParentTransform().position - NextPoint).sqrMagnitude > _twoBodyDistance && _historyPoints.Count > 0 && _historyRotas.Count > 0;
        }

        public void NotifyDead()//生命值小于0，自己被摧毁
        {
            //if(OnNotifyDead != null)
            //{
            //    OnNotifyDead.Invoke();
            //}
            if (NextBody != null)//告诉下个身体，它的上个身体被摧毁
            {
                NextBody.LastBodyBeDestroy();
            }
            if (ParentHead != null)//告诉上个头，它的下个身体被摧毁
            {
                ParentHead.NextBodyDestroy();
            }
            Destroy(gameObject);//摧毁自身
        }

        public void NotifyDamange()
        {
            MeshRenderer[] SonsColor = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < SonsColor.Length; i++)
                SonsColor[i].material.color += new Color(_changeColor, -_changeColor, -_changeColor);
        }

        public void HeadBeDestry()//上个头被摧毁
        {
            Destroy(gameObject);
        }

        public void LastBodyBeDestroy()//上个身体被摧毁
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