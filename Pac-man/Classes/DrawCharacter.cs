using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pac_man.Controls;

namespace Pac_man.Classes
{
	public sealed class DrawCharacter
	{
		public static void Draw(ref System.Windows.Forms.PaintEventArgs e, CharacterType type, MovementWay way = MovementWay.Right)
		{
			switch (type)
			{
				case CharacterType.Packman:
					e.Graphics.Clear(System.Drawing.SystemColors.Control);
					e.Graphics.FillEllipse(System.Drawing.Brushes.DarkGreen, 0, 0, 20, 20);
					e.Graphics.FillEllipse(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(6, 7, 3, 3));
					e.Graphics.FillEllipse(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(12, 7, 3, 3));
					e.Graphics.FillEllipse(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(12, 7, 3, 3));
					e.Graphics.FillEllipse(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(8, 12, 6, 2));
					break;

				case CharacterType.Enemy:
					e.Graphics.Clear(System.Drawing.SystemColors.Control);
					e.Graphics.FillEllipse(System.Drawing.Brushes.Blue, new System.Drawing.Rectangle(0, 0, 20, 25));
					e.Graphics.FillEllipse(System.Drawing.Brushes.LightYellow, new System.Drawing.Rectangle(4, 8, 3, 3));
					e.Graphics.FillEllipse(System.Drawing.Brushes.LightYellow, new System.Drawing.Rectangle(13, 8, 3, 3));
					e.Graphics.FillPolygon(System.Drawing.SystemBrushes.Control,
											new System.Drawing.Point[] { 
                                                    new System.Drawing.Point(1, 20), 
                                                    new System.Drawing.Point(4, 15), 
                                                    new System.Drawing.Point(7, 20),
                                                    new System.Drawing.Point(10, 15),
                                                    new System.Drawing.Point(13, 20),
                                                    new System.Drawing.Point(16, 15),
                                                    new System.Drawing.Point(19, 20)});
					break;
			}
		}

	}
}
