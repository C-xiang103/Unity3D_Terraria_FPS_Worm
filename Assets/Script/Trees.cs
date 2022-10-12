using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoss
{

    public class Trees : MonoBehaviour
    {
        public void NotifyDead()
        {
            Destroy(gameObject);
        }
    }
}
