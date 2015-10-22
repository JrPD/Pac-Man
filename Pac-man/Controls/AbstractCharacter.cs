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

	public enum EnemyType
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

	public abstract class Character : System.Windows.Forms.Control
	{

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
