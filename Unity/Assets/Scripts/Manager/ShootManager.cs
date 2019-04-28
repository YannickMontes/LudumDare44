using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
	public void AssignShootTypePowerUp(ShootTypePowerUpConfig shootTypeConfig)
	{
		m_shotTypeConfig = shootTypeConfig;
	}

	#region Private

	private void Awake()
	{
		m_character = GetComponent<Character>();
	}

	private void Start()
	{
		InputManager.Instance.RegisterOnShootInput(Shoot, true);
	}

	private void Update()
	{
		m_lastShotElapsedTime += Time.deltaTime;
	}

	private void OnDestroy()
	{
		InputManager.Instance.RegisterOnShootInput(Shoot, false);
	}

	public void Shoot(bool shotPressed)
	{
		if (m_shotTypeConfig is ClassicShootPowerUpConfig)
		{
			if (shotPressed && m_lastShotElapsedTime >= m_cooldownShot)
			{
				Vector3 worldPoint = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
				worldPoint.z = 0.0f;
				Vector3 direction = worldPoint - transform.position;
				LaunchProjectiles(ConvertToClosestCardinalDirection(direction));
				m_lastShotElapsedTime = 0.0f;
			}
		}
	}

	private void LaunchProjectiles(Vector2 direction)
	{
		RotateParentEmmiter(direction);

		ClassicShootPowerUpConfig shotConfig = m_shotTypeConfig as ClassicShootPowerUpConfig;
		switch (shotConfig.Emmiter)
		{
			case ShootTypePowerUpConfig.EmmiterType.SINGLE:
				GameObject.Instantiate(m_projectile, m_defaultEmmiter.transform.position, m_defaultEmmiter.transform.rotation).GetComponent<Projectile>().InitProjectile(1.0f, shotConfig.ShootPower);
				break;

			case ShootTypePowerUpConfig.EmmiterType.ALL:
				foreach (GameObject emmiter in m_emmiters)
				{
					GameObject.Instantiate(m_projectile, emmiter.transform.position, emmiter.transform.rotation).GetComponent<Projectile>().InitProjectile(1.0f, shotConfig.ShootPower);
				}
				break;
		}
	}

	private void RotateParentEmmiter(Vector2 direction)
	{
		if (direction == Vector2.up)
		{
			m_emmiterParent.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
		}
		else if (direction == Vector2.down)
		{
			m_emmiterParent.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
		}
		else if (direction == Vector2.right)
		{
			m_emmiterParent.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		}
		else if (direction == Vector2.left)
		{
			m_emmiterParent.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
		}
	}

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

	[SerializeField]
	private ShootTypePowerUpConfig m_shotTypeConfig = null;
	[SerializeField]
	private GameObject m_projectile = null;
	[SerializeField]
	private float m_cooldownShot = 0.5f;

	[Header("Emmiters Config")]
	[SerializeField]
	private GameObject m_emmiterParent = null;
	[SerializeField]
	private GameObject m_defaultEmmiter = null;
	[SerializeField]
	private List<GameObject> m_emmiters = null;

	private Character m_character = null;
	private float m_lastShotElapsedTime = float.MaxValue;

	#endregion Private
}