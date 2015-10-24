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
		#region Events
		public event EnemyMovement EnemyMovement;
		public event EnemyPacmanCatched EnemyPacmanCatched;
		#endregion

		#region Fields, Properties

		const byte Step = MapsList.Step;

		private  Random _random;

		private readonly Pacman _pacman;

		private Timer _timer;
		private Timer _timerCheck;


		public bool[,] AllowedLocationsMap { get; set; }

		public CharacterType Type { get { return CharacterType.Enemy; } }

		public EnemyType EnemyType { get; set; }

		MovementWay _movement = MovementWay.Right;

		MovementWay _prevDir = MovementWay.Right;
		#endregion
	
		#region Ctor
		public Enemy()
		{
			this.Height = this.Width = Step;
			EnemyMovement += Enemy_Enemy_Movement;
			EnemyPacmanCatched += Enemy_EnemyPacmanCatched;
			_random = new Random();
		}

		public Enemy(Pacman pacman, EnemyType type, Point location, Game.Level level)
			: this()
		{
			SetSpeed(level);
			this.Location = location;
			_pacman = pacman;
			EnemyType = type;
			_timerCheck = new Timer();
			_timerCheck.Interval = 20;
			_timerCheck.Tick += _timerCheck_Tick;
			_timerCheck.Start();

			AllowedLocationsMap = pacman.AllowedLocationsMap;
		}

		#endregion
		
		/// <summary>
		/// set game level
		/// </summary>
		/// <param name="level"></param>
		private void SetSpeed(Game.Level level)
		{
			_timer = new Timer();
			switch (level)
			{
				case Game.Level.Low:
				{
					_timer.Interval = 300;
					break;
				}
				case Game.Level.Middle:
				{
					_timer.Interval = 200;
					break;
				}
				case Game.Level.Hight:
				{
					_timer.Interval = 150;
					break;
				}
			}
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private void Enemy_EnemyPacmanCatched(object sender)
		{
			_timer.Stop();
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			Timer timer = (Timer) sender;
			Move(_movement);
			timer.Start();
		}
		/// <summary>
		/// check if catched every 20 ms
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _timerCheck_Tick(object sender, EventArgs e)
		{
			Timer timer = (Timer)sender;

			if (_pacman != null)
			{
				if (EnemyMovement != null)
					EnemyMovement(this, this.Location);
				return;
			}
			timer.Start();
		}


		private void Enemy_Enemy_Movement(object sender, System.Drawing.Point location)
		{
			// angry gosts mode)))
			//------------------------------
			AngryGostMode(sender, location);
			//------------------------------
			
			Debug.WriteLine( this.Location.ToString());
			if (_pacman.Location == this.Location)
				{
				if (EnemyPacmanCatched != null)
				{
					if (!_pacman.IsCatched)
					{
					EnemyPacmanCatched(this);
					_timer.Enabled = false;
					}
				}
				_pacman.Catched(this);
			}
		}

		private void AngryGostMode(object sender, System.Drawing.Point location)
		{
			for (int i = 0; i <= _pacman._dots.Length - 1; i++)
			{
				if (_pacman._dots[i] == null)
					continue;

				if (_pacman._dots[i].Location.X >= location.X &&
					_pacman._dots[i].Location.X <= (location.X + (this.Width / 3)) &&
					_pacman._dots[i].Location.Y >= location.Y &&
					_pacman._dots[i].Location.Y <= ((this.Height / 3) + location.Y))
				{
					Enemy enemy = sender as Enemy;
					if (enemy != null) enemy.TotalPoints += _pacman._dots[i].Points;
					_pacman._dots[i].Dispose();
					_pacman._dots[i] = null;
				}
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			DrawCharacter.Draw(ref e, Type);
			base.OnPaint(e);
		}

		// is place allowed or not
		public bool IsAllowed(MovementWay move)
		{
			bool result = true;

			Point loc = new Point();
			loc.X = this.Location.X / Step;
			loc.Y = this.Location.Y / Step - 1;

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

			byte min = 0;
			byte max = 28;

			// false, if >max or <min
			if (loc.X >= max || loc.X < min || loc.Y >= max || loc.Y < min)
			{
				return false;
			}

			// if blocked - return false
			if (this.AllowedLocationsMap[loc.X, loc.Y])
			{
				result = false;
			}

			return result;
		}
	
		public override void Move(MovementWay way)
		{
			if (_pacman.IsCatched || _pacman.Location==_pacman.Exit)
			{
				EnemyMovement -= Enemy_Enemy_Movement;
				return;
			}
			// swich ememy type. Chase or random way
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

			

			//if (EnemyMovement != null)
			//	EnemyMovement(this, this.Location);
		}

		private MovementWay GenerateRandomWay()
		{
			Point e = EnemyLocation;
		
			List<MovementWay> possibleDirections = GetPossibleDirections(e);
			AvoidRevirsingDir(ref possibleDirections);

			int rnd = _random.Next(0, possibleDirections.Count);
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
				if (this.Location.X!=0)
				{
					loc.X = this.Location.X / Step;
					
				}
				if (this.Location.Y!=0)
				{
					loc.Y = this.Location.Y / Step - 1;
				}
				return loc;
			}
		}

		private List<MovementWay> GetPossibleDirections(Point pos)
		{
			List<MovementWay> moves = new List<MovementWay>();

			if ( pos.Y>27)
			{
				 moves.Add(MovementWay.Up);
				 return moves;
			}
			if (pos.X > 27)
			{
				moves.Add(MovementWay.Left);
				return moves;
			}
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
			e.X = e.X * Step;
			e.Y = e.Y * Step + Step;
			
			MovementWay way = _movement;

			//Avoids  reversing directions
			AvoidRevirsingDir(ref possibleDirections);
			
			// set distance as big unreal distance
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

