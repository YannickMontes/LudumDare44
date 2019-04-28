using UnityEngine;

public abstract class ShootTypePowerUpConfig : PowerUpConfig
{
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