using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
	public delegate void OnTileChanged(Vector3Int oldTile, Vector3Int newTile);

	public static Character Instance { get; private set; }

	public ShootManager ShootManager { get { return m_shootManager; } }

	public SurvivabilityPowerUpConfig SurvivabilityPowerUp { get { return m_survivabilityPowerUpConfig; } set { m_survivabilityPowerUpConfig = value; } }

	public void RegisterOnTileChange(OnTileChanged method, bool register)
	{
		if (register)
			m_tileChangedListeners += method;
		else
			m_tileChangedListeners -= method;
	}

	#region Private

	private void Awake()
	{
		m_shootManager = GetComponent<ShootManager>();
		UpdateCurrentTile();
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		HandleDirectionAnimator();
		HandleShootDirectionAnimator();
	}

	private void FixedUpdate()
	{
		transform.Translate(InputManager.Instance.Axis * m_speed * Time.fixedDeltaTime);
		UpdateCurrentTile();
		m_elapsedTimeSinceLastHit += Time.fixedDeltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Enemy")
		{
			Debug.Log("Player collide enemy");
			TakeDamage(collision.gameObject.GetComponent<Enemy>().ContactDamage);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.collider.tag == "Enemy")
		{
			if (m_elapsedTimeSinceLastHit >= m_invincibleTime)
				TakeDamage(collision.gameObject.GetComponent<Enemy>().ContactDamage);
		}
	}

	private void UpdateCurrentTile()
	{
		Vector3Int newTile = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
		if (newTile != m_currentTile)
		{
			m_tileChangedListeners?.Invoke(m_currentTile, newTile);
			m_currentTile = newTile;
		}
	}

	private void TakeDamage(float value)
	{
		m_animator.SetBool("IsHurt", true);
		GameManager.Instance.DecreaseTime(value);
		m_elapsedTimeSinceLastHit = 0.0f;
		StartCoroutine(FlickSprite());
	}

	private IEnumerator FlickSprite()
	{
		int cpt = 6;
		int current = 1;
		float step = m_invincibleTime / (float)cpt;
		while (current <= cpt)
		{
			float alpha = current % 2 == 0 ? 1.0f : 0.5f;
			m_spriteRenderer.color = new Color(m_spriteRenderer.color.r, m_spriteRenderer.color.g, m_spriteRenderer.color.b, alpha);
			yield return new WaitForSeconds(step);
			current++;
		}
		m_animator.SetBool("IsHurt", false);
	}

	private void HandleDirectionAnimator()
	{
		int horizontalMove = 0;
		int verticalMove = 0;
		if (InputManager.Instance.Axis != Vector2.zero)
		{
			if (Mathf.Abs(InputManager.Instance.Axis.x) > Mathf.Abs(InputManager.Instance.Axis.y))
			{
				horizontalMove = InputManager.Instance.Axis.x > 0 ? 1 : -1;
			}
			else
			{
				verticalMove = InputManager.Instance.Axis.y > 0 ? 1 : -1;
			}
		}
		m_animator.SetInteger("MoveHorizontal", horizontalMove);
		m_animator.SetInteger("MoveVertical", verticalMove);
	}

	private void HandleShootDirectionAnimator()
	{
		int shotHorizontal = 0;
		int shotVertical = 0;
		if (ShootManager.IsShooting)
		{
			shotHorizontal = (int)Mathf.Clamp(ShootManager.ShotDirection.x, -1, 1);
			shotVertical = (int)Mathf.Clamp(ShootManager.ShotDirection.y, -1, 1);
		}
		m_animator.SetBool("IsShooting", ShootManager.IsShooting);
		m_animator.SetInteger("ShootHorizontal", shotHorizontal);
		m_animator.SetInteger("ShootVertical", shotVertical);
	}

	[SerializeField]
	private float m_speed = 0.0f;
	[SerializeField]
	private float m_invincibleTime = 0.5f;
	[SerializeField]
	private SpriteRenderer m_spriteRenderer = null;
	[SerializeField]
	private SurvivabilityPowerUpConfig m_survivabilityPowerUpConfig = null;
	[SerializeField]
	private Animator m_animator = null;

	private ShootManager m_shootManager;
	private float m_elapsedTimeSinceLastHit = 0.0f;
	private OnTileChanged m_tileChangedListeners = null;
	private Vector3Int m_currentTile;

	#endregion Private
}