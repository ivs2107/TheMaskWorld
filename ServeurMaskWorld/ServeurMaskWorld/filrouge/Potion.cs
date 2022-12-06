using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
	[Serializable]
	class Potion : IObject
    {
		private int power;

		/*
		* constructor
		*/
		public Potion(int power)
		{
			this.power = power;
		}

		public Potion(Potion potion)
        {
			this.power = potion.power;
        }


		/*
		* name of the potion
		*/
		public string getName()
		{
			return "";
		}


		/*
		* get power but same name with other objects
		*/
		public int getPower()
		{
			return this.power;
		}

		public void setPower(int dmg)
		{
			this.power = dmg;
		}

		/*
		* display potion
		*/
		public void show()
		{
			Console.Write("Potion");
			Console.Write("\n");
			Console.Write(this.getPower());
			Console.Write("\n");
			Console.Write(this.getName());
			Console.Write("\n");
		}


	}
}
