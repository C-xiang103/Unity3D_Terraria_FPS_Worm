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
        private Queue<Vector3> _historyPoints = new Queue<Vector3>();//父节点历史点位队列
        private Queue<Quaternion> _historyRotas = new Queue<Quaternion>();//父节点历史方向队列
        [NonSerialized] public GameObject ParentHead;//该节身体的头
        private float _twoBodyDistance = 2.7f;//前后节点的间隔距离
        private Transform _transform;//自身组件

        [NonSerialized] public bool BeShoot;
        private int _bodyHp = 4;
        [SerializeField] private GameObject _headPrefab;//头部预制体，父节点被摧毁，创建头为父节点

        private const float AllChangeColor = 0.3f;//色彩变化总值
        private float _changeColor;//色彩改变单位量

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

        private void MoveBodyByPrevious()//跟随移动
        {
            //父节点位置信息入队
            _historyPoints.Enqueue(ParentHead.transform.position);
            _historyRotas.Enqueue(ParentHead.transform.rotation);

            while (NeedUpDatePoint(_transform.position))//判断是否需要更新子节点位置
            {
                //更新子节点位置
                _transform.position = _historyPoints.Dequeue();
                _transform.rotation = _historyRotas.Dequeue();
            }
        }

        private bool NeedUpDatePoint(Vector3 NextPoint)
        {
            return (ParentHead.transform.position - NextPoint).sqrMagnitude > _twoBodyDistance && _historyPoints.Count > 0 && _historyRotas.Count > 0;
        }

        private void ReduceHp()//生命值减少
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

        private void ChangeColor()//变红
        {
            MeshRenderer[] SonsColor= gameObject.GetComponentsInChildren<MeshRenderer>();
            for(int i = 0; i < SonsColor.Length; i++)
                SonsColor[i].material.color += new Color(_changeColor, -_changeColor, -_changeColor);
        }

        private void CreateNewHead()//上节点被销毁，创建新的头并双向绑定
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