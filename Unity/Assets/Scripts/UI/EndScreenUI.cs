using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
	public void InitUI(Character character)
	{
		m_avaibleMoney.text = "Avaible Money : " + (int)GameManager.Instance.CurrentTimer;

		foreach (PowerUp powerUp in character.PowerUpManager.PowerUps)
		{
			PowerUpUI powerUpUI = GameObject.Instantiate(m_powerUpUI, m_listParent).GetComponent<PowerUpUI>();
			powerUpUI.Init(this, powerUp);
		}

		gameObject.SetActive(true);
	}

	public void DisableEndScreenUI()
	{
		StartCoroutine(DisableAndLoadNextLevel());
	}

	#region Private

	private IEnumerator DisableAndLoadNextLevel()
	{
		GameManager.Instance.LoadNextLevel();
		yield return null;
		gameObject.SetActive(false);
	}

	[SerializeField]
	private Text m_avaibleMoney = null;
	[SerializeField]
	private GameObject m_powerUpUI = null;
	[SerializeField]
	private Transform m_listParent = null;

	#endregion Private
}