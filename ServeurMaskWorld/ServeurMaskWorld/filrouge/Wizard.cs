using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{


	[Serializable]
	[Obsolete("Not used any more", true)]
	class Wizard : Hero
    {
		protected int mana;

		public Wizard()
		{
			this.name = "Wizard";
			this.hp = 2;
			this.agility = 2;
			this.intelligence = 8;
			this.strength = 2;
			this.mana = 10;
			this.pObject = null;
		}

		public Wizard(string _name, int _strength, int _agility, int _intelligence, double _hp, Potion _potion, int _mana, int _posLine, int _posColumn, int _movement, int _attackRange, int _speed, Constants.Case _cType,Spell _spell)// : base(_name, _strength, _agility, _intelligence, _hp, new Potion(_potion), -1, _posLine, _posColumn, _movement, _attackRange, _speed, _cType,_spell)
		{
			this.mana = _mana;
		}

		public void castSpell()
		{
			if (mana > 2)
			{
				Console.Write("FireBall");
				Console.Write("\n");
				mana -= 2;
			}
			else
			{
				Console.Write("Manque de mana");
				Console.Write("\n");
			}
		}

		public override void interact(Hero autre)
		{
			Console.Write("Hello ");
			Console.Write(autre.getName());
			Console.Write(" I'm a wizard powerfull named ");
			Console.Write(this.name);
			Console.Write("\n");
		}

		public override void show()
		{
			Console.Write(this.getInformations());
		}

		public override bool isSpell1InRange(int posLineToCompare, int posColumnToCompare)
		{
			if ((Math.Abs(posLineToCompare - posLine) <= this.attackRange && (posColumnToCompare - posColumn) == 0) || (Math.Abs(posColumnToCompare - posColumn) <= this.attackRange && (posLineToCompare - posLine) == 0))
			{
				return true;
			}
			return false;
		}

		public override string getInformations()
		{
			return base.getInformations() + "======================" + "\n" + "Class Wizard" + "\n" + "======================" + "\n" + "\n" + "\n";
		}

	}
}
