using Godot;
using System;

public partial class Car : Node2D
{
	[Export] public float Speed = 100f;
	[Export] public int TileSize = 16;

	[ExportGroup("Animations")]
	[Export] public string AnimDown  = "";
	[Export] public string AnimUp    = "";
	[Export] public string AnimLeft  = "";
	[Export] public string AnimRight = "";

	private AnimatedSprite2D _sprite;
	private Vector2I[] _path;
	private int _pathIndex = 0;
	private Vector2 _targetWorldPos;
	private bool _moving = false;
	private bool _waitingForPath = false;
	private Vector2I _currentDirection = Vector2I.Down;

	public RoadPathfinder Pathfinder;
	public Vector2I CurrentTile;
	public Vector2I DestinationTile;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Position = TileToWorld(CurrentTile);
		PlayDirectionAnim(_currentDirection);
	}

	public override void _Process(double delta)
	{
		if (_waitingForPath)
		{
			TryGetPath();
			return;
		}

		if (!_moving) return;

		Position = Position.MoveToward(_targetWorldPos, Speed * (float)delta);

		if (Position.DistanceTo(_targetWorldPos) < 0.5f)
		{
			Position = _targetWorldPos;
			_pathIndex++;

			if (_pathIndex < _path.Length)
				MoveToNext();
			else
			{
				_moving = false;
				_sprite.Stop();
				OnDestinationReached();
			}
		}
	}

	public void GoTo(Vector2I destination)
	{
		DestinationTile = destination;
		TryGetPath();
	}

	public void RecalculatePath()
	{
		if (DestinationTile == default) return;
		TryGetPath();
	}

	private void TryGetPath()
	{
		// TODO: hook up RoadPathfinder once roads are built
		var path = Pathfinder?.GetPath(CurrentTile, DestinationTile);

		if (path == null || path.Length <= 1)
		{
			_waitingForPath = true;
			_moving = false;
			return;
		}

		_path = path;
		_pathIndex = 1;
		_waitingForPath = false;
		MoveToNext();
	}

	private void MoveToNext()
	{
		var nextTile = _path[_pathIndex];
		_currentDirection = nextTile - CurrentTile;
		CurrentTile = nextTile;
		_targetWorldPos = TileToWorld(CurrentTile);
		_moving = true;
		PlayDirectionAnim(_currentDirection);
	}

	private void PlayDirectionAnim(Vector2I dir)
	{
		if (_sprite == null) return;

		string anim = dir switch
		{
			_ when dir == Vector2I.Down  => AnimDown,
			_ when dir == Vector2I.Up    => AnimUp,
			_ when dir == Vector2I.Left  => AnimLeft,
			_ when dir == Vector2I.Right => AnimRight,
			_                            => AnimDown
		};

		if (_sprite.Animation != anim)
			_sprite.Play(anim);
	}

	private void OnDestinationReached()
	{
		GD.Print("Car reached destination");
		// TODO: hook into route system - pick next stop and call GoTo() again
	}

	private Vector2 TileToWorld(Vector2I tile)
	{
		return new Vector2(tile.X * TileSize + TileSize / 2f, tile.Y * TileSize + TileSize / 2f);
	}
}
