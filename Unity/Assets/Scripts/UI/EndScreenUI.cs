using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
	public void InitUI(Character character)
	{
		m_rateOfFireImage.sprite = m_rateOfFireProgression.GetNextPowerUp(character.ShootManager.RateOfFireConfig).Icon;
		m_shotTypeImage.sprite = m_shotTypeProgression.GetNextPowerUp(character.ShootManager.ShotTypeConfig).Icon;
		m_survavibilityImage.sprite = m_survavibilityProgression.GetNextPowerUp(character.SurvivabilityPowerUp).Icon;
		gameObject.SetActive(true);
	}

	public void ImprovePowerUp(int type)
	{
		switch (type)
		{
			case 1:
				SurvivabilityPowerUpConfig survivability = m_survavibilityProgression.GetNextPowerUp(Character.Instance.SurvivabilityPowerUp) as SurvivabilityPowerUpConfig;
				if (survivability != null)
					Character.Instance.SurvivabilityPowerUp = survivability;
				break;

			case 2:
				RateOfFirePowerUpConfig rateOfFire = m_rateOfFireProgression.GetNextPowerUp(Character.Instance.ShootManager.RateOfFireConfig) as RateOfFirePowerUpConfig;
				Character.Instance.ShootManager.AssignRateOfFirePowerUp(rateOfFire);
				break;

			case 3:
				ShootTypePowerUpConfig shoot = m_shotTypeProgression.GetNextPowerUp(Character.Instance.ShootManager.ShotTypeConfig) as ShootTypePowerUpConfig;
				Character.Instance.ShootManager.AssignShootTypePowerUp(shoot);
				break;
		}

		GameManager.Instance.LoadNextLevel();
	}

	#region Private

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

	#endregion Private
}