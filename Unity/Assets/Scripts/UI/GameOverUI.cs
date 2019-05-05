using System.Collections;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	public void ReloadGame()
	{
		StartCoroutine(DisableAndRestart());
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private IEnumerator DisableAndRestart()
	{
		GameManager.Instance.Restart();
		yield return null;
		gameObject.SetActive(false);
	}
}