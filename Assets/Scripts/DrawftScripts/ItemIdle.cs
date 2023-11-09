
using UnityEngine;

public class ItemIdle : MonoBehaviour
{
	private float initialY;

	void Start()
	{
		initialY = transform.position.y;
	}
	void Update()
	{
		float maxHeight = 0.2f;
		float frequency = 6.0f;
		float verticalOffset = Mathf.Sin(Time.time * frequency) * maxHeight;
		Vector2 newPosition = new Vector2(transform.position.x, initialY + verticalOffset); 
		transform.position = newPosition;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			int count = PlayerPrefs.GetInt("QuantityItem");
			
			count++;
			PlayerPrefs.SetInt("QuantityItem", count);
			gameObject.SetActive(false);
		}
	}
}
