public class LaserShootPowerUp : PowerUp
{
	public new LaserShootPowerUpConfig Config { get { return base.Config as LaserShootPowerUpConfig; } }

	public LaserShootPowerUp(PowerUpConfig config) : base(config)
	{
	}
}