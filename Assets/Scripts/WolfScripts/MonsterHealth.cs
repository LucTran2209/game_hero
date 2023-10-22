using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonterHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbodyMonster;
    [SerializeField] private Slider bloodSlider;

    // Start is called before the first frame update
    void Start()
    {
        //bloodSlider = GameObject.Find("BloodSlider");
        currentHealth = maxHealth;
        bloodSlider.maxValue = maxHealth;
        animator = GetComponent<Animator>();
        rigidbodyMonster = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bloodSlider.value = currentHealth;

    }

    public void TakeDmg(float Damage)
    {
        currentHealth -= Damage;

        // Set animation is hited when current health > 0
        if (currentHealth > 0 )
        {
            animator.SetTrigger("Hurt");
        }
        else
        {
            Deadth();
        }
    }

    public void Deadth()
    {
        // Set animation deadth and deactive gameobject
        animator.SetBool("IsDead", true);
        rigidbodyMonster.constraints = RigidbodyConstraints2D.FreezePositionY;
        GetComponent<Collider2D>().isTrigger = true;
        
      
    }
}
