public class SurvivabilityPowerUp : PowerUp
{
	public new SurvivabilityPowerUpConfig Config { get { return base.Config as SurvivabilityPowerUpConfig; } }

	public SurvivabilityPowerUp(PowerUpConfig config) : base(config)
	{
	}
}