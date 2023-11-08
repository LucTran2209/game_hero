using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    // Start is called before the first frame update
    private void DeActive()
    {
        Destroy(gameObject);
    }
}
