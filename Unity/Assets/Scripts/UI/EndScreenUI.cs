using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
	public void InitUI(Character character)
	{
		m_rateOfFireImage.sprite = m_rateOfFireProgression.GetNextPowerUp(character.ShootManager.RateOfFireConfig).Icon;
		m_shotTypeImage.sprite = m_shotTypeProgression.GetNextPowerUp(character.ShootManager.ShotTypeConfig).Icon;
		m_survavibilityImage.sprite = m_survavibilityProgression.GetNextPowerUp(character.SurvivabilityPowerUp).Icon;
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