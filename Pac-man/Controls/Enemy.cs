

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pac_man.Controls
{
	public delegate void EnemyMovement(object sender, System.Drawing.Point location);
	public delegate void EnemyPacmanCatched(object sender);

	public  class Enemy :Character
	{
		public event EnemyMovement EnemyMovement;
		public event EnemyPacmanCatched EnemyPacmanCatched;

		protected Pacman _pacman = null;
		protected Timer Timer;
		public override sealed bool[,] AllowedLocationsMap { get; set; }
		public override CharacterType Type { get { return CharacterType.Enemy; } set { } }

		public Enemy()
		{
			this.Height = this.Width = 20;
			this.Location = new Point(120, 280);
			EnemyMovement += new EnemyMovement(Enemy_Enemy_Movement);
			EnemyPacmanCatched += Enemy_EnemyPacmanCatched;
		}

		public Enemy(Pacman pacman)
			: this()
		{
			_pacman = pacman;
			Timer = new Timer(){Interval = 70};
			Timer.Start();
			Timer.Tick+=_timer_Tick;
			AllowedLocationsMap = pacman.AllowedLocationsMap;
		}

		protected void Enemy_EnemyPacmanCatched(object sender)
		{
			Timer.Stop();
		}

		protected void _timer_Tick(object sender, EventArgs e)
		{
			Timer timer = (Timer) sender;
			timer.Start();

			Move(_movement);
		}

		protected void Enemy_Enemy_Movement(object sender, System.Drawing.Point location)
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


		public MovementWay PrevWay;
		public MovementWay GenerateWay()
		{

			switch (_movement)
			{
				case MovementWay.Right:
				{

					//_movement = GoNext();
						if (IsAllowed(_movement))
							break;
						else if (IsAllowed(_movement))
						{
							_movement = MovementWay.Right;
						}
						break;
					}
				case MovementWay.Down:
					{
						_movement = MovementWay.Left;
						if (IsAllowed(_movement))
							return _movement;
						break;
					}
				case MovementWay.Left:
					{

						_movement = MovementWay.Up;
						if (IsAllowed(_movement))
							return _movement;
						break;
					}
				case MovementWay.Up:
					{
						_movement = MovementWay.Right;
						if (IsAllowed(_movement))
							return _movement;
						break;
					}
			}

			return _movement;
		}

		private MovementWay GoNext()
		{
			throw new NotImplementedException();
		}

		public override void Move(MovementWay way)
		{
			way = GenerateWay();
			base.Move(way);

			if (_pacman != null)
			{
				if (EnemyMovement != null) EnemyMovement(this, this.Location);
				return;
			}

			if (EnemyMovement != null)
				EnemyMovement(this, this.Location);
		}

		public new int TotalPoints
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
	}


}