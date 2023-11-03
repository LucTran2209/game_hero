using UnityEngine;
using UnityEngine.SceneManagement;

public class NguoiLunSceneController : MonoBehaviour
{
	public void OnReplayButton()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void OnHomeButton()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}
}
