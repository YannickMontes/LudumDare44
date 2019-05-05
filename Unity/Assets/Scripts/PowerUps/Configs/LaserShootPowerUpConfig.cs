using System;
using UnityEngine;

public class LaserShootPowerUpConfig : ShootTypePowerUpConfig
{
	public float LaserRange { get { return m_laserRange; } }

	public override Type PowerUpType { get { return typeof(LaserShootPowerUp); } }

	public override PowerUp CreatePowerUp()
	{
		return new LaserShootPowerUp(this);
	}

	#region Private

	[SerializeField]
	private float m_laserRange = 10.0f;

	#endregion Private
}