using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Pac_man.Classes;

namespace Pac_man.Controls
{
	public delegate void EnemyMovement(object sender, System.Drawing.Point location);
	public delegate void EnemyPacmanCatched(object sender);

	public  class Enemy :Character, ICharacter
	{
		public event EnemyMovement EnemyMovement;
		public event EnemyPacmanCatched EnemyPacmanCatched;
		private Random random;

		private Pacman _pacman = null;
		private Timer Timer;
		public bool[,] AllowedLocationsMap { get; set; }
		public CharacterType Type { get { return CharacterType.Enemy; } set { } }
		public EnemyType EnemyType { get; set; }

		public Enemy()
		{
			this.Height = this.Width = 20;
			EnemyMovement += new EnemyMovement(Enemy_Enemy_Movement);
			EnemyPacmanCatched += Enemy_EnemyPacmanCatched;
			random = new Random();
		}

		public Enemy(Pacman pacman, EnemyType type, Point location, Game.Level level)
			: this()
		{
			SetSpeed(level);
			this.Location = location;
			_pacman = pacman;
			EnemyType = type;

			AllowedLocationsMap = pacman.AllowedLocationsMap;
		}

		private void SetSpeed(Game.Level level)
		{
			Timer = new Timer();
			switch (level)
			{
				case Game.Level.Low:
				{
					Timer.Interval = 300;
					break;
				}
				case Game.Level.Middle:
				{
					Timer.Interval = 200;
					break;
				}
				case Game.Level.Hight:
				{
					Timer.Interval = 100;
					break;
				}
			}
			Timer.Start();
			Timer.Tick += _timer_Tick;
		}

		private void Enemy_EnemyPacmanCatched(object sender)
		{
			Timer.Stop();

		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			Timer timer = (Timer) sender;
			timer.Start();

			Move(_movement);
		}

