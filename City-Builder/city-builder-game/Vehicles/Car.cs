using Godot;

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
	private Vector2I _currentDirection = Vector2I.Down;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		PlayDirectionAnim(_currentDirection);
		StartPath();
	}

	public override void _Process(double delta)
	{
		if (!_moving) return;

		Position = Position.MoveToward(_targetWorldPos, Speed * (float)delta);

		if (Position.DistanceTo(_targetWorldPos) < 0.5f)
		{
			Position = _targetWorldPos;
			_pathIndex++;

			if (_pathIndex < _path.Length)
				MoveToNext();
			else
				OnDestinationReached();
		}
	}

	private void StartPath()
	{
		_path = new Vector2I[]
		{
			new(2, 2), new(3, 2), new(4, 2), new(5, 2),
			new(5, 3), new(5, 4), new(5, 5),
			new(4, 5), new(3, 5), new(2, 5),
			new(2, 4), new(2, 3), new(2, 2)
		};

		_pathIndex = 1;
		CurrentTile = _path[0];
		Position = TileToWorld(CurrentTile);
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

	private void OnDestinationReached()
	{
		_pathIndex = 1;
		CurrentTile = _path[0];
		Position = TileToWorld(CurrentTile);
		MoveToNext();
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

	private Vector2 TileToWorld(Vector2I tile)
	{
		return new Vector2(tile.X * TileSize + TileSize / 2f, tile.Y * TileSize + TileSize / 2f);
	}

	// kept for later A* hookup
	public Vector2I CurrentTile { get; private set; }
}
