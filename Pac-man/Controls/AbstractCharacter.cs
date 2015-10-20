using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_man.Controls
{
	public enum CharacterType
	{
		Packman,
		Enemy
	}
	enum EnemyType
	{
		Right,
		Left,
		Scatter,
		Chasing
	}

	public enum MovementWay
	{
		Up,
		Down,
		Left,
		Right
	}

	public interface ICharacter : IDisposable
	{
		int TotalPoints { get; set; }
		int Speed { get; set; }
		CharacterType Type { get; }
		void Move(MovementWay way);
	}

	public abstract class Character : System.Windows.Forms.Control, ICharacter
	{
		protected MovementWay _movement = MovementWay.Right;

		public abstract bool[,] AllowedLocationsMap { get; set; }

		int _mSpeed = 20;
		public virtual int Speed
		{
			get
			{
				return _mSpeed;
			}
			set
			{
				_mSpeed = value;
			}
		}

		protected bool IsAllowed(MovementWay move)
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
			if (loc.X >= 29 || loc.X<0 || loc.Y >= 29 || loc.Y<0)
			{
				return false;
			}

			if (AllowedLocationsMap[loc.X, loc.Y])
			{
				result = false;
			}

			return result;
		}
	
		public virtual CharacterType Type { get; set; }

		public virtual int TotalPoints { get; set; }

		public new virtual void Move(MovementWay way)
		{
			switch (way)
			{
				case MovementWay.Up:
					this.Location = new Point(this.Location.X, this.Location.Y - Speed);
					break;

				case MovementWay.Down:
					this.Location = new Point(this.Location.X, this.Location.Y + Speed);
					break;

				case MovementWay.Left:
					this.Location = new Point(this.Location.X - Speed, this.Location.Y);
					break;

				case MovementWay.Right:
					this.Location = new Point(this.Location.X + Speed, this.Location.Y);
					break;
			}
		}
	}
}
