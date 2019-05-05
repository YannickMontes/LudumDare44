using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootManager))]
[RequireComponent(typeof(PowerUpManager))]
public class Character : MonoBehaviour
{
	public delegate void OnTileChanged(Vector3Int oldTile, Vector3Int newTile);

	public static Character Instance { get; private set; }

	public ShootManager ShootManager { get { return m_shootManager; } }

	public PowerUpManager PowerUpManager { get { return m_powerUpManager; } }

	public void RegisterOnTileChange(OnTileChanged method, bool register)
	{
		if (register)
			m_tileChangedListeners += method;
		else
			m_tileChangedListeners -= method;
	}

	public void TakeDamage(float value)
	{
		if (m_elapsedTimeSinceLastHit <= m_invincibleTime)
			return;
		m_animator.SetBool("IsHurt", true);
		GameManager.Instance.DecreaseTime(value);
		m_elapsedTimeSinceLastHit = 0.0f;
		StartCoroutine(FlickSprite());
	}

	#region Private

	private void Awake()
	{
		UpdateCurrentTile();
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
			m_shootManager = GetComponent<ShootManager>();
			m_powerUpManager = GetComponent<PowerUpManager>();
			m_powerUpManager.Init();
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

	private void UpdateCurrentTile()
	{
		Vector3Int newTile = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
		if (newTile != m_currentTile)
		{
			m_tileChangedListeners?.Invoke(m_currentTile, newTile);
			m_currentTile = newTile;
		}
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
		if (InputManager.Instance.RawAxis != Vector2.zero)
		{
			if (Mathf.Abs(InputManager.Instance.RawAxis.x) > Mathf.Abs(InputManager.Instance.RawAxis.y))
			{
				horizontalMove = InputManager.Instance.RawAxis.x > 0 ? 1 : -1;
			}
			else
			{
				verticalMove = InputManager.Instance.RawAxis.y > 0 ? 1 : -1;
			}
		}
		//m_spriteRenderer.flipX = horizontalMove == 1;
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
			//m_spriteRenderer.flipX = shotHorizontal == 1;
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
	private Animator m_animator = null;

	private ShootManager m_shootManager = null;
	private PowerUpManager m_powerUpManager = null;
	private float m_elapsedTimeSinceLastHit = 0.0f;
	private OnTileChanged m_tileChangedListeners = null;
	private Vector3Int m_currentTile;

	#endregion Private
}