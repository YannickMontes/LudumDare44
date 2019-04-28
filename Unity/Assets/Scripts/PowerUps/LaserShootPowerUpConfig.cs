using UnityEngine;

public class LaserShootPowerUpConfig : ShootTypePowerUpConfig
{
	public float LaserRange { get { return m_laserRange; } }

	public override void Apply(Character character)
	{
		character.ShootManager.AssignShootTypePowerUp(this);
	}

	#region Private

	[SerializeField]
	private float m_laserRange = 10.0f;

	#endregion Private
}