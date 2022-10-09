using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoss
{

    public class CrateBody : MonoBehaviour
    {
        /// <summary>
        /// 初始化创建Boss身体
        /// </summary>
        /// 
        [SerializeField] private GameObject _bodyPreform;
        [SerializeField] private GameObject NewBodyHead;
        private static GameObject[] _allBody;
        public static GameObject[] AllBody => _allBody;
        private const int MaxBodyCount = 30;

        private void Start()
        {
            _allBody = new GameObject[MaxBodyCount];
            //AllBodyLength = GameObject.Find("Division").GetComponent<Division>().AllBody.Length;

            //AllBodyLength = Division.Instance.AllBody.Length;

            for (int i = 0; i < MaxBodyCount; i++)
            {
                GameObject nextBody = Instantiate(_bodyPreform);
                nextBody.GetComponent<MoveBody>().PreviousBody = NewBodyHead;
                NewBodyHead = nextBody;
                _allBody[i] = nextBody;
                //GameObject.Find("Division").GetComponent<Division>().AllBody[i] = nextBody;
            }
        }
    }

}
