using UnityEngine;

public abstract class PowerUpConfig : ScriptableObject
{
	public abstract void Apply(Character character);
}