using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
	public void InitUI(Character character)
	{
		m_nextRateOfFire = m_rateOfFireProgression.GetNextPowerUp(character.ShootManager.RateOfFireConfig) as RateOfFirePowerUpConfig;
		m_rateOfFireImage.sprite = m_nextRateOfFire != null ? m_nextRateOfFire.Icon : null;
		m_nextShootType = m_shotTypeProgression.GetNextPowerUp(character.ShootManager.ShotTypeConfig) as ShootTypePowerUpConfig;
		m_shotTypeImage.sprite = m_nextShootType != null ? m_nextShootType.Icon : null;
		m_nextSurvivability = m_survavibilityProgression.GetNextPowerUp(character.SurvivabilityPowerUp) as SurvivabilityPowerUpConfig;
		m_survavibilityImage.sprite = m_nextSurvivability != null ? m_nextSurvivability.Icon : null;
		gameObject.SetActive(true);
	}

	public void ImprovePowerUp(int type)
	{
		switch (type)
		{
			case 1:
				if (m_nextSurvivability != null)
					Character.Instance.SurvivabilityPowerUp = m_nextSurvivability;
				break;

			case 2:
				if (m_nextRateOfFire != null)
					Character.Instance.ShootManager.AssignRateOfFirePowerUp(m_nextRateOfFire);
				break;

			case 3:
				if (m_nextShootType != null)
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

	[Header("Sprite Locations")]
	[SerializeField]
	private Image m_rateOfFireImage = null;
	[SerializeField]
	private Image m_survavibilityImage = null;
	[SerializeField]
	private Image m_shotTypeImage = null;

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