using UnityEngine;

public class SplineWalker : MonoBehaviour
{
	public float Progress { get; set; }

	public BezierSpline Spline { get { return m_spline; } set { m_spline = value; } }

	public bool ShouldFollowSpline { get; set; }

	#region Private

	private void Update()
	{
		if (m_spline != null && ShouldFollowSpline)
		{
			Progress += Time.deltaTime * m_speed / m_spline.ControlPointCount;
			if (Progress > 1f)
			{
				Progress = 1f;
			}
			newPoint = m_spline.GetPoint(0.1f);
			Vector3 direction = newPoint - transform.position;
			transform.Translate(direction.normalized * Time.deltaTime * m_speed);
		}
	}

	private void OnDestroy()
	{
		if (m_spline != null)
			Destroy(m_spline.gameObject);
	}

	private void OnDrawGizmos()
	{
		if (newPoint != Vector3.zero)
			Gizmos.DrawSphere(newPoint, 0.2f);
	}

	[SerializeField]
	private float m_speed = 2.0f;

	private BezierSpline m_spline = null;
	private Vector3 newPoint = Vector3.zero;

	#endregion Private
}