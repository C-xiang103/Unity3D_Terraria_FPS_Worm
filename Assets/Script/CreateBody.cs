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
        /// 创建身体，显示ui
        /// </summary>
        [SerializeField] private Body _bodyPrefab;//身体预制体
        [SerializeField] private Head _firstHead;//第一个头
        
        private const int MaxBodyCount = 30;//设置Boss初始长度
        [NonSerialized] public static int BossLength;//Boss当前长度
        [SerializeField] private GameObject _hpObject;//生命值显示
        private Text _hpText;//生命值文本
        private float _allTime = 0f;//游戏时长
        private bool _gameOver = false;//击败Boss
        [SerializeField] private AudioSource _audioSource;//Boss音效
        [SerializeField] private AudioClip _audioClip;//击败后场景音效

        private void Start()
        {
            _hpText = _hpObject.GetComponent<Text>();

            BossLength = MaxBodyCount;
            Body body = _firstHead;
            for (int i = 0; i < MaxBodyCount; i++)
            {
                Body TheBody = Instantiate(_bodyPrefab);

                if(i != 0)//绑定LastBody->ThisBody 排除第一个头
                {
                    body.NextBody = TheBody;
                }

                TheBody.ParentHead = body;//绑定ThisBody->HeadBody

                body = TheBody;
                if(i==0)
                {
                    var FirstHeadNextBody = _firstHead.GetComponent<Head>();//绑定Head->firstBody
                    if(FirstHeadNextBody != null)
                    {
                        FirstHeadNextBody.NextBody = TheBody;
                    }
                }
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
                _hpText.text = "恭喜过关,耗时" + (int)_allTime + "秒";
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
            _hpText.text = "BOSS:" + BossLength + "  耗时:" + (int)_allTime;
        }
    }

}
