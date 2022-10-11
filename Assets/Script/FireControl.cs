using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace BigBoss
{
    /// <summary>
    /// �������
    /// </summary>
    public class FireControl : MonoBehaviour
    {
        [SerializeField] private Transform _firePointTransform;//�����
        [SerializeField] private AudioSource _fireSource;//��ǹ����
        private float _maxFireRange = 300f;//�ӵ���Զ����
        private const float FireRate = 0.1f;//ÿ���ӵ�������
        private float _fireTime = 0f;//������ȴʱ��
        [SerializeField] private ParticleSystem _fireEffect;//����������Ч
        private float _bulletsNumber = 500;//�ӵ�����
        [SerializeField] private Text _bulletsNumberText;//�ӵ��ı�
        [SerializeField] private GameObject CrossHair;//׼��
        private const float KeepChangeColorMaxTime = 0.1f;//����׼����ɫ�仯�����ʱ��
        private float _changeColorTime;//�ָ�ԭɫ����ʱ
        private Color _crossHairColor;//׼����ɫ

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
            _fireTime = _fireTime > 0 ? _fireTime - Time.deltaTime : _fireTime;//������ȴ
            if(Input.GetMouseButton(0) && _bulletsNumber > 0 && _fireTime  <0f)//�����ж�
            {
                _fireTime = FireRate;//���ÿ�����ȴ
                GunFire();
                _bulletsNumber--;//��ҩ
                _bulletsNumberText.text = "" + _bulletsNumber;//�ı�����
            }

            if(_changeColorTime>0)//׼����ɫ�ָ�
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
            _fireSource.Play();//���ſ���Ч��
            //�����жϣ���ִ�ж�ӦЧ��
            if(Physics.Raycast(_firePointTransform.position,_firePointTransform.forward,out FireHit,_maxFireRange))
            {
                var IsBody = FireHit.collider.gameObject.GetComponent<Body>();
                var IsHead = FireHit.collider.gameObject.GetComponent<Head>();
                //ͷ�����������ж�
                if (IsBody != null)
                {
                    IsBody.BeShoot = true;
                }
                else if (IsHead != null)
                {
                    IsHead.BeShoot = true;
                }
                //����׼�ı��
                if(IsBody != null || IsHead != null)
                {
                    _crossHairColor = Color.red;
                    ChangeCrossHairColor(CrossHair, _crossHairColor);
                    _changeColorTime = KeepChangeColorMaxTime;
                }
            }
        }

        private void ChangeCrossHairColor(GameObject ParentGameObject,Color NeedChange)//�ı�����������ͼƬ��ɫ
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