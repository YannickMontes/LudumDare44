using System.Collections.Generic;
using UnityEngine;

public class SplineManager : MonoBehaviour
{
	public static SplineManager Instance { get; private set; }

	public BezierSpline GetBezierSpline(List<Vector3> positions)
	{
		GameObject bezierSpline = new GameObject("SplinePath" + (s_splineNumber++).ToString());
		BezierSpline spline = bezierSpline.AddComponent<BezierSpline>();
		spline.CreateCurve(positions);
		return spline;
	}

	#region Private

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private static long s_splineNumber = 0;

	#endregion Private
}