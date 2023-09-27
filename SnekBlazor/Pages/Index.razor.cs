using System.Drawing;
using static SnekBlazor.Classes.Enumerators;

namespace SnekBlazor.Pages
{
	public partial class Index
	{
		private Random random = new Random();
		private Timer? _SnakeMovementEngine;

		private Queue<Point> _PlayerPosition = new Queue<Point>() { };
		private Point _FoodPosition = new Point(-1, -1);
		private bool _GameOver = false;
		private bool _Win = false;

		private MovementDirection _LastMovementDirection = MovementDirection.None;
		private int _Size = 10;

		public void DirectionChanged(MovementDirection direction)
		{
			if (_LastMovementDirection == MovementDirection.None)
			{
				_SnakeMovementEngine = new Timer(SnakeMovementUpdateCallback, null, 0, 300);
				_LastMovementDirection = direction;
				_PlayerPosition.Enqueue(new Point(0, 0));
				_FoodPosition = GetRandomFoodPosition();
			}
			else
			{
				if ((direction == MovementDirection.Up && _LastMovementDirection != MovementDirection.Down) ||
					(direction == MovementDirection.Down && _LastMovementDirection != MovementDirection.Up) ||
					(direction == MovementDirection.Left && _LastMovementDirection != MovementDirection.Right) ||
					(direction == MovementDirection.Right && _LastMovementDirection != MovementDirection.Left))
				{
					_LastMovementDirection = direction;
				}
				else
				{
					//Invalid Move
				}
			}

			StateHasChanged();
		}

		public void SnakeMovementUpdateCallback(object? status)
		{
			Point newPoint = _PlayerPosition.Last();

			switch (_LastMovementDirection)
			{
				case MovementDirection.Up:
					{
						newPoint.Y--;
						break;
					}
				case MovementDirection.Down:
					{
						newPoint.Y++;
						break;
					}
				case MovementDirection.Left:
					{
						newPoint.X--;
						break;
					}
				case MovementDirection.Right:
					{
						newPoint.X++;
						break;
					}
				default:
				case MovementDirection.None:
					{

						break;
					}
			}

			if (newPoint.X < 0)
			{
				newPoint.X = _Size - 1;
			}
			if (newPoint.Y < 0)
			{
				newPoint.Y = _Size - 1;
			}

			if (newPoint.X > _Size - 1)
			{
				newPoint.X = 0;
			}
			if (newPoint.Y > _Size - 1)
			{
				newPoint.Y = 0;
			}

			_PlayerPosition.Dequeue();

			if (_PlayerPosition.Contains(newPoint))
			{
				_GameOver = true;
			}

			_PlayerPosition.Enqueue(newPoint);

			if (_PlayerPosition.Count >= _Size * _Size)
			{
				_Win = true;
			}

			if (_GameOver || _Win)
			{
				_SnakeMovementEngine?.Change(Timeout.Infinite, Timeout.Infinite);
			}

			if (newPoint.X == _FoodPosition.X &&
				newPoint.Y == _FoodPosition.Y)
			{
				_FoodPosition = GetRandomFoodPosition();
				IncreaseSnakeLenght();
			}

			StateHasChanged();
		}

		public void IncreaseSnakeLenght()
		{
			_PlayerPosition.Enqueue(_PlayerPosition.Last());
		}

		private Point GetRandomPosition()
		{
			return new Point(random.Next(_Size - 1), random.Next(_Size - 1));
		}

		private Point GetRandomFoodPosition()
		{
			Point point;

			do
			{
				point = GetRandomPosition();
			} while (_PlayerPosition.Contains(point));

			return point;
		}

		private void ResetGame()
		{
			_LastMovementDirection = MovementDirection.None;
			_PlayerPosition = new Queue<Point>() { };
			_FoodPosition = new Point(-1, -1);
			_GameOver = false;
			_Win = false;
		}

	}
}
