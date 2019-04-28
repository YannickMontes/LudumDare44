using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridTile : MonoBehaviour
{
	public static GridTile Instance { get; private set; }

	public List<Vector3> GetPath(Vector3 start, Vector3 end)
	{
		return Pathfinding.AStar.FindPath(m_groundTilemap, start, end);
	}

	public TileBase GetTile(Vector3 pos)
	{
		return m_groundTilemap.GetTile(new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z));
	}

	#region Private

	private void Awake()
	{
		Instance = this;
	}

	[SerializeField]
	private Tilemap m_groundTilemap = null;

	#endregion Private
}