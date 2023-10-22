using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKick : MonoBehaviour
{

    [SerializeField] private GameObject kickPrefab;
    [SerializeField] private GameObject attackPoint;
    private float speed = 5f;
    private Vector3 attackPointPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackPointPosition = attackPoint.transform.position;
        if (Input.GetKey(KeyCode.K))
        {
            Vector3 v;
            if (attackPointPosition.x > 0)
            {
                v = new Vector3(1, 0, 0);
            }
            else
            {
                v = new Vector3(-1, 0, 0);
            }         
            GameObject kick = Instantiate(kickPrefab);
            kick.transform.position = attackPointPosition;
            kick.transform.Translate(speed * v * Time.deltaTime);
            kick.SetActive(true);    
                      
        }
    }
}
