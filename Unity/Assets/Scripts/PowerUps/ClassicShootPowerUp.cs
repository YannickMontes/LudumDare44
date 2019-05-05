public class ClassicShootPowerUp : PowerUp
{
	public new ClassicShootPowerUpConfig Config { get { return base.Config as ClassicShootPowerUpConfig; } }

	public ClassicShootPowerUp(PowerUpConfig config) : base(config)
	{
	}
}