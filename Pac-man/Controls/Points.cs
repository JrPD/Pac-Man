using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_man.Controls
{

	public interface IDots : IDisposable
	{
		int Points { get; }
		System.Drawing.Color DotColor { get; set; }
	}

	public class Dots : System.Windows.Forms.Control, IDots
	{
		const byte Step = MapsList.Step;

		public Dots()
		{
			this.Width = this.Height = Step;
		}

		public Dots(int point)
			: this()
		{
			_points = point;
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			System.Drawing.Pen p = new System.Drawing.Pen(DotColor);
			e.Graphics.FillEllipse(p.Brush, 0, 0, 10, 10);

			//base.OnPaint(e);
		}

		int _points = 100;
		public int Points
		{
			get { return _points; }
		}

		System.Drawing.Color _mColor = System.Drawing.Color.Black;
		public System.Drawing.Color DotColor
		{
			get
			{
				return _mColor;
			}
			set
			{
				_mColor = value;
			}
		}

		public new void Dispose()
		{
			_points = 0;
			this.Dispose(true);
		}

		public static Point Loc(int x, int y)
		{
			return new Point(Step * x, Step * y + Step);
		}

	}
}
