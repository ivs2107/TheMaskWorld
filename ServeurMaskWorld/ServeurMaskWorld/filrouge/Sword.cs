using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
	[Serializable]
	class Sword : IObject
    {
		private int damage;

		public Sword(int damage)
		{
			this.damage = damage;
		}

		public Sword(Sword sword)
        {
			this.damage = sword.damage;
        }


		/*
		* get the name of the sword
		*/
		public string getName()
		{
			return "";
		}

		/*
		* get the power of the sword
		*/
		public int getPower()
		{
			return this.damage;
		}

		public void setPower(int dmg)
		{
			this.damage = dmg;
		}

		/*
		* show stats of  the sword
		*/
		public void show()
		{
			Console.Write("Sword");
			Console.Write("\n");
			Console.Write(this.getPower());
			Console.Write("\n");
			Console.Write(this.getName());
			Console.Write("\n");
		}

	}
}
