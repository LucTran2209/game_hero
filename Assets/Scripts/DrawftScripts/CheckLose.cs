using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckLose : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.GetComponent<PlayerHealth>().DeadWhenFall();
		}
	}
}
