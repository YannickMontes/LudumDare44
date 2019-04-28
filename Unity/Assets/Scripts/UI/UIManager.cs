using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }

	public GameOverUI GameOver { get { return m_gameOver; } }
	public EndScreenUI EndScreen { get { return m_endScreen; } }

	#region Private

	private void Awake()
	{
		if (Instance != null)
			Destroy(Instance.gameObject);
		Instance = this;
	}

	[SerializeField]
	private GameOverUI m_gameOver = null;
	[SerializeField]
	private EndScreenUI m_endScreen = null;

	#endregion Private
}