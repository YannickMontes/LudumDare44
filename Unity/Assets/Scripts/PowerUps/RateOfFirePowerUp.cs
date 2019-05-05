public class RateOfFirePowerUp : PowerUp
{
	public new RateOfFirePowerUpConfig Config { get { return base.Config as RateOfFirePowerUpConfig; } }

	public RateOfFirePowerUp(PowerUpConfig config) : base(config)
	{
	}
}