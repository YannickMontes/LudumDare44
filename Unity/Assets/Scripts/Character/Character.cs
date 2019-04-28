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

	private void FixedUpdate()
	{
		//	Vector2 input = new Vector3(Mathf.Abs(InputManager.Instance.Axis.x) < 0.5f ? InputManager.Instance.Axis.x / 2 : InputManager.Instance.Axis.x
		//		, Mathf.Abs(InputManager.Instance.Axis.y) < 0.5f ? InputManager.Instance.Axis.y / 2.0f : InputManager.Instance.Axis.y);
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
		if (m_elapsedTimeSinceLastHit >= m_invincibleTime)
			TakeDamage(collision.gameObject.GetComponent<Enemy>().ContactDamage);
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
	}

	[SerializeField]
	private float m_speed = 0.0f;
	[SerializeField]
	private float m_invincibleTime = 0.5f;
	[SerializeField]
	private SpriteRenderer m_spriteRenderer = null;
	[SerializeField]
	private SurvivabilityPowerUpConfig m_survivabilityPowerUpConfig = null;

	private ShootManager m_shootManager;
	private float m_elapsedTimeSinceLastHit = 0.0f;
	private OnTileChanged m_tileChangedListeners = null;
	private Vector3Int m_currentTile;

	#endregion Private
}