using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	public void ReloadGame()
	{
		GameManager.Instance.Restart();
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}