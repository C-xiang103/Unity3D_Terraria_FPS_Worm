using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace BigBoss
{
    /// <summary>
    /// 开火控制
    /// </summary>
    public class FireControl : MonoBehaviour
    {
        [SerializeField] private Transform _firePointTransform;//开火点
        [SerializeField] private AudioSource _fireSource;//开枪声音
        private float _maxFireRange = 300f;//子弹最远距离
        private const float FireRate = 0.1f;//每发子弹射出间隔
        private float _fireTime = 0f;//开火冷却时间
        [SerializeField] private ParticleSystem _fireEffect;//开火粒子特效
        private float _bulletsNumber = 500;//子弹总量
        [SerializeField] private Text _bulletsNumberText;//子弹文本
        [SerializeField] private GameObject CrossHair;//准心
        private const float KeepChangeColorMaxTime = 0.1f;//保持准心颜色变化的最大时间
        private float _changeColorTime;//恢复原色倒计时
        private Color _crossHairColor;//准心颜色

        private void Start()
        {
            _fireTime = FireRate;
            _changeColorTime = KeepChangeColorMaxTime;
            _bulletsNumberText.text = ""+_bulletsNumber;
            _crossHairColor = Color.green;
            ChangeCrossHairColor(CrossHair, _crossHairColor);
        }
        private void Update()
        {
            _fireTime = _fireTime > 0 ? _fireTime - Time.deltaTime : _fireTime;//开火冷却
            if(Input.GetMouseButton(0) && _bulletsNumber > 0 && _fireTime  <0f)//开火判断
            {
                _fireTime = FireRate;//重置开火冷却
                GunFire();
                _bulletsNumber--;//弹药
                _bulletsNumberText.text = "" + _bulletsNumber;//文本更新
            }

            if(_changeColorTime>0)//准心颜色恢复
            {
                _changeColorTime -= Time.deltaTime;
            }
            else if(_crossHairColor == Color.red)
            {
                _crossHairColor = Color.green;
                ChangeCrossHairColor(CrossHair, _crossHairColor);
            }
        }

        private void GunFire()
        {
            RaycastHit FireHit;
            _fireEffect.Play();
            _fireSource.Play();//播放开火效果
            //击中判断，并执行对应效果
            if(Physics.Raycast(_firePointTransform.position,_firePointTransform.forward,out FireHit,_maxFireRange))
            {
                var IsBody = FireHit.collider.gameObject.GetComponent<Body>();
                var IsHead = FireHit.collider.gameObject.GetComponent<Head>();
                //头或身体的射击判断
                if (IsBody != null)
                {
                    IsBody.BeShoot = true;
                }
                else if (IsHead != null)
                {
                    IsHead.BeShoot = true;
                }
                //击中准心变红
                if(IsBody != null || IsHead != null)
                {
                    _crossHairColor = Color.red;
                    ChangeCrossHairColor(CrossHair, _crossHairColor);
                    _changeColorTime = KeepChangeColorMaxTime;
                }
            }
        }

        private void ChangeCrossHairColor(GameObject ParentGameObject,Color NeedChange)//改变所有子物体图片颜色
        {
            Image[] Son= ParentGameObject.GetComponentsInChildren<Image>();
            if(CrossHair!=null)
            {
                for(int i=0;i<Son.Length;i++)
                {
                    Son[i].color = NeedChange;
                }
            }
        }
    }

}