using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyMoving : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.transform.SetParent(this.transform);
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.transform.SetParent(null);
		}
	}
}
