using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	public void Init()
	{
		Character.Instance.transform.position = m_mapStart.transform.position;
		m_virtualCam.Follow = Character.Instance.transform;
	}

	#region Private

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.Log("New LevelManager instancied, destroying old one.");
			Destroy(Instance.gameObject);
		}
		Instance = this;
	}

	[SerializeField]
	private MapStart m_mapStart = null;
	[SerializeField]
	private Cinemachine.CinemachineVirtualCamera m_virtualCam = null;

	#endregion Private
}