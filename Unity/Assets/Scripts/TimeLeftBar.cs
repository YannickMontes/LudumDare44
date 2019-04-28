using UnityEngine;
using UnityEngine.UI;

public class TimeLeftBar : MonoBehaviour
{
	private void Update()
	{
		m_slider.value = GameManager.Instance.CurrentTimer / GameManager.MAX_TIMER;
	}

	#region Private

	[SerializeField]
	private Slider m_slider = null;

	#endregion Private
}