using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoss
{

    public class CreateBody : MonoBehaviour
    {
        /// <summary>
        /// ��������
        /// </summary>
        [SerializeField] private GameObject _bodyPrefab;//����Ԥ����
        [SerializeField] private GameObject _firstHead;//��һ��ͷ
        private GameObject _newBodyHead;//һ���������ͷ
        private const int MaxBodyCount = 3;//����Boss��ʼ����
        [NonSerialized] public static int BossLength;//Boss��ǰ����
        [SerializeField] private GameObject _hpObject;//����ֵ
        private Text _hpText;//����ֵ�ı�
        private float _allTime = 0f;//��Ϸʱ��
        private bool _gameOver = false;//����Boss
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private void Start()
        {
            _hpText = _hpObject.GetComponent<Text>();

            BossLength = MaxBodyCount;
            _newBodyHead = _firstHead;
            for (int i = 0; i < MaxBodyCount; i++)
            {
                GameObject TheBody = Instantiate(_bodyPrefab);
                TheBody.GetComponent<Body>().ParentHead = _newBodyHead;
                _newBodyHead = TheBody;
                if(i==0)
                {
                    var FirstHeadNextBody = _firstHead.GetComponent<Head>();//����ͷ��->��һ����������
                    if(FirstHeadNextBody != null)
                    {
                        FirstHeadNextBody._nextBody = TheBody;
                    }
                }
            }
        }

        private void Update()
        {
            if (BossLength == 0 && !_gameOver)
            {
                _gameOver = true;
                _hpText.text = "��ϲ����,��ʱ" + (int)_allTime + "��";
                _audioSource.clip = _audioClip;
                _audioSource.Play();
            }
            else if(!_gameOver)
            {
                UpUi();
            }
        }

        private void UpUi()
        {
            _allTime += Time.deltaTime;
            _hpText.text = "BOSS:" + BossLength + "  ��ʱ:" + (int)_allTime;
        }
    }

}
