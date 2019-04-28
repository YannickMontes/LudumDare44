using UnityEngine;

public class Enemy : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			m_spotPlayer = true;
			m_playerToFollow = other.gameObject;
		}
	}

	#region Private

	private void FixedUpdate()
	{
		if (m_spotPlayer)
		{
			FollowPlayer();
		}
	}

	private void FollowPlayer()
	{
		Vector3 direction = m_playerToFollow.transform.position - transform.position;
		transform.Translate(direction.normalized * m_speed * Time.fixedDeltaTime);
	}

	[SerializeField]
	private float m_speed = 1.0f;

	private bool m_spotPlayer = false;
	private GameObject m_playerToFollow = null;

	#endregion Private
}