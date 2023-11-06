using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterMain.PowerBall
{
    public class AutoDestroy : MonoBehaviour
    {
        public float aliveTime;
        void Start () {
            Destroy(gameObject, aliveTime);
        }
    }
}
