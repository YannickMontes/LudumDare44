using UnityEngine;

public abstract class PowerUpConfig : ScriptableObject
{
	public abstract void Apply(Character character);

	public Sprite Icon { get { return m_icon; } }

	public int Price { get { return m_price; } }

	#region Private

	[SerializeField]
	private Sprite m_icon = null;
	[SerializeField]
	private int m_price = 0;

	#endregion Private
}