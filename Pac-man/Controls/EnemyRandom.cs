

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pac_man.Controls
{
	public class EnemyRandom : Enemy
	{
		public new event EnemyMovement EnemyMovement;
		public new event EnemyPacmanCatched EnemyPacmanCatched;

		private Random random;

		public EnemyRandom(Pacman pacman):base(pacman, EnemyType.Scatter)
		{
			//_pacman = pacman;
			this.Location = new Point(40, 60);
			Timer = new Timer() { Interval = 500 };
			Timer.Start();
			Timer.Tick += _timer_Tick;
			AllowedLocationsMap = pacman.AllowedLocationsMap;

			EnemyMovement +=  Enemy_Enemy_Movement;
			EnemyPacmanCatched += new EnemyPacmanCatched(Enemy_EnemyPacmanCatched) ;
			random = new Random();
		}
	
		public override void Move(MovementWay way)
		{
			//way = GenerateRandomWay();
			base.Move(way);

			if (_pacman != null)
			{
				if (EnemyMovement != null) EnemyMovement(this, this.Location);
				return;
			}

			if (EnemyMovement != null)
				EnemyMovement(this, this.Location);
		}

		MovementWay _movement = MovementWay.Right;

		private MovementWay GenerateRandomWay()
		{
			int rnd = random.Next(0, 4);
			switch (rnd)
			{
				case 0:
				{
					_movement = MovementWay.Right;
					break;
				}
				case 1:
				{
					_movement= MovementWay.Down;
					break;
				}
				case 2:
				{
					_movement = MovementWay.Left;
					break;
				}
				case 3:
				{
					_movement = MovementWay.Up;
					break;
				}
			}

			if (IsAllowed(_movement))
			{
				return _movement;
			}
			else
			{
				return GenerateRandomWay();
			}
		}
	}
}