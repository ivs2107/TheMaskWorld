using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
	[Serializable]
	public class Point
    {

		private int x;
		private int y;

		/*
		*  constructor
		*/
		public Point(int _x, int _y)
		{
			x = _x;
			y = _y;
		}

		/*
		*  translate the point
		*/
		public void translate(int delta_x, int delta_y)
		{
			x += delta_x;
			y += delta_y;
		}

		/*
		*  display point
		*/
		public void print()
		{
			Console.Write("(");
			Console.Write(x);
			Console.Write(",");
			Console.Write(y);
			Console.Write(")");
		}

		public void setX(int _x)
		{
			x = _x;
		}
		public void setY(int _y)
		{
			y = _y;
		}
	
		public int getX()
		{
			return x;
		}

		public int getY()
		{
			return y;
		}

	}
}
