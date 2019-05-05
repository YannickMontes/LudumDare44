using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/PowerUps/RateOfFire")]
public class RateOfFirePowerUpConfig : PowerUpConfig
{
	public float CooldownShot { get { return m_cooldownShot; } }
	public override Type PowerUpType { get { return typeof(RateOfFirePowerUp); } }

	public override PowerUp CreatePowerUp()
	{
		return new RateOfFirePowerUp(this);
	}

	#region Private

	[SerializeField]
	private float m_cooldownShot = 0.5f;

	#endregion Private
}