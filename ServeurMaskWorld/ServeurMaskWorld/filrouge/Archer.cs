using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{

	[Serializable]
	[Obsolete("Not used any more", true)]
	class Archer : Hero
    {
		public Archer(string _name, int _strength, int _agility, int _intelligence, double _hp, Sword _epee, int _posLine, int _posColumn, int _movement, int _attackRange, int _speed, Constants.Case _cType, Spell _spell) //: base(_name, _strength, _agility, _intelligence, _hp, new Sword(_epee), 2,_posLine, _posColumn, _movement, _attackRange, _speed, _cType, _spell)
		{
		}
		public Archer()
		{
			this.name = "Archer";
			this.hp = 10;
			this.agility = 2;
			this.intelligence = 2;
			this.strength = 8;
			this.pObject = null;
			this.spell = Spell.VerticalSpell;
		}
		public override void interact(Hero autre)
		{
			Console.Write("Hiiii ");
			Console.Write(autre.getName());
			Console.Write(" I'm a warrior by the name of ");
			Console.Write(this.name);
			Console.Write("\n");
		}
		public override string getInformations()
		{
			return base.getInformations() + "======================" + "\n" + "Class Archer" + "\n" + "======================" + "\n" + "\n" + "\n";
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

	}
}
