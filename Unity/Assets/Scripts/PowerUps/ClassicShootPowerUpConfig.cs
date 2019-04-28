using UnityEngine;

[CreateAssetMenu(menuName = "Game/PowerUps/ClassicShotConfig")]
public class ClassicShootPowerUpConfig : ShootTypePowerUpConfig
{
	public Sprite Sprite
	{
		get
		{
			return m_shotSrite;
		}
	}

	public EmmiterType Emmiter
	{
		get
		{
			return m_emmiterType;
		}
	}

	public override void Apply(Character character)
	{
		character.ShootManager.AssignShootTypePowerUp(this);
	}

	#region Private

	[SerializeField]
	private Sprite m_shotSrite = null;
	[SerializeField]
	private EmmiterType m_emmiterType = default(EmmiterType);

	#endregion Private
}