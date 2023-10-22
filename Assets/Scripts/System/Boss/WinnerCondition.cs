using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerCondition : MonoBehaviour
{
    [SerializeField] GameObject panelWinner;
    [SerializeField] AudioSource audioWinner;

    // Update is called once per frame
    void Update()
    {
        AttributeManager health = GetComponent<AttributeManager>();
        if (health.IsDeadth())
        {
                   
            StartCoroutine(Winner(2f));
            
        }
        
    }

    IEnumerator Winner (float time)
    {
        
        yield return new WaitForSeconds(time);
        audioWinner.Play();
        panelWinner.SetActive(true);
        Time.timeScale = 0f;
    }
}
