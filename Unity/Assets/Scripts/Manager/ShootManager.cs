using UnityEngine;

public class ShootManager
{
	public void AssignShootTypePowerUp(ShootTypePowerUpConfig shootTypeConfig)
	{
		m_shotTypeConfig = shootTypeConfig;
	}

	public void Shoot(bool shotPressed, Character character)
	{
		if (m_shotTypeConfig is ClassicShootPowerUpConfig)
		{
			if (shotPressed && m_lastShotElapsedTime >= character.CooldownShot)
			{
				Vector2 worldPoint = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
				//RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, , 500.0f, 1 << 8);
				//if (hit)
				//{
				Vector2 direction = worldPoint - (Vector2)character.transform.position;
				Debug.DrawLine(character.transform.position, direction, Color.blue, 5.0f);
				GameObject.Instantiate(character.ProjectilePrefab).GetComponent<Projectile>().InitProjectile(1.0f, ConvertToClosestCardinalDirection(direction));
				m_lastShotElapsedTime = 0.0f;
				//}
			}
		}
	}

	public void Update(float deltaTime)
	{
		m_lastShotElapsedTime += deltaTime;
	}

	#region Private

	private Vector2 ConvertToClosestCardinalDirection(Vector2 position)
	{
		if (Mathf.Abs(position.x) > Mathf.Abs(position.y))
		{
			if (position.x > 0.0f)
				return Vector2.right;
			else
				return Vector2.left;
		}
		else
		{
			if (position.y > 0.0f)
				return Vector2.up;
			else
				return Vector2.down;
		}
	}

	private ShootTypePowerUpConfig m_shotTypeConfig = null;
	protected float m_lastShotElapsedTime = float.MaxValue;

	#endregion Private
}