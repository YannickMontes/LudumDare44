using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float ContactDamage { get { return m_contactDamage; } }

	public bool HasSpotPlayer { get; private set; }

	public void TakeDamage(float damage)
	{
		m_life -= damage;
		if (m_life <= 0)
			Destroy(gameObject);
	}

	public void StopFollowingPlayer()
	{
		HasSpotPlayer = false;
		UnregisterPlayerMovement();
	}

	#region Private

	private void Awake()
	{
		HasSpotPlayer = false;
		m_splineWalker = GetComponent<SplineWalker>();
		UpdateCurrentTile();
	}

	private void Update()
	{
		if (m_isCollidingPlayer)
		{
			Character.Instance.TakeDamage(m_contactDamage);
		}
	}

	private void FixedUpdate()
	{
		if (HasSpotPlayer)
		{
			UpdateCurrentTile();
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
		if (collision.tag == "Player" && !HasSpotPlayer)
		{
			HasSpotPlayer = true;
			m_characterToFllow = collision.gameObject.GetComponent<Character>();
			m_characterToFllow.RegisterOnTileChange(OnCharacterTileChanged, true);
			UpdatePath();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Character.Instance.TakeDamage(m_contactDamage);
			m_splineWalker.ShouldFollowSpline = false;
			m_isCollidingPlayer = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			UpdatePath();
			m_splineWalker.ShouldFollowSpline = true;
			m_isCollidingPlayer = false;
		}
	}

	private void OnCharacterTileChanged(Vector3Int oldTile, Vector3Int newTile)
	{
		UpdatePath();
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

	private void UpdatePath()
	{
		if (m_characterToFllow != null)
		{
			List<Vector3> path = GridTile.Instance.GetPath(transform.position, m_characterToFllow.transform.position);
			path.RemoveAt(0);
			if (m_splineWalker.Spline == null)
			{
				m_splineWalker.Spline = path.Count > 0 ? (SplineManager.Instance.GetBezierSpline(path)) : null;
				m_splineWalker.Progress = 0.0f;
				m_splineWalker.ShouldFollowSpline = true;
			}
			else if (path.Count > 0)
			{
				m_splineWalker.Spline.CreateCurve(path);
				m_splineWalker.Progress = 0.0f;
				m_splineWalker.ShouldFollowSpline = true;
			}
		}
	}

	[SerializeField]
	private float m_life = 2.0f;
	[SerializeField]
	private float m_contactDamage = 1.0f;

	private SplineWalker m_splineWalker = null;
	private Vector3Int m_currentTile;
	private Character m_characterToFllow = null;
	private bool m_isCollidingPlayer = false;

	#endregion Private
}