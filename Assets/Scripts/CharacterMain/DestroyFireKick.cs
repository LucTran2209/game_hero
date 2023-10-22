using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFireKick : MonoBehaviour
{
    public float destroyDelay = 2.0f;
    private float speed = 5f;

    void Start()
    {

        // Gọi hàm Destroy sau khoảng thời gian được xác định (destroyDelay)
        Invoke("DestroyObject", destroyDelay);
    }

    void DestroyObject()
    {
        // Hủy game object hiện tại
        Destroy(gameObject);
    }
}
