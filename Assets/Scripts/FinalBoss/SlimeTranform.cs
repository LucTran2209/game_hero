using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTranform : MonoBehaviour
{

    [SerializeField] private GameObject Wall;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] float timeInt;
    [SerializeField] GameObject[] fly;

    private BossBehavior bossBehavior;
    private AttributeManager attributeManager;
    private Animator animatorBoss;

    [SerializeField] GameObject boss;


    void Awake()
    {
        animatorBoss = boss.GetComponent<Animator>();
        bossBehavior = boss.GetComponent<BossBehavior>();
        attributeManager = boss.GetComponent<AttributeManager>();
        foreach (GameObject originalFly in fly)
        {
            originalFly.transform.localScale = Vector3.one;
        }
    }

    void Update()
    {
        timeInt -= Time.deltaTime;
        if (enemies.Count <= 0)
        {
            OpenWall();
        }
        

    }

    private bool checkSlime()
    {
        return animatorBoss.GetCurrentAnimatorStateInfo(0).IsName("Tranform") ||
         animatorBoss.GetCurrentAnimatorStateInfo(0).IsName("Slime_Idle");
    }

    private void OpenWall()
    {
        Wall.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.tag == "Player")
            {

                bossBehavior.enabled = true;
                attributeManager.enabled = true;
            Wall.SetActive(true);
                animatorBoss.SetBool("Tranform", true);
            }
            if (collision.tag == "Monster")
            {
                enemies.Add(collision.gameObject);
            }
        }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.tag == "Monster")
        {
            enemies.Remove(trig.gameObject);
        }
    }
}
