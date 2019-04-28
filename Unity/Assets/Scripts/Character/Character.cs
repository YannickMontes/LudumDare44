using UnityEngine;

public class Character : MonoBehaviour
{
	public float CooldownShot
	{
		get
		{
			return m_cooldownShot;
		}
	}

	public GameObject ProjectilePrefab
	{
		get
		{
			return m_projectile;
		}
	}

	public ShootManager ShootManager
	{
		get
		{
			return m_shootManager;
		}
	}

	#region Private

	private void Start()
	{
		m_shootManager.AssignShootTypePowerUp(m_defaultShotTypeConfig);
		InputManager.Instance.RegisterOnShootInput(OnShootInput, true);
	}

	private void FixedUpdate()
	{
		transform.Translate(InputManager.Instance.Axis * m_speed * Time.fixedDeltaTime);
	}

	private void Update()
	{
		m_shootManager.Update(Time.deltaTime);
	}

	private void OnShootInput(bool pressed)
	{
		m_shootManager.Shoot(pressed, this);
	}

	[SerializeField]
	private float m_speed = 0.0f;
	[SerializeField]
	private GameObject m_projectile = null;
	[SerializeField]
	private float m_cooldownShot = 0.5f;
	[SerializeField]
	private ShootTypePowerUpConfig m_defaultShotTypeConfig = null;

	private ShootManager m_shootManager = new ShootManager();

	#endregion Private
}