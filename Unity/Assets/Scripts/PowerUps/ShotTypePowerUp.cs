public class ShotTypePowerUp : PowerUp
{
	public new ShootTypePowerUpConfig Config { get { return base.Config as ShootTypePowerUpConfig; } }

	public ShotTypePowerUp(PowerUpConfig config) : base(config)
	{
	}
}