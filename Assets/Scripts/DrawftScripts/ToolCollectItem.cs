using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToolCollectItem : MonoBehaviour
{
    public bool enable = false;
    private Transform player;
    private List<Vector3> listItemPos;

    private float gravity;

    public Vector3 targetPos = new Vector3(355f, -35f);

    [ContextMenu("Hack")]
    public void Hack()
    {
        if (!enable) return;


        player = FindObjectOfType<PlayerHealth>().transform;

        listItemPos = FindObjectsOfType<ItemIdle>()
            .Select(item => item.transform.position).ToList();

        gravity = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;

        StartCoroutine(CollectItems());
    }

    [ContextMenu("Teleport")]
    public void Teleport()
    {
        player = FindObjectOfType<PlayerHealth>().transform;
        player.position = targetPos;
    }

    private IEnumerator CollectItems()
    {
        yield return new WaitForSecondsRealtime(1f);

        foreach (var itemPos in listItemPos)
        {
            Debug.Log("Player transport");
            player.position = itemPos;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        player.GetComponent<Rigidbody2D>().gravityScale = gravity;
        player.position = targetPos;

        Debug.LogWarning("Done");
    }
}
