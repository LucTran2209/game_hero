using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class HitBoxCustomeTrigger : MonoBehaviour
{
    public event Action<Collider2D> onCustomTriggerEnter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            onCustomTriggerEnter?.Invoke(other);
        }
    }
}
