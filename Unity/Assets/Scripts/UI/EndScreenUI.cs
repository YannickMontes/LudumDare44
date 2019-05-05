using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
	public void InitUI(Character character)
	{
		m_avaibleMoney.text = "Avaible Money : " + (int)GameManager.Instance.CurrentTimer;

		m_nextRateOfFire = m_rateOfFireProgression.GetNextPowerUp(character.ShootManager.RateOfFireConfig) as RateOfFirePowerUpConfig;
		m_rateOfFireImage.sprite = m_nextRateOfFire != null ? m_nextRateOfFire.Icon : null;
		m_rateOfFirePrice.text = m_nextRateOfFire != null ? m_nextRateOfFire.Price.ToString() : "";

		m_nextShootType = m_shotTypeProgression.GetNextPowerUp(character.ShootManager.ShotTypeConfig) as ShootTypePowerUpConfig;
		m_shotTypeImage.sprite = m_nextShootType != null ? m_nextShootType.Icon : null;
		m_shotTypePrice.text = m_nextShootType != null ? m_nextShootType.Price.ToString() : "";

		m_nextSurvivability = m_survavibilityProgression.GetNextPowerUp(character.SurvivabilityPowerUp) as SurvivabilityPowerUpConfig;
		m_survavibilityImage.sprite = m_nextSurvivability != null ? m_nextSurvivability.Icon : null;
		m_survavibilityPrice.text = m_nextSurvivability != null ? m_nextSurvivability.Price.ToString() : "";

		gameObject.SetActive(true);
	}

	public void ImprovePowerUp(int type)
	{
		switch (type)
		{
			case 1:
				if (m_nextSurvivability != null && GameManager.Instance.CurrentTimer >= m_nextSurvivability.Price)
					Character.Instance.SurvivabilityPowerUp = m_nextSurvivability;
				break;

			case 2:
				if (m_nextRateOfFire != null && GameManager.Instance.CurrentTimer >= m_nextRateOfFire.Price)
					Character.Instance.ShootManager.AssignRateOfFirePowerUp(m_nextRateOfFire);
				break;

			case 3:
				if (m_nextShootType != null && GameManager.Instance.CurrentTimer >= m_nextShootType.Price)
					Character.Instance.ShootManager.AssignShootTypePowerUp(m_nextShootType);
				break;
		}
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

	[Header("Sprite Locations")]
	[SerializeField]
	private Image m_rateOfFireImage = null;
	[SerializeField]
	private Image m_survavibilityImage = null;
	[SerializeField]
	private Image m_shotTypeImage = null;

	[Header("Text Locations")]
	[SerializeField]
	private Text m_rateOfFirePrice = null;
	[SerializeField]
	private Text m_survavibilityPrice = null;
	[SerializeField]
	private Text m_shotTypePrice = null;

	[Header("PowerUps Progressions")]
	[SerializeField]
	private PowerUpProgressionConfig m_rateOfFireProgression = null;
	[SerializeField]
	private PowerUpProgressionConfig m_survavibilityProgression = null;
	[SerializeField]
	private PowerUpProgressionConfig m_shotTypeProgression = null;

	private SurvivabilityPowerUpConfig m_nextSurvivability = null;
	private RateOfFirePowerUpConfig m_nextRateOfFire = null;
	private ShootTypePowerUpConfig m_nextShootType = null;

	#endregion Private
}