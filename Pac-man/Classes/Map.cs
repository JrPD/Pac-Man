using System;
using System.Diagnostics;
using System.Drawing;
using Pac_man.Controls;
using Pac_man.Controls;

namespace Pac_man
{
	public class Map
	{
		public string Name { get; set; }

		public Block[] Blocks;

		public Dots[] Dots;

		public Pacman Pacman { get; set;}

		public Point Entrance;

		public Point Exit;
		
		public Map()
		{
			Blocks =new Block[100];
			Dots =  new Dots[200];
			Entrance = new Point();
			Exit= new Point();
		}

		public void SetPackMan()
		{
			Pacman = new Pacman(Dots, AllowedMapPlaces());
			Pacman.Location =  new Point(Entrance.X, Entrance.Y);
			Pacman.Exit = Exit;
		}

		public bool[,] DotsLocations()
		{
			bool[,] places = new bool[29, 29];

			foreach (var dot in Dots)
			{
				if (dot == null)
				{
					continue;
				}
				int x = 0;
				int y = 0;

				if (dot.Location.X != 0)
					x = dot.Location.X/20;

				if (dot.Location.Y != 0)
					y = dot.Location.Y/20 - 1;

				places[x, y] = true;
			}


			for (int i = 0; i < 29; i++)
			{
				for (int j = 0; j < 29; j++)
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
			bool[,] places = new bool[29,29];

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
					x = block.Location.X/20;

				if (block.Location.Y != 0)
					y = block.Location.Y / 20-1;

				if (block.Width!=0)
					width = block.Width/20;

				if (block.Height!= 0)
					height = block.Height / 20;
				
				for (int i = 0; i < width; i++)
				{
					for (int j = 0; j < height; j++)
					{
						places[x + i, y + j] = true;
					}
				}
			}

			for (int i = 0; i < 29; i++)
			{
				for (int j = 0; j < 29; j++)
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

//560, 10, 10, 20	
//10, 560, 10, 20	
//10, 560, 560, 20	
//560, 10, 10, 570	
//100, 10, 50, 60
//10, 100, 50, 60	
//100, 10, 430, 60    
//10, 100, 520, 60	  
//10, 100, 50, 440	
//100, 10, 50, 530	  
//100, 10, 430, 530	  
//10, 100, 520, 430	
//10, 100, 260, 100	
//10, 100, 340, 100	
//180, 10, 220, 290	
//10, 100, 260, 400	
//10, 100, 340, 400	
//10, 50,  100, 270	
//10, 50,  480, 270	
//10, 50,  50, 200	
//10, 50,  520, 200	
//10, 50,  50, 340	
//10, 50,  520, 340	
					
					

					  
					  

					  
//1, 1				  
//2, 1
//3, 1
//4, 1
//5, 1
//6, 1
//13, 1
//14, 1
//15, 1
//16, 1
//17, 1
//18, 1
//1, 2
//1, 3
//1, 4
//1, 5
//1, 6
//1, 13
//1, 14
//1, 15
//1, 16
//1, 17
//1, 18
//1,9
//18, 2
//18, 3
//18, 4
//18, 5
//18, 6
//18, 13
//18, 14
//18, 15
//18, 16
//18, 17
//18, 18
//18, 9
//2,18
//3,18
//4,18
//5,18
//6,18
//7,18
//13,18
//14,18
//15,18
//16,18