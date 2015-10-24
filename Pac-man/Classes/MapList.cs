using Pac_man.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Pac_man
{
	public static class MapsList
	{
		public const byte Step = 20;

		private static List<Map> _maps = new List<Map>();

		public static void Add(Map map)
		{
			_maps.Add(map);
		}

		public static List<Map> GetMaps { get { return _maps; } }

		#region Defalut map

		public static Block[] Blocks()
		{
			Block[] blocks = new Block[26];
			{
				Block top = new Block(560, 20, new Point(20, 20));
				Block left = new Block(20, 560, new Point(20, 20));
				Block right = new Block(20, 560, new Point(560, 20));
				Block down = new Block(560, 20, new Point(20, 580));

				////--------------------------------------------------

				Block b = new Block(100, 20, new Point(60, 60));
				Block b1 = new Block(20, 100, new Point(60, 60));

				Block b2 = new Block(100, 20, new Point(440, 60));
				Block b3 = new Block(20, 100, new Point(520, 60));

				Block b9 = new Block(20, 100, new Point(60, 440));
				Block b10 = new Block(100, 20, new Point(60, 540));

				Block b11 = new Block(100, 20, new Point(440, 540));
				Block b12 = new Block(20, 100, new Point(520, 440));

				// center--------------------------------------------------

				Block b4 = new Block(20, 100, new Point(260, 100));
				Block b5 = new Block(20, 100, new Point(340, 100));

				Block b6 = new Block(180, 20, new Point(220, 300));

				Block b7 = new Block(20, 100, new Point(260, 400));
				Block b8 = new Block(20, 100, new Point(340, 400));


				//left and right center--------------------------------------------------

				Block b13 = new Block(20, 50, new Point(100, 280));
				Block b14 = new Block(20, 50, new Point(480, 280));

				Block b19 = new Block(20, 50, new Point(60, 200));
				Block b20 = new Block(20, 50, new Point(520, 200));

				Block b21 = new Block(20, 50, new Point(60, 340));
				Block b22 = new Block(20, 50, new Point(520, 340));


				//--------------------------------------------------



				blocks[0] = b;
				blocks[1] = b1;
				blocks[2] = b2;
				blocks[3] = b3;
				blocks[4] = b4;
				blocks[5] = b5;
				blocks[6] = b6;
				blocks[7] = b7;
				blocks[8] = b8;
				blocks[9] = b9;
				blocks[10] = b10;
				blocks[11] = b11;
				blocks[12] = b12;
				blocks[13] = b13;
				blocks[14] = b14;
				blocks[15] = top;
				blocks[16] = left;
				blocks[17] = down;
				blocks[18] = right;
				blocks[19] = b19;
				blocks[20] = b20;
				blocks[21] = b21;
				blocks[22] = b22;
				//blocks[23] = b23;
				//blocks[24] = b24;
				//blocks[25] = b25;
				return blocks;
			}
		}

		public static Dots[] Dots()
		{
			Dots[] dots = new Dots[100];

			Dots d = new Dots();
			d.Location = Controls.Dots.Loc(1, 1);
			Dots d1 = new Dots();
			d1.Location = Controls.Dots.Loc(2, 1);
			Dots d2 = new Dots();
			d2.Location = Controls.Dots.Loc(3, 1);
			Dots d3 = new Dots();
			d3.Location = Controls.Dots.Loc(4, 1);

			Dots d4 = new Dots();
			d4.Location = Controls.Dots.Loc(5, 1);
			Dots d5 = new Dots();
			d5.Location = Controls.Dots.Loc(6, 1);
			Dots d6 = new Dots();
			d6.Location = Controls.Dots.Loc(13, 1);
			Dots d7 = new Dots();
			d7.Location = Controls.Dots.Loc(14, 1);

			Dots d8 = new Dots();
			d8.Location = Controls.Dots.Loc(15, 1);
			Dots d9 = new Dots();
			d9.Location = Controls.Dots.Loc(16, 1);
			Dots d10 = new Dots();
			d10.Location = Controls.Dots.Loc(17, 1);
			Dots d11 = new Dots();
			d11.Location = Controls.Dots.Loc(18, 1);



			Dots d12 = new Dots();
			d12.Location = Controls.Dots.Loc(1, 2);
			Dots d13 = new Dots();
			d13.Location = Controls.Dots.Loc(1, 3);
			Dots d14 = new Dots();
			d14.Location = Controls.Dots.Loc(1, 4);
			Dots d15 = new Dots();
			d15.Location = Controls.Dots.Loc(1, 5);
			Dots d16 = new Dots();
			d16.Location = Controls.Dots.Loc(1, 6);
			Dots d17 = new Dots();
			d17.Location = Controls.Dots.Loc(1, 13);
			Dots d18 = new Dots();
			d18.Location = Controls.Dots.Loc(1, 14);
			Dots d19 = new Dots();
			d19.Location = Controls.Dots.Loc(1, 15);
			Dots d20 = new Dots();
			d20.Location = Controls.Dots.Loc(1, 16);
			Dots d21 = new Dots();
			d21.Location = Controls.Dots.Loc(1, 17);
			Dots d22 = new Dots();
			d22.Location = Controls.Dots.Loc(1, 18);
			Dots d23 = new Dots();
			d23.Location = Controls.Dots.Loc(1, 9);


			Dots d24 = new Dots();
			d24.Location = Controls.Dots.Loc(18, 2);
			Dots d25 = new Dots();
			d25.Location = Controls.Dots.Loc(18, 3);
			Dots d26 = new Dots();
			d26.Location = Controls.Dots.Loc(18, 4);
			Dots d27 = new Dots();
			d27.Location = Controls.Dots.Loc(18, 5);
			Dots d28 = new Dots();
			d28.Location = Controls.Dots.Loc(18, 6);
			Dots d29 = new Dots();
			d29.Location = Controls.Dots.Loc(18, 13);
			Dots d30 = new Dots();
			d30.Location = Controls.Dots.Loc(18, 14);
			Dots d31 = new Dots();
			d31.Location = Controls.Dots.Loc(18, 15);
			Dots d32 = new Dots();
			d32.Location = Controls.Dots.Loc(18, 16);
			Dots d33 = new Dots();
			d33.Location = Controls.Dots.Loc(18, 17);
			Dots d34 = new Dots();
			d34.Location = Controls.Dots.Loc(18, 18);
			Dots d35 = new Dots();
			d35.Location = Controls.Dots.Loc(18, 9);


			Dots d36 = new Dots();
			d36.Location = Controls.Dots.Loc(2, 18);
			Dots d37 = new Dots();
			d37.Location = Controls.Dots.Loc(3, 18);
			Dots d38 = new Dots();
			d38.Location = Controls.Dots.Loc(4, 18);
			Dots d39 = new Dots();
			d39.Location = Controls.Dots.Loc(5, 18);
			Dots d40 = new Dots();
			d40.Location = Controls.Dots.Loc(6, 18);
			Dots d41 = new Dots();
			d41.Location = Controls.Dots.Loc(7, 18);
			Dots d42 = new Dots();
			d42.Location = Controls.Dots.Loc(13, 18);
			Dots d43 = new Dots();
			d43.Location = Controls.Dots.Loc(14, 18);
			Dots d44 = new Dots();
			d44.Location = Controls.Dots.Loc(15, 18);
			Dots d45 = new Dots();
			d45.Location = Controls.Dots.Loc(16, 18);
			Dots d46 = new Dots();
			d46.Location = Controls.Dots.Loc(17, 18);
			Dots d47 = new Dots();
			d47.Location = Controls.Dots.Loc(18, 18);




			dots[0] = d;
			dots[1] = d1;
			dots[2] = d3;
			dots[3] = d2;

			dots[4] = d4;
			dots[5] = d5;
			dots[6] = d6;
			dots[7] = d7;

			dots[8] = d8;
			dots[9] = d9;
			dots[10] = d10;
			dots[11] = d11;

			dots[12] = d12;
			dots[13] = d13;
			dots[14] = d14;
			dots[15] = d15;

			dots[16] = d16;
			dots[17] = d17;
			dots[18] = d18;
			dots[19] = d19;

			dots[20] = d20;
			dots[21] = d21;
			dots[22] = d22;
			dots[23] = d23;

			dots[24] = d24;
			dots[25] = d25;
			dots[26] = d26;
			dots[27] = d27;

			dots[28] = d28;
			dots[29] = d29;
			dots[30] = d30;
			dots[31] = d31;

			dots[32] = d32;
			dots[33] = d33;
			dots[34] = d34;
			dots[35] = d35;

			dots[36] = d36;
			dots[37] = d37;
			dots[38] = d38;
			dots[39] = d39;

			dots[40] = d40;
			dots[41] = d41;
			dots[42] = d42;
			dots[43] = d43;
			dots[44] = d44;
			dots[45] = d45;
			dots[46] = d46;
			dots[47] = d47;
			return dots;
		}

		#endregion

		public static void LoadMaps()
		{
			_maps.Clear();
			// add default map to map list
			_maps.Add(new Map() { Blocks = Blocks(), Dots = Dots(), Name = "Default" });
			_maps[0].SetPackMan();

			string pathToMap = ConfigurationSettings.AppSettings["PathToMaps"];
			DirectoryInfo di = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
			string fullPath = di.FullName + "\\" + pathToMap;

			if (!Directory.Exists(fullPath))
			{
				Directory.CreateDirectory(fullPath);
				return;
			}
			string[] files = Directory.GetFiles(fullPath, "*.txt");

			if (!files.Any())
			{
				return;
			}
			// if files exists - clear maps file. no needed default map
			_maps.Clear();

			foreach (var file in files)
			{
				// add new map to map list
				_maps.Add(AddNewMap(file));
			}

		}

		private static Map AddNewMap(string file)
		{
			Map map = new Map { Name = file.Split('\\').Last() };

			try
			{
				using (StreamReader reader = new StreamReader(file))
				{
					// to set array position
					int positionBlock = 0;
					int positionDot = 0;

					string line;
					bool isBlocks = true;
					var In = reader.ReadLine();
					var Out = reader.ReadLine();
					if (!string.IsNullOrWhiteSpace(In) && !string.IsNullOrWhiteSpace(Out)
						&& In.Contains("#") && Out.Contains("#"))
					{
						In = In.Replace('#', ' ');
						Out = Out.Replace('#', ' ');
						string[] inp = In.Split(',');
						string[] outp = Out.Split(',');
						map.Entrance.X = Convert.ToInt32(inp[0]) * Step;
						map.Entrance.Y = Convert.ToInt32(inp[1]) * Step;
						map.Exit.X = Convert.ToInt32(outp[0]) * Step;
						map.Exit.Y = Convert.ToInt32(outp[1]) * Step;
					}

					while ((line = reader.ReadLine()) != null)
					{
						if (line.Contains('-'))
						{
							isBlocks = false;
							continue;
						}

						if (isBlocks)
						{

							AddNewBlock(line, map, ref positionBlock);
						}
						else
						{
							AddNewDot(line, map, ref positionDot);
						}
					}
					return map;
				}
			}
			catch (Exception)
			{
				return map;
				//throw;
			}
			return null;
		}

		public static void AddNewBlock(string line, Map map, ref int pos)
		{
			string[] dots = line.Split(',');
			if (dots.Count() == 4)
			{
				int width = Convert.ToInt32(dots[0]);
				int height = Convert.ToInt32(dots[1]);
				int x = Convert.ToInt32(dots[2]);
				int y = Convert.ToInt32(dots[3]);
				if (pos < 4)
				{
					map.Blocks[pos] = (new Block(width, height, new Point(x, y)));
				}
				map.Blocks[pos] = (Block.SetBlockProp(width, height, x, y));
				pos++;
			}
		}

		public static void AddNewDot(string line, Map map, ref int pos)
		{
			string[] dots = line.Split(',');
			if (dots.Count() == 2)
			{
				int x = Convert.ToInt32(dots[0]);
				int y = Convert.ToInt32(dots[1]);
				map.Dots[pos] = (new Dots() { Location = Controls.Dots.Loc(x, y) });
				pos++;
			}
		}
	}
}