		private void Enemy_Enemy_Movement(object sender, System.Drawing.Point location)
		{
			if (_pacman.Location == location)
			{
				if (EnemyPacmanCatched != null)
				{
					EnemyPacmanCatched(this);
					Timer.Enabled = false;
				}
				_pacman.Catched(this);
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			DrawCharacter.Draw(ref e, Type);

			base.OnPaint(e);
		}

		MovementWay _movement = MovementWay.Right;
		MovementWay _prevDir;

		bool _findWall = false;
		
		public bool IsAllowed(MovementWay move)
		{
			bool result = true;

			Point loc = new Point();
			loc.X = this.Location.X / 20;
			loc.Y = this.Location.Y / 20 - 1;

			switch (move)
			{
				case MovementWay.Right:
					{
						loc.X += 1;
						break;
					}
				case MovementWay.Left:
					{
						loc.X -= 1;
						break;
					}

				case MovementWay.Up:
					{
						loc.Y -= 1;
						break;
					}
				case MovementWay.Down:
					{
						loc.Y += 1;
						break;
					}
			}
			if (loc.X >= 29 || loc.X < 0 || loc.Y >= 29 || loc.Y < 0)
			{
				return false;
			}

			if (this.AllowedLocationsMap[loc.X, loc.Y])
			{
				result = false;
			}

			return result;
		}
	
		public override void Move(MovementWay way)
		{
			switch (EnemyType)
			{
				case EnemyType.Chasing:
				{
					way = ChasingMode();
					break;
				}
				case EnemyType.Scatter:
				{
					_prevDir = way;
					way = GenerateRandomWay();
					break;
				}
			}
			
			base.Move(way);

			if (_pacman != null)
			{
				if (EnemyMovement != null) EnemyMovement(this, this.Location);
				return;
			}

			if (EnemyMovement != null)
				EnemyMovement(this, this.Location);
		}

		private MovementWay GenerateRandomWay()
		{
			Point e = EnemyLocation;

			List<MovementWay> possibleDirections = GetPossibleDirections(e);
			AvoidRevirsingDir(ref possibleDirections);

			int rnd = random.Next(0, possibleDirections.Count);
			_movement = possibleDirections[rnd];
			if (IsAllowed(_movement))
			{
				return possibleDirections[rnd];
			}
			else
			{
				return GenerateRandomWay();
			}
		}

		public int TotalPoints
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		private Point EnemyLocation
		{
			get
			{
				Point loc = new Point();
				loc.X = this.Location.X/20;
				loc.Y = this.Location.Y/20 - 1;
				return loc;
			}
		}

		private List<MovementWay> GetPossibleDirections(Point pos)
		{
			if (pos.X>28 || pos.Y>28)
			{
				return null;
			}
			List<MovementWay> moves = new List<MovementWay>();
			if (!AllowedLocationsMap[pos.X, pos.Y-1])
			{
				moves.Add(MovementWay.Up);	
			}
			if (!AllowedLocationsMap[pos.X, pos.Y +1])
			{
				moves.Add(MovementWay.Down);
			}
			if (!AllowedLocationsMap[pos.X-1, pos.Y])
			{
				moves.Add(MovementWay.Left);
			}
			if (!AllowedLocationsMap[pos.X+1, pos.Y])
			{
				moves.Add(MovementWay.Right);
			}

			return moves;
		}

		private MovementWay ChasingMode()
		{
			Point e = EnemyLocation;
			
			List<MovementWay> possibleDirections = GetPossibleDirections(e);
			e.X = e.X * 20;
			e.Y = e.Y * 20+20;
			
			MovementWay way = _movement;

			//Avoids  reversing directions
			AvoidRevirsingDir(ref possibleDirections);
			
			int shortestDistance = 1000;

			int targetX = _pacman.Location.X;
			int targetY = _pacman.Location.Y;

			// select shortest distance
			if (possibleDirections.Contains(MovementWay.Up) && _prevDir != MovementWay.Down)
			{
				int z = GetManhattanDistance(targetX, targetY, e.X, e.Y-1);
				if (z < shortestDistance)
				{
					shortestDistance = z;
					way = MovementWay.Up;
				}
			}

			if (possibleDirections.Contains(MovementWay.Down) && _prevDir != MovementWay.Up)
			{
				int z = GetManhattanDistance(targetX, targetY, e.X , e.Y+1);
				if (z < shortestDistance)
				{
					shortestDistance = z;
					way = MovementWay.Down;
				}
			}

			if (possibleDirections.Contains(MovementWay.Left) && _prevDir != MovementWay.Right)
			{
				int z = GetManhattanDistance(targetX, targetY, e.X-1, e.Y );
				if (z < shortestDistance)
				{
					shortestDistance = z;
					way = MovementWay.Left;
				}
			}

			if (possibleDirections.Contains(MovementWay.Right) && _prevDir != MovementWay.Left)
			{
				int z = GetManhattanDistance(targetX, targetY, e.X+1, e.Y );
				if (z < shortestDistance)
				{
					shortestDistance = z;
					way = MovementWay.Right;
				}
			}
			_prevDir = way;
			return way;
		}

		private void AvoidRevirsingDir(ref List<MovementWay> possibleDirections)
		{
			if (_prevDir == MovementWay.Up && possibleDirections.Contains(MovementWay.Down) &&
				possibleDirections.Contains(MovementWay.Up))
				possibleDirections.Remove(MovementWay.Down);

			if (_prevDir == MovementWay.Down && possibleDirections.Contains(MovementWay.Down) &&
				possibleDirections.Contains(MovementWay.Up))
				possibleDirections.Remove(MovementWay.Up);

			if (_prevDir == MovementWay.Right && possibleDirections.Contains(MovementWay.Right) &&
				possibleDirections.Contains(MovementWay.Left))
				possibleDirections.Remove(MovementWay.Left);

			if (_prevDir == MovementWay.Left && possibleDirections.Contains(MovementWay.Right) &&
				possibleDirections.Contains(MovementWay.Left))
				possibleDirections.Remove(MovementWay.Right);

		}

		private int GetManhattanDistance(int x1, int y1, int x2, int y2)
		{
			int d = Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

			return d;
		}

		//public MovementWay GenerateWay()
		//{
		//	var move = MoveCircle();
		//	if (IsAllowed(move))
		//	{
		//		return move;
		//	}
		//	else
		//	{
		//		return TurnClockWise(_prevDir);
		//	}
		//}

		//private MovementWay TurnClockWise(MovementWay move)
		//{
		//	switch (move)
		//	{
		//		case MovementWay.Right:
		//			{
		//				_movement = MovementWay.Down;
		//				break;
		//			}
		//		case MovementWay.Up:
		//			{
		//				_movement = MovementWay.Right;
		//				break;
		//			}
		//		case MovementWay.Left:
		//			{
		//				_movement = MovementWay.Up;
		//				break;
		//			}
		//		case MovementWay.Down:
		//			{
		//				_movement = MovementWay.Left;
		//				break;
		//			}
		//	}
		//	return _movement;
		//}

		//public bool[,] GetLocalGroup(Point pos)
		//{
		//	bool[,] locations = new bool[3, 3];
		//	for (int i = 0; i < 3; i++)
		//	{
		//		for (int j = 0; j < 3; j++)
		//		{
		//			locations[i, j] = false;
		//		}
		//	}

		//	if (AllowedLocationsMap[pos.X - 1, pos.Y - 1])
		//	{
		//		locations[0, 0] = true;
		//	}
		//	if (AllowedLocationsMap[pos.X, pos.Y - 1])
		//	{
		//		locations[0, 1] = true;
		//	}
		//	if (AllowedLocationsMap[pos.X + 1, pos.Y - 1])
		//	{
		//		locations[0, 2] = true;
		//	}

		//	if (AllowedLocationsMap[pos.X - 1, pos.Y])
		//	{
		//		locations[1, 0] = true;
		//	}
		//	if (AllowedLocationsMap[pos.X, pos.Y])
		//	{
		//		locations[1, 1] = true;
		//	}
		//	if (AllowedLocationsMap[pos.X + 1, pos.Y])
		//	{
		//		locations[1, 2] = true;
		//	}

		//	if (AllowedLocationsMap[pos.X - 1, pos.Y + 1])
		//	{
		//		locations[2, 0] = true;
		//	}
		//	if (AllowedLocationsMap[pos.X, pos.Y + 1])
		//	{
		//		locations[2, 1] = true;
		//	}
		//	if (AllowedLocationsMap[pos.X + 1, pos.Y + 1])
		//	{
		//		locations[2, 2] = true;
		//	}

		//	for (int i = 0; i < 3; i++)
		//	{
		//		for (int j = 0; j < 3; j++)
		//		{
		//			Debug.Write(locations[i, j]);
		//		}
		//		Debug.WriteLine("");
		//	}
		//	Debug.WriteLine("");
		//	return locations;
		//}

		//private MovementWay MoveCircle()
		//{
		//	var ways = GetPossibleDirections(EnemyLocation);
		//	var l = GetLocalGroup(EnemyLocation);

		//	if (l[2, 0] || (l[0, 0] && l[1, 0]) || (l[1, 0] && l[2, 0]) || (l[0, 0] && l[0, 1] && l[0, 2]))
		//	{
		//		return MovementWay.Down;
		//	}
		//	if (l[0, 2] || (l[0, 2] && l[1, 2]) || (l[1, 2] && l[1, 2]) || (l[0, 2] && l[1, 2] && l[2, 2]))
		//	{
		//		return MovementWay.Up;
		//	}
		//	if (l[2, 2] || (l[2, 1] && l[2, 2]) || (l[2, 0] && l[2, 1]) || (l[2, 0] && l[2, 1] && l[2, 2]))
		//	{
		//		return MovementWay.Right;
		//	}
		//	if (l[0, 0] || l[0, 1] || (l[0, 0] && l[0, 1]) || (l[0, 1] && l[0, 2]) || (l[0, 0] && l[1, 0] && l[2, 0]))
		//	{
		//		return MovementWay.Left;
		//	}
		//	return TurnClockWise(_prevDir);
		//}
	}
}

