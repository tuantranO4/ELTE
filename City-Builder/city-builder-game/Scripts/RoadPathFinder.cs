using Godot;
using System;
using System.Collections.Generic;

public class RoadPathfinder
{
	private AStar2D _aStar = new AStar2D();
	private Dictionary<Vector2I, long> _tileToId = new();
	private long _nextId = 0;

	public void AddRoad(Vector2I pos, float weightScale = 1.0f)
	{
		if (_tileToId.ContainsKey(pos)) return;

		long id = _nextId++;
		_tileToId[pos] = id;
		_aStar.AddPoint(id, new Vector2(pos.X, pos.Y), weightScale);

		Vector2I[] dirs = { Vector2I.Up, Vector2I.Down, Vector2I.Left, Vector2I.Right };
		foreach (var dir in dirs)
		{
			var neighbor = pos + dir;
			if (_tileToId.TryGetValue(neighbor, out long neighborId))
				_aStar.ConnectPoints(id, neighborId);
		}
	}

	public void RemoveRoad(Vector2I pos)
	{
		if (!_tileToId.TryGetValue(pos, out long id)) return;
		_aStar.RemovePoint(id);
		_tileToId.Remove(pos);
	}

	public Vector2I[] GetPath(Vector2I from, Vector2I to)
	{
		if (!_tileToId.ContainsKey(from) || !_tileToId.ContainsKey(to)) return null;

		var path = _aStar.GetPointPath(_tileToId[from], _tileToId[to]);
		if (path.Length == 0) return null;

		return Array.ConvertAll(path, p => new Vector2I((int)p.X, (int)p.Y));
	}

	public bool HasRoad(Vector2I pos) => _tileToId.ContainsKey(pos);
}
