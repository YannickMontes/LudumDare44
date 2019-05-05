using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/PowerUps/ProgressionList")]
public class PowerUpProgressionConfig : ScriptableObject
{
	public PowerUpConfig GetNextPowerUp(PowerUpConfig currentOne)
	{
		int currentIndex = m_powerUpProgression.IndexOf(currentOne);
		if (currentIndex == -1)
		{
			Debug.LogError("Current config not found in progression");
			return null;
		}
		if (currentIndex == m_powerUpProgression.Count - 1)
		{
			Debug.LogWarning("Max power up reached.");
			return null;
		}
		return m_powerUpProgression[currentIndex + 1];
	}

	#region Private

	[SerializeField]
	private List<PowerUpConfig> m_powerUpProgression = null;

	#endregion Private
}