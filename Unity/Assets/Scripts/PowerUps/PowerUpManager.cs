using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
	public IReadOnlyList<PowerUp> PowerUps { get { return m_powerUps.AsReadOnly(); } }

	public void Init()
	{
		foreach (PowerUpConfig config in m_defaultPowerUpsConfigs)
		{
			m_powerUps.Add(config.CreatePowerUp());
		}
	}

	public PowerUpConfig GetPowerUpConfig(Type type)
	{
		foreach (PowerUp powerUp in m_powerUps)
		{
			if (powerUp.Config.GetType() == type || powerUp.Config.GetType().IsSubclassOf(type))
				return powerUp.Config;
		}
		return null;
	}

	public void UpgradePowerUp(Type type)
	{
		foreach (PowerUp powerUp in m_powerUps)
		{
			if (powerUp.Config.GetType() == type)
			{
				powerUp.UpgradeConfig();
			}
		}
	}

	#region Private

	[SerializeField]
	private List<PowerUpConfig> m_defaultPowerUpsConfigs = new List<PowerUpConfig>();

	private List<PowerUp> m_powerUps = new List<PowerUp>();

	#endregion Private
}