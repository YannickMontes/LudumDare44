using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
	public void Init(EndScreenUI parent, PowerUp powerUp)
	{
		m_parent = parent;
		bool hasNextLevel = powerUp.Config.NextLevel != null;
		if (hasNextLevel)
		{
			m_image.sprite = powerUp.Config.NextLevel.Icon;
			m_price.text = powerUp.Config.NextLevel.Price.ToString();
		}
		m_button.interactable = hasNextLevel;
		m_powerUp = powerUp;
	}

	public void OnButtonClick()
	{
		m_powerUp.UpgradeConfig();
		m_parent.DisableEndScreenUI();
	}

	#region Private

	[SerializeField]
	private Button m_button = null;
	[SerializeField]
	private Image m_image = null;
	[SerializeField]
	private Text m_price = null;

	private EndScreenUI m_parent = null;
	private PowerUp m_powerUp = null;

	#endregion Private
}