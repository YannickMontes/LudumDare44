using System;
using UnityEngine;

public abstract class PowerUpConfig : ScriptableObject
{
	public Sprite Icon { get { return m_icon; } }

	public int Price { get { return m_price; } }

	public PowerUpConfig NextLevel { get { return m_nextLevel; } }

	public abstract Type PowerUpType { get; }

	public abstract PowerUp CreatePowerUp();

	#region Private

	[SerializeField]
	private Sprite m_icon = null;
	[SerializeField]
	private int m_price = 0;
	[SerializeField]
	private PowerUpConfig m_nextLevel = null;

	#endregion Private
}