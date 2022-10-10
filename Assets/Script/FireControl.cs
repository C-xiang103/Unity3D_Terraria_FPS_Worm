using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace BigBoss
{

    public class FireControl : MonoBehaviour
    {
        [SerializeField] private Transform _firePointTransform;//�����
        [SerializeField] private AudioSource _fireSource;//��ǹ��Ч
        private float _maxFireRange = 500f;//�ӵ���Զ����
        private const float FireRate = 0.2f;//ÿ���ӵ�������
        private float _fireTime = 0f;//������ȴʱ��
        [SerializeField] private ParticleSystem _fireEffect;//����������Ч

        private void Start()
        {
            _fireTime = FireRate;
        }
        private void Update()
        {
            _fireTime = _fireTime > 0 ? _fireTime - Time.deltaTime : _fireTime;
            if(Input.GetMouseButton(0)&&_fireTime<0f)
            {
                _fireTime = FireRate;
                GunFire();
            }
        }

        private void GunFire()
        {
            RaycastHit FireHit;
            _fireEffect.Play();
            _fireSource.Play();
            if(Physics.Raycast(_firePointTransform.position,_firePointTransform.forward,out FireHit,_maxFireRange))
            {
                var IsBody = FireHit.collider.gameObject.GetComponent<Body>();
                var IsHead = FireHit.collider.gameObject.GetComponent<Head>();
                if (IsBody != null)
                    IsBody.BeShoot = true;
                if (IsHead != null)
                    IsHead.BeShoot = true;
            }
        }
    }

}