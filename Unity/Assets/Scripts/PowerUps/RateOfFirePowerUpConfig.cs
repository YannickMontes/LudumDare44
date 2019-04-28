using UnityEngine;

[CreateAssetMenu(menuName = "Game/PowerUps/RateOfFire")]
public class RateOfFirePowerUpConfig : PowerUpConfig
{
	public float CooldownShot { get { return m_cooldownShot; } }

	public override void Apply(Character character)
	{
		character.ShootManager.AssignRateOfFirePowerUp(this);
	}

	#region Private

	[SerializeField]
	private float m_cooldownShot = 0.5f;

	#endregion Private
}