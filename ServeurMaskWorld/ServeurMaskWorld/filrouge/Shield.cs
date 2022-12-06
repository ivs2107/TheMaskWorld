using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{

    class Shield : IObject
    {
		private int solidity;

		/*
		* constructor
		*/
		public Shield(int solidity)
		{
			this.solidity = solidity;
		}

		/*
		* get name
		*/
		public string getName()
		{
			return "";
		}

		/*
		* get power of the shield
		*/
		public int getPower()
		{
			return this.solidity;
		}
		public void setPower(int dmg)
		{
			this.solidity = dmg;
		}
		/*
		* display shield
		*/
		public void show()
		{
			Console.Write("Shield");
			Console.Write("\n");
			Console.Write(this.getPower());
			Console.Write("\n");
			Console.Write(this.getName());
			Console.Write("\n");
		}

	}
}
