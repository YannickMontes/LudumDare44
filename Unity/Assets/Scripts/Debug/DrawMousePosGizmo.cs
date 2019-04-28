using UnityEngine;

public class DrawMousePosGizmo : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
			worldPoint.z = 0.0f;
			Gizmos.DrawSphere(worldPoint, 0.2f);
		}
	}
}