using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
    [Serializable]
    public class Spell
    {
        public enum SpellRangeTypes
        {
            Line,
            Circle,
        }

        // Different Spells
        public static Spell DefaultSpell { get; set; }
        public static Spell CrossSpell { get; set; }
        public static Spell VerticalSpell { get; set; }
        public static Spell HorizontalSpell { get; set; }

        public static Spell HorizontalSpellMelee { get; set; }

        public static Spell HealDefaultSpell { get; set; }

        public static Spell DemaciaSpell { get; set; }

        public static Spell ResetSpell { get; set; }


        public static Spell MeteorCubeSpell { get; set; }

        public static Spell GriffeJul { get; set; }

        public static Spell ZoneArround { get; set; }


        public static Spell DeathhMark { get; set; }

        public static Spell SpawnPlant { get; set; }

        public static Spell AttackDefaultOberli { get; set; }

        public static Spell AttackUltiOberli { get; set; }

        public static Spell MudaMuda { get; set; }

        public static Spell CrossSpellMow { get; set; }

        public static Spell LanceFlammesKeirowz { get; set; }


        public static Spell SanglierBasique { get; set; }
        public static Spell SanglierOneShot { get; set; }


        public static Spell DeathCrossMishka { get; set; }

        public static Spell PunchAdrien { get; set; }


        // Spell Properties
        public List<Point> PointList { get; set; }

        private string Name { get; set; }

        public int Damage { get; set; }

        public int AttackRange { get; set; }

        public Boolean IsSpellInLine { get; set; }

        public  int ManaCost { get; set; }

        private SpellRangeTypes SpellRangeType { get; set; }

        public bool isVertical { get; set; }



        // Constructeur statique
        static Spell()
        {
            DefaultSpell = new Spell("default spell", 5, 1, 0,false, new List<Point>() { new Point(0, 0) });

            VerticalSpell = new Spell("vertical spell", 4, 1,0, true, new List<Point>() { new Point(0, 0), new Point(0, 1), new Point(0, 2) }, /*new Point(0, -1) },*/ SpellRangeTypes.Line);
            VerticalSpell.isVertical = true;
            HorizontalSpell = new Spell("vertical spell", 4, 3,0, true, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0) }, SpellRangeTypes.Line);

            HorizontalSpellMelee = new Spell("vertical spell", 4, 1,0, true, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0) }, SpellRangeTypes.Line);


            CrossSpell = new Spell("cross spell", 5, 3,0, false, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(0, -1) });


            HealDefaultSpell = new Spell("heal spell", -8, 1, 5,false, new List<Point>() { new Point(0, 0) });

            DemaciaSpell = new Spell("Chauvete Pure", 10, 1,10, false, new List<Point>() { new Point(0, 0) });

            ResetSpell = new Spell("Reset spell", 5, 1,8, false, new List<Point>() { new Point(0, 0) });


            MeteorCubeSpell = new Spell("Metor Cube", 10, 3,10, false, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(0, -1), new Point(1, 1), new Point(1, -1), new Point(-1, 1), new Point(-1, -1),
            new Point(-2, 0), new Point(2, 0), new Point(0, 2), new Point(0, -2), new Point(2, 1), new Point(2, -1), new Point(-2, 1), new Point(-2, -1),new Point(1, 2), new Point(1, -2), new Point(-1, 2), new Point(-1, -2), new Point(2, 2), new Point(2, -2), new Point(-2, 2), new Point(-2, -2)
            });

            ZoneArround = new Spell("faucille sanglante", 10, 1,10, false, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(-1, -1), new Point(0, -2), new Point(1, -1), new Point(-1, -2), new Point(1, -2) });


            GriffeJul = new Spell("Swipe spell", 7, 1, 7,true, new List<Point>() { new Point(0, 0), new Point(0, 1), new Point(0, 2),new Point(0, 3), new Point(0, 4) }, SpellRangeTypes.Line);
            GriffeJul.isVertical = true;
            DeathhMark = new Spell("Demacia spell", 0, 1, 10,false, new List<Point>() { new Point(0, 0) });

            //DefaultSpell = new Spell("default spell", 0, 1, false, new List<Point>() { new Point(0, 0) });

            AttackDefaultOberli = new Spell("vertical spell", 5, 3, 0, true, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0) }, SpellRangeTypes.Line);

            AttackUltiOberli = new Spell("Ober spell", 5, 3, 10, false, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(0, -1) });

            MudaMuda = new Spell("default spell", 3, 1, 10, false, new List<Point>() { new Point(0, 0) });

            SpawnPlant = new Spell("Spawn spell", 0, 3, 5, false, new List<Point>() { new Point(0, 0) });


            CrossSpellMow = new Spell("Sang Juif", 10, 3, 10, false, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(0, -1) });

            LanceFlammesKeirowz = new Spell("vertical spell", 7, 1, 0, true, new List<Point>() { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3), new Point(0, 4) }, /*new Point(0, -1) },*/ SpellRangeTypes.Line);
            

            SanglierBasique = new Spell("Sanglier Basique", 15, 3, 0, false, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(0, -1) });
            SanglierOneShot = new Spell("One Shot", 200, 3, 0, false, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(0, -1) });

            DeathCrossMishka = new Spell("cross spell", 7, 3, 8, false, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(0, -1) });

            PunchAdrien = new Spell("Punch spell", 4, 3, 0, true, new List<Point>() { new Point(-1, 0), new Point(0, 0), new Point(1, 0) ,new Point(-1, 1), new Point(0, 1), new Point(1, 1) }, SpellRangeTypes.Line);

        }

        public Spell(string name, int damage, int attackRange,int manaCost, Boolean isSpellInLine, List<Point> pointList, SpellRangeTypes spellRangeType = SpellRangeTypes.Circle)
        {
            PointList = pointList;
            Name = name;
            Damage = damage;
            AttackRange = attackRange;
            IsSpellInLine = isSpellInLine;
            SpellRangeType = spellRangeType;
            ManaCost = manaCost;
            isVertical = false;

        }

        public bool isSpellInRange(int posLineToCompare, int posColumnToCompare, int posLine, int posColumn)
        {
            switch (SpellRangeType)
            {
                case SpellRangeTypes.Line:
                    if ((Math.Abs(posLineToCompare - posLine) <= AttackRange && (posColumnToCompare - posColumn) == 0) || (Math.Abs(posColumnToCompare - posColumn) <= AttackRange && (posLineToCompare - posLine) == 0))
                    {
                        return true;
                    }
                    return false;
                default:
                    // Circle
                    if (Math.Abs(posLineToCompare - posLine) + Math.Abs(posColumnToCompare - posColumn) <= AttackRange)
                    {
                        return true;
                    }
                    return false;
            }

            
        }

        public void doDamage(Hero heroAttacking, Hero heroToAttack, int numberDeathMark=0)
        {


            if (heroToAttack.getName() == "Oberli" && heroToAttack.getHp() == 1)
            {

                ProgramFilRouge.attackOberli--;
            }

            if (heroAttacking.getName() == "Mishka")
            {
                heroToAttack.mishkaHasAttacked = true;
            }
            if (heroAttacking.getName() == "Alucard" || heroToAttack.getName() == "Alucard")
            {
                ProgramFilRouge.comboAdrien++;
            }
            if (this == DemaciaSpell)
            {
                if (heroAttacking.getHp() / heroAttacking.getMaxHp() <= 0.5)
                {
                    heroToAttack.setHp(heroToAttack.getHp() - this.Damage * 2);
                    return;
                }
            }
            else if (this == ResetSpell )
            {
                if (heroAttacking.getName() != "Mow") { return; }
                heroToAttack.setHp(heroToAttack.getHp() - this.Damage);
                return;
               // ProgramFilRouge.gameManager.newTurn(ProgramFilRouge.actualMap);
            }
            else if (this == ZoneArround)
            {
                heroAttacking.setHp(heroAttacking.getHp() + 3);
            }
            else if (this == AttackDefaultOberli)
            {
                heroToAttack.setHp(heroToAttack.getHp() - (this.Damage + ProgramFilRouge.attackOberli));
                return;
            }
            else if (this == AttackUltiOberli)
            {
                heroToAttack.setHp(heroToAttack.getHp() - (this.Damage * ProgramFilRouge.attackOberli));
                return;
            }
            else if(this == MudaMuda)
            {
                heroToAttack.setHp(heroToAttack.getHp() - this.Damage*ProgramFilRouge.comboAdrien);
                return;
            }
            else if(this == DeathhMark)
            {
                heroToAttack.setHp(heroToAttack.getHp() - (5 * numberDeathMark));
                return;
            }

            heroToAttack.setHp(heroToAttack.getHp() - this.Damage);
            
            
        }


        public string ToString()
        {
            return Name;
        }

    }
}
