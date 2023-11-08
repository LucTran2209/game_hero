using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBallScript : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody2D rb;
    public float damage = 100;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.z == 0)
        {
            rb.AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);
        }else
        {
            rb.AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.gameObject.GetComponent<AttributeManager>().TakeDmg(damage);
            Debug.Log("ssssss");
            Destroy(gameObject);
        }
    }
    
}
