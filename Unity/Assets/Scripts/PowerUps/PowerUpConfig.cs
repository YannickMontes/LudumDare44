using UnityEngine;

public abstract class PowerUpConfig : ScriptableObject
{
	public abstract void Apply(Character character);

	public Sprite Icon
	{
		get
		{
			return m_icon;
		}
	}

	#region Private

	[SerializeField]
	private Sprite m_icon = null;

	#endregion Private
}