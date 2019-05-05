using UnityEngine;

public abstract class ShootTypePowerUpConfig : PowerUpConfig
{
	public int ShootPower { get { return m_shootPower; } }

	public enum EmmiterType
	{
		SINGLE,
		ALL
	}

	#region Private

	[SerializeField]
	private int m_shootPower = 1;

	#endregion Private
}