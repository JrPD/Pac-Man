using System;
using System.Diagnostics;
using System.Drawing;
using Pac_man.Controls;
using Pac_man.Controls;

namespace Pac_man
{
	public class Map
	{
		readonly byte Step = MapsList.Step;

		public string Name { get; set; }

		public Block[] Blocks;

		public Dots[] Dots;

		public Pacman Pacman { get; set;}

		public Point Entrance;

		public Point Exit;
		
		public Map()
		{
			// blocks count
			Blocks =new Block[100];

			// dots counts
			Dots =  new Dots[300];
			Entrance = new Point();
			Exit= new Point();
		}

		public void SetPackMan()
		{
			Pacman = new Pacman(Dots, AllowedMapPlaces());
			Pacman.Exit = Exit;

			// if entrance is not initialize
			if (Entrance == Point.Empty)
				Entrance = new Point(Step, 2*Step);

			Pacman.Entrance = Entrance;
		}
		//dimensions
		const byte id = 29;
		const byte jd = 29;

		public bool[,] DotsLocations()
		{
			bool[,] places = new bool[id,jd];

			foreach (var dot in Dots)
			{
				if (dot == null)
				{
					continue;
				}
				int x = 0;
				int y = 0;

				if (dot.Location.X != 0)
					x = dot.Location.X/Step;

				if (dot.Location.Y != 0)
					y = dot.Location.Y / Step - 1;

				places[x, y] = true;
			}


			for (int i = 0; i < id; i++)
			{
				for (int j = 0; j < jd; j++)
				{
					if (places[j, i])
					{
						Debug.Write("P");
					}
					else
					{
						Debug.Write("-");
					}
				}
				Debug.WriteLine("");
			}
			return places;
		}

		public  bool[,] AllowedMapPlaces()
		{
			bool[,] places = new bool[id,jd];

			foreach (var block in Blocks)
			{
				if (block==null)
				{
					continue;
				}
				int x=0;
				int y = 0;
				int width = 0;
				int height = 0;

				if (block.Location.X!=0)
					x = block.Location.X/Step;

				if (block.Location.Y != 0)
					y = block.Location.Y / Step-1;

				if (block.Width!=0)
					width = block.Width/Step;

				if (block.Height!= 0)
					height = block.Height / Step;
				
				for (int i = 0; i < width; i++)
				{
					for (int j = 0; j < height; j++)
					{
						places[x + i, y + j] = true;
					}
				}
			}

			for (int i = 0; i < id; i++)
			{
				for (int j = 0; j < jd; j++)
				{
					if (places[j,i])
					{
						Debug.Write("OO");
					}
					else
					{
						Debug.Write("--");
					}
				}
				Debug.WriteLine("");
			}
			Console.Read();
			return places;
		}
	}
}
