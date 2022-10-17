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
        /// �������壬��ʾui
        /// </summary>
        [SerializeField] private Body _bodyPrefab;//����Ԥ����
        [SerializeField] private Head _firstHead;//��һ��ͷ
        
        private const int MaxBodyCount = 50;//����Boss��ʼ����
        [NonSerialized] public static int BossLength;//Boss��ǰ����
        [SerializeField]private Text _hpText;//����ֵ�ı�
        private float _allTime = 0f;//��Ϸʱ��
        private bool _gameOver = false;//����Boss
        [SerializeField] private AudioSource _audioSource;//Boss��Ч
        [SerializeField] private AudioClip _audioClip;//���ܺ󳡾���Ч

        private void Start()
        {
            BossLength = MaxBodyCount;
            Body lastCreatedBody = null;
            for (int i = 0; i < MaxBodyCount; i++)
            {
                Body newBody = Instantiate(_bodyPrefab);

                if (i == 0) //��LastBody->ThisBody �ų���һ��ͷ
                {
                    _firstHead.NextBody = newBody;
                    newBody.ParentHead = _firstHead;
                }
                else
                {
                    newBody.ParentBody = lastCreatedBody;
                    lastCreatedBody.NextBody = newBody;
                }

                lastCreatedBody = newBody;
            }
        }

        private void Update()
        {
            if(_gameOver)
            {
                return;
            }
            else if (BossLength == 0)
            {
                _gameOver = true;
                _hpText.text = "��ϲ����,��ʱ" + (int)_allTime + "��";
                _audioSource.clip = _audioClip;
                _audioSource.Play();
            }
            else
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
