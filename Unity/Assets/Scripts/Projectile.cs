using UnityEngine;

public class Projectile : MonoBehaviour
{
	public void InitProjectile(float speed, float power)
	{
		m_speed = speed;
		m_power = power;
	}

	#region Private

	private void Start()
	{
		Destroy(gameObject, 5.0f);
	}

	private void FixedUpdate()
	{
		transform.Translate(Vector3.right * m_speed);
	}

	[SerializeField]
	private float m_speed = 3.0f;

	private float m_power = 0.0f;

	#endregion Private
}