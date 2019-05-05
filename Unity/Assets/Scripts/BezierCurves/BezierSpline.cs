using System;
using System.Collections.Generic;
using UnityEngine;

public class BezierSpline : MonoBehaviour
{
	public int CurveCount
	{
		get
		{
			return (m_points.Length - 1) / 3;
		}
	}

	[SerializeField]
	private Vector3[] m_points;
	[SerializeField]
	private BezierControlPointMode[] m_modes;

	public int ControlPointCount
	{
		get
		{
			return m_points.Length;
		}
	}

	public Vector3 GetControlPoint(int index)
	{
		return m_points[index];
	}

	public void SetControlPoint(int index, Vector3 point)
	{
		if (index % 3 == 0)
		{
			Vector3 delta = point - m_points[index];
			if (index > 0)
			{
				m_points[index - 1] += delta;
			}
			if (index + 1 < m_points.Length)
			{
				m_points[index + 1] += delta;
			}
		}
		m_points[index] = point;
		EnforceMode(index);
	}

	public Vector3 GetPoint(float t)
	{
		int i;
		if (t >= 1f)
		{
			t = 1f;
			i = m_points.Length - 4;
		}
		else
		{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetPoint(
			m_points[i], m_points[i + 1], m_points[i + 2], m_points[i + 3], t));
	}

	public Vector3 GetVelocity(float t)
	{
		int i;
		if (t >= 1f)
		{
			t = 1f;
			i = m_points.Length - 4;
		}
		else
		{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetFirstDerivative(
			m_points[i], m_points[i + 1], m_points[i + 2], m_points[i + 3], t)) - transform.position;
	}

	public Vector3 GetDirection(float t)
	{
		return GetVelocity(t).normalized;
	}

	public void Reset()
	{
		m_points = new Vector3[] {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};
		m_modes = new BezierControlPointMode[] {
			BezierControlPointMode.FREE,
			BezierControlPointMode.FREE
		};
	}

	public void CreateCurve(List<Vector3> points)
	{
		int pointCount = 0;
		if (points.Count < 4)
		{
			pointCount = 4;
		}
		else
		{
			int pointsLeft = points.Count - 4;
			pointCount = points.Count + (pointsLeft % 3 != 0 ? 3 - (pointsLeft % 3) : 0);
		}
		m_points = new Vector3[pointCount];
		m_modes = new BezierControlPointMode[(pointCount / 3) + 1];
		for (int i = 0; i < pointCount; i++)
		{
			if (i < points.Count)
				m_points[i] = points[i];
			else
				m_points[i] = m_points[i - 1];
		}
		for (int i = 0; i < m_modes.Length; i++)
		{
			m_modes[i] = BezierControlPointMode.ALIGNED;
		}
		for (int i = 0; i < m_points.Length; i = i + 3)
		{
			EnforceMode(i);
		}
	}

	public void AddCurve()
	{
		Vector3 point = m_points[m_points.Length - 1];
		Array.Resize(ref m_points, m_points.Length + 3);
		point.x += 1f;
		m_points[m_points.Length - 3] = point;
		point.x += 1f;
		m_points[m_points.Length - 2] = point;
		point.x += 1f;
		m_points[m_points.Length - 1] = point;

		Array.Resize(ref m_modes, m_modes.Length + 1);
		m_modes[m_modes.Length - 1] = m_modes[m_modes.Length - 2];
		EnforceMode(m_points.Length - 4);
	}

	public BezierControlPointMode GetControlPointMode(int index)
	{
		return m_modes[(index + 1) / 3];
	}

	public void SetControlPointMode(int index, BezierControlPointMode mode)
	{
		m_modes[(index + 1) / 3] = mode;
	}

	private void EnforceMode(int index)
	{
		int modeIndex = (index + 1) / 3;
		BezierControlPointMode mode = m_modes[modeIndex];
		if (mode == BezierControlPointMode.FREE || modeIndex == 0 || modeIndex == m_modes.Length - 1)
		{
			return;
		}

		int middleIndex = modeIndex * 3;
		int fixedIndex, enforcedIndex;
		if (index <= middleIndex)
		{
			fixedIndex = middleIndex - 1;
			enforcedIndex = middleIndex + 1;
		}
		else
		{
			fixedIndex = middleIndex + 1;
			enforcedIndex = middleIndex - 1;
		}
		Vector3 middle = m_points[middleIndex];
		Vector3 enforcedTangent = middle - m_points[fixedIndex];
		if (mode == BezierControlPointMode.ALIGNED)
		{
			enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, m_points[enforcedIndex]);
		}
		m_points[enforcedIndex] = middle + enforcedTangent;
	}
}