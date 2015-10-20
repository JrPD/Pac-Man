using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pac_man.Controls;
using Pac_man.Controls;

namespace Pac_man
{
	public partial class Game : Form
	{
		public Map CurrentMap = null;

		public Game()
		{
			InitializeComponent();
		}

		private void InitGame()
		{
			// load maps into list
			MapsList.LoadMaps();

			// fill in combobox 
			foreach (var map in MapsList.GetMaps)
			{
				selectMapMenuItem.Items.Add(map.Name);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			InitGame();
			startToolStripMenuItem_Click(this, null);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void startToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.GroupBox.Controls.Clear();
			this.GroupBox.Refresh();

			LoadMapElements();
			
			CurrentMap.Pacman.PacmanPointsChanged += pack_Pacman_PointsChanged;
			CurrentMap.Pacman.PacmanMessages += pack_Pacman_Messages;

			GroupBox.Controls[0].Visible = true;
			GroupBox.Focus();
			this.KeyPreview = true;
		}

		void pack_Pacman_Messages(object sender, string messages)
		{
			MessageBox.Show(messages);
			CurrentMap = null;
			MapsList.LoadMaps();
		}

		void pack_Pacman_PointsChanged(object sender, int totalPoints)
		{
			PointsLabel.Text = totalPoints.ToString();
		}

		private void LoadMapElements()
		{
			if (selectMapMenuItem.SelectedItem==null)
			{
				CurrentMap = MapsList.GetMaps[0];
				FillInGroupBox(CurrentMap);
				return;
			}
			
			foreach (var map in MapsList.GetMaps)
			{
				if (string.Equals(selectMapMenuItem.SelectedItem, map.Name))
				{
					FillInGroupBox(map);
					
				}
			}
		}

		private void FillInGroupBox(Map map)
		{
			CurrentMap = map;
			CurrentMap.SetPackMan();
			this.GroupBox.Controls.Add(map.Pacman);

			this.GroupBox.Controls.Add(new Block(20, 20, map.Entrance) { BackColor = Color.Black });
			this.GroupBox.Controls.Add(new Block(20, 20, map.Exit) { BackColor = Color.Green });

			foreach (var block in map.Blocks)
			{
				this.GroupBox.Controls.Add(block);
			}
			foreach (var dot in map.Dots)
			{
				this.GroupBox.Controls.Add(dot);
			}

			Enemy enemyLeft = new Enemy(map.Pacman);
			GroupBox.Controls.Add(enemyLeft);

			//Enemy enemyRandom = new EnemyRandom(map.Pacman);
			//GroupBox.Controls.Add(enemyRandom);

		}

		private void Game_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					((ICharacter)this.GroupBox.Controls[0]).Move(MovementWay.Up);
					break;

				case Keys.S:
					((ICharacter)this.GroupBox.Controls[0]).Move(MovementWay.Down);
					break;

				case Keys.A:
					((ICharacter)this.GroupBox.Controls[0]).Move(MovementWay.Left);
					break;

				case Keys.D:
					((ICharacter)this.GroupBox.Controls[0]).Move(MovementWay.Right);
					break;
			}
		}
	}
}
