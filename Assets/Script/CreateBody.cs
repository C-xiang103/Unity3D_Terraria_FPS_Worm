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
        /// 创建身体
        /// </summary>
        [SerializeField] private GameObject _bodyPrefab;//身体预制体
        [SerializeField] private GameObject _firstHead;//第一个头
        private GameObject _newBodyHead;//一节新身体的头
        private const int MaxBodyCount = 3;//设置Boss初始长度
        [NonSerialized] public static int BossLength;//Boss当前长度
        [SerializeField] private GameObject _hpObject;//生命值
        private Text _hpText;//生命值文本
        private float _allTime = 0f;//游戏时长
        private bool _gameOver = false;//击败Boss
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
                    var FirstHeadNextBody = _firstHead.GetComponent<Head>();//创建头部->第一节身体连接
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
                _hpText.text = "恭喜过关,耗时" + (int)_allTime + "秒";
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
            _hpText.text = "BOSS:" + BossLength + "  耗时:" + (int)_allTime;
        }
    }

}
