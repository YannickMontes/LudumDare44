using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance
	{
		get;
		private set;
	}

	public float CurrentTimer
	{
		get
		{
			return m_timer;
		}
	}

	public void DecreaseTime(float value)
	{
		m_timer -= value;
		if (m_timer <= 0)
		{
			GameOver();
		}
	}

	#region Private

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.Log("GameManager already instanced. Destroying this object.");
			Destroy(this);
		}
		else
		{
			DontDestroyOnLoad(this);
			Instance = this;
		}
	}

	private void Start()
	{
		m_timer = MAX_TIMER;
	}

	private void Update()
	{
		DecreaseTime((MAX_TIMER / m_timeToFinishLevel) * Time.deltaTime);
	}

	private void GameOver()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	[SerializeField]
	private float m_timeToFinishLevel = 30.0f;

	private float m_timer = MAX_TIMER;

	public const int MAX_TIMER = 100;

	#endregion Private
}