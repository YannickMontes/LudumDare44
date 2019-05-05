public abstract class PowerUp
{
	public PowerUpConfig Config { get { return m_config; } }

	public PowerUp(PowerUpConfig config)
	{
		m_config = config;
	}

	public virtual void UpgradeConfig()
	{
		m_config = Config.NextLevel;
	}

	#region Private

	private PowerUpConfig m_config = null;

	#endregion Private
}