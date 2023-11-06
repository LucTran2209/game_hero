using UnityEngine;

public class SpikedBallRotation : MonoBehaviour
{
	public Transform checkpoint1; 
	public Transform checkpoint2; 
	public float rotationSpeed = 100.0f; 
	private Transform currentCheckpoint;
	private bool isRotatingRight = false;

	void Start()
	{
		currentCheckpoint = checkpoint1;
	}

	void Update()
	{
		float distance = Vector2.Distance(transform.position, currentCheckpoint.position);

		if (distance < 0.1f) 
		{
			isRotatingRight = !isRotatingRight;
			currentCheckpoint = isRotatingRight ? checkpoint2 : checkpoint1;
		}

		float rotationDirection = isRotatingRight ? -1.0f : +1.0f;
		transform.Rotate(Vector3.forward, rotationDirection * rotationSpeed * Time.deltaTime);
	}
}
