using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKick : MonoBehaviour
{

    public Transform attackStartPosition;
    public GameObject powerBall;
    float fireRate = 0.5f;
    float nextFire = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FirePowerBall();
        }
        
    }

    void FirePowerBall()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            

            if (true)
            {
                Instantiate(powerBall, attackStartPosition.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else
            {
                Instantiate(powerBall, attackStartPosition.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
        }
    }

}

