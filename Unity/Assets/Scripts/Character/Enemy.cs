using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float ContactDamage { get { return m_contactDamage; } }

	public void TakeDamage(float damage)
	{
		m_life -= damage;
		if (m_life <= 0)
			Destroy(gameObject);
	}

	public void StopFollowingPlayer()
	{
		m_spotPlayer = false;
		m_currentTarget = -1;
		UnregisterPlayerMovement();
	}

	#region Private

	private void Awake()
	{
		UpdateCurrentTile();
	}

	private void FixedUpdate()
	{
		if (m_spotPlayer)
		{
			FollowPlayer();
		}
	}

	private void OnDestroy()
	{
		UnregisterPlayerMovement();
	}

	private void UnregisterPlayerMovement()
	{
		if (m_characterToFllow != null)
			m_characterToFllow.RegisterOnTileChange(OnCharacterTileChanged, false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" && !m_spotPlayer)
		{
			m_spotPlayer = true;
			m_characterToFllow = collision.gameObject.GetComponent<Character>();
			m_characterToFllow.RegisterOnTileChange(OnCharacterTileChanged, true);
			UpdatePath();
		}
	}

	private void OnCharacterTileChanged(Vector3Int oldTile, Vector3Int newTile)
	{
		UpdatePath(false);
	}

	private void FollowPlayer()
	{
		if (m_currentTarget != -1)
		{
			Vector3 direction = m_pathToPlayer[m_currentTarget] - transform.position;
			if (direction.magnitude <= 0.1f)
			{
				m_currentTarget = m_currentTarget + 1 < m_pathToPlayer.Count ? m_currentTarget + 1 : -1;
			}
			if (m_currentTarget != -1)
			{
				direction = m_pathToPlayer[m_currentTarget] - transform.position;
				transform.Translate(direction.normalized * m_speed * Time.fixedDeltaTime);
				UpdateCurrentTile();
			}
		}
	}

	private void UpdateCurrentTile()
	{
		Vector3Int newTile = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
		if (newTile != m_currentTile)
		{
			m_currentTile = newTile;
			UpdatePath();
		}
	}

	private void UpdatePath(bool resetTarget = true)
	{
		if (m_characterToFllow != null)
		{
			m_pathToPlayer = GridTile.Instance.GetPath(transform.position, m_characterToFllow.transform.position);
			if (resetTarget)
				m_currentTarget = 0;
		}
	}

	private void OnDrawGizmos()
	{
		if (m_pathToPlayer != null)
		{
			foreach (Vector3 pos in m_pathToPlayer)
			{
				Gizmos.DrawSphere(pos, 0.2f);
			}
		}
	}

	[SerializeField]
	private float m_speed = 1.0f;
	[SerializeField]
	private float m_life = 2.0f;
	[SerializeField]
	private float m_contactDamage = 1.0f;

	private bool m_spotPlayer = false;
	private Vector3Int m_currentTile;
	private Character m_characterToFllow = null;
	private List<Vector3> m_pathToPlayer = null;
	private int m_currentTarget = 0;

	#endregion Private
}