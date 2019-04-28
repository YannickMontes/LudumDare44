using UnityEngine;

public class Character : MonoBehaviour
{
	public ShootManager ShootManager { get { return m_shootManager; } }

	#region Private

	private void Awake()
	{
		m_shootManager = GetComponent<ShootManager>();
	}

	private void FixedUpdate()
	{
		transform.Translate(InputManager.Instance.Axis * m_speed * Time.fixedDeltaTime);
	}

	[SerializeField]
	private float m_speed = 0.0f;

	private ShootManager m_shootManager;

	#endregion Private
}