using UnityEngine;

public class Projectile : MonoBehaviour
{
	public void InitProjectile(float speed, Vector3 direction)
	{
		m_speed = speed;
		m_direction = direction.normalized;
	}

	#region Private

	private void Start()
	{
		Destroy(this, 5.0f);
	}

	private void FixedUpdate()
	{
		transform.Translate(m_direction * m_speed);
	}

	[SerializeField]
	private float m_speed = 3.0f;

	private Vector3 m_direction = Vector3.zero;

	#endregion Private
}