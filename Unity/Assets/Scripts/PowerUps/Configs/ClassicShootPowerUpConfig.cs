using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/PowerUps/ClassicShotConfig")]
public class ClassicShootPowerUpConfig : ShootTypePowerUpConfig
{
	public Sprite Sprite { get { return m_shotSrite; } }

	public EmmiterType Emmiter { get { return m_emmiterType; } }

	public override Type PowerUpType { get { return typeof(ClassicShootPowerUp); } }

	public override PowerUp CreatePowerUp()
	{
		return new ClassicShootPowerUp(this);
	}

	#region Private

	[SerializeField]
	private Sprite m_shotSrite = null;
	[SerializeField]
	private EmmiterType m_emmiterType = default(EmmiterType);

	#endregion Private
}