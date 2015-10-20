using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pac_man.Controls
{
	 public interface IBlock:IDisposable
    {
        System.Drawing.Color BlockColor { get; set; }
    }

	public partial class Block : System.Windows.Forms.Control, IBlock
	{
		public Block()
		{
			this.BackColor = BlockColor;
		}

		public Block(int width, int height)
			: this()
		{
			this.Width = width;
			this.Height = height;
		}

		public Block(int width, int height, System.Drawing.Point location)
			: this(width, height)
		{
			this.Location = location;
		}

		private System.Drawing.Color _mBlockColor = System.Drawing.Color.DarkRed;

		public System.Drawing.Color BlockColor
		{
			get { return _mBlockColor; }
			set { _mBlockColor = value; }
		}

		public static Block SetBlockProp(int width, int height, int x, int y)
		{
			return new Block(width*20, height*20, new Point(x*20, y*20));
		}

	}
}
