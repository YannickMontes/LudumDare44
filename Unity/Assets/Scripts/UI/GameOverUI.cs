using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	public void ReloadGame()
	{
		GameManager.Instance.Restart();
		gameObject.SetActive(false);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}