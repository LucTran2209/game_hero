using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    Collider2D collider2D;
    public List<Collider2D> getDetectionColliders = new List<Collider2D>();



    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") getDetectionColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player") getDetectionColliders.Remove(collision);
    }
}
