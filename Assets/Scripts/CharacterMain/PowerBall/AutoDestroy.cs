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
        public GameObject shootingEffect;

        public float aliveTime;

        private void Start()
        {
            DestroyBullet(aliveTime);
        }
        void DestroyBullet (float time) {
            Destroy(gameObject, time);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.name);
             
            if(collision.tag == "Monster" || collision.tag == "Ground")
            {
                DestroyBullet(0);
                Instantiate(shootingEffect, transform.position, Quaternion.identity);
                var enemy = collision.GetComponent<AttributeManager>();
                if (enemy != null)
                {
                    enemy.TakeDmg(500f);
                }
            }
        }
/*
        public void AppearEffect()
        {
            Instantiate(shootingEffect, transform.position, Quaternion.identity);
        }  */   
    }
}
