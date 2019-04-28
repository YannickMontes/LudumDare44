using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public const int MAX_TIMER = 100;

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

	public void PlayerReachedEndOfLevel()
	{
		m_decreaseTimer = false;
		InputManager.Instance.Enable = false;
		UIManager.Instance.EndScreen.InitUI(Character.Instance);
		DisableEnemies();
	}

	public void Restart()
	{
		Destroy(Character.Instance.gameObject);
		LoadLevel(0);
	}

	public void LoadNextLevel()
	{
		LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
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

	private void LoadLevel(int index)
	{
		Scene scene = SceneManager.GetSceneByBuildIndex(index);
		if (scene.IsValid())
		{
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.buildIndex);
			StartCoroutine(WaitBeforeStart(asyncOperation));
		}
		else
		{
			Debug.Log("Build index not found");
		}
	}

	private void Start()
	{
		StartCoroutine(WaitBeforeStart(null));
	}

	private void Update()
	{
		if (m_decreaseTimer)
			DecreaseTime((MAX_TIMER / m_timeToFinishLevel) * Time.deltaTime);
	}

	private void GameOver()
	{
		InputManager.Instance.Enable = false;
		DisableEnemies();
		UIManager.Instance.GameOver.gameObject.SetActive(true);
	}

	private void DisableEnemies()
	{
		Enemy[] enemies = FindObjectsOfType<Enemy>();//Horriblement sale
		foreach (Enemy enemy in enemies)
		{
			enemy.StopFollowingPlayer();
		}
	}

	private IEnumerator WaitBeforeStart(AsyncOperation isLoadingScene)
	{
		if (isLoadingScene != null)
		{
			while (!isLoadingScene.isDone)
			{
				yield return null;
			}
		}
		InputManager.Instance.Enable = true;
		m_timer = MAX_TIMER;
		m_decreaseTimer = true;
	}

	[SerializeField]
	private float m_timeToFinishLevel = 30.0f;

	private float m_timer = MAX_TIMER;
	private bool m_decreaseTimer = false;

	#endregion Private
}