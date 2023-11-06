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
    }

    void Update()
    {
        timeInt -= Time.deltaTime;
        if (enemies.Count <= 0)
        {
            OpenWall();
        }
        if (!checkSlime())
        {
            bossBehavior.enabled = true;
            attributeManager.enabled = true;
        }

        if (enemies.Count == 1 && timeInt <= 0)
        {
            timeInt = 30f;
            foreach (GameObject originalFly in fly)
            {
                GameObject newFly = Instantiate(originalFly, originalFly.transform.position, originalFly.transform.rotation);

            }
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
