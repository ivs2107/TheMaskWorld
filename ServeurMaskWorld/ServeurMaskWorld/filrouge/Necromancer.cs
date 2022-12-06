using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{

	[Serializable]
	[Obsolete("Not used any more", true)]
	class Necromancer : Wizard
    {
		public Necromancer()
		{
			this.name = "Necro";
			this.hp = 2;
			this.agility = 2;
			this.intelligence = 8;
			this.strength = 2;
			this.mana = 10;
			this.pObject = null;
		}

		public Necromancer(string _name, int _strength, int _agility, int _intelligence, double _hp, Potion _potion, int _mana, int _posLine, int _posColumn, int _movement, int _attackRange, int _speed, Constants.Case _cType, Spell _spell)// : base(_name, _strength, _agility, _intelligence, _hp, _potion, _mana, _posLine, _posColumn, _movement, _attackRange, _speed, _cType, _spell)
		{
			
		}

		public void riseUndeads()
		{
			if (mana > 2)
			{
				Console.Write("RISE UNDEAD!!!!");
				Console.Write("\n");
				mana -= 2;
			}
			else
			{
				Console.Write("Manque de mana");
				Console.Write("\n");
			}
		}

		public override string getInformations()
		{
			return base.getInformations() + "======================" + "\n" + "Class Necromancer" + "\n" + "======================" + "\n" + "\n" + "\n";
		}

		public override void show()
		{
			Console.Write(this.getInformations());
		}

		public override bool isSpell1InRange(int posLineToCompare, int posColumnToCompare)
		{
			return false;
		}

	}
}
