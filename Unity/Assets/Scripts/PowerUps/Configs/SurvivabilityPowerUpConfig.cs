using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/PowerUps/Survivability")]
public class SurvivabilityPowerUpConfig : PowerUpConfig
{
	public float TimerReductionFactor { get { return m_timerReductionFactor; } }
	public float ReducedDamageFromEnemies { get { return m_reducedDamageFromEnemies; } }
	public override Type PowerUpType { get { return typeof(SurvivabilityPowerUp); } }

	public override PowerUp CreatePowerUp()
	{
		return new SurvivabilityPowerUp(this);
	}

	#region Private

	[SerializeField]
	private float m_timerReductionFactor = 1.0f;
	[SerializeField]
	private float m_reducedDamageFromEnemies = 0.0f;

	#endregion Private
}