using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Numerics;
namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
    [System.Serializable]
    public class Hero
    {
        //new
        protected int id;

        protected int strength;
        protected int agility;
        protected int intelligence;
        protected double hp;
        protected int mana;
        protected string name = "";
        protected IObject pObject;

        protected int posLine;
        protected int posColumn;
        protected int movement;
        protected  int attackRange;
        protected int speed;
        protected int posLineAttack = 0;
        protected int posLColumnAttack = 0;
        protected double maxHp;

        protected bool canControl = false;

        public bool mishkaHasAttacked = false;
        
        [NonSerialized]protected Vector3 scaleImage;
        
        protected Constants.Case cType;
        protected Spell spell; //spell that has been choose
        protected Spell[] tabSpells = new Spell[3];

        public bool HasAlreadyAttack { get; set; }

        public Backpack backpack = new Backpack();
        protected bool hasEvolved = false;

        public Hero()
        {
            this.name = "no_name";
            this.hp = 0;
            this.agility = 0;
            this.intelligence = 0;
            this.strength = 0;
            this.pObject = null;
            tabSpells[0] = Spell.DefaultSpell;
            tabSpells[1] = Spell.DefaultSpell;
            tabSpells[2] = Spell.DefaultSpell;
            spell = tabSpells[0];
            HasAlreadyAttack = false;
            this.maxHp = 0;
        }

        public Hero(string _name, int _strength, int _agility, int _intelligence, double _hp, IObject _pObject,int  _mana, int _posLine, int _posColumn, int _movement, int _attackRange, int _speed, Constants.Case _cType, Spell[] tabSpells/*Spell spell*/,Vector3 _scaleImage)
        {
            this.hp = _hp;
            this.maxHp = _hp;
            this.intelligence = _intelligence;
            this.agility = _agility;
            this.strength = _strength;
            this.name = _name;
            this.posLine = _posLine;
            this.posColumn = _posColumn;
            this.movement = _movement;
            this.attackRange = _attackRange;
            this.speed = _speed;
            this.cType = _cType;
            this.pObject = _pObject;
            this.tabSpells = tabSpells;
            this.spell = tabSpells[0]; //Default
            this.scaleImage = _scaleImage;
            this.mana = _mana;
            HasAlreadyAttack = false;

        }

        public static T CreateDeepCopy<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(ms);
            }
        }

        public virtual void show()
        {
            Console.Write(this.getInformations());
        }

        public int getPosLine()
        {
            return posLine;
        }

        public void setCanControl(bool canControl)
        {
            this.canControl = canControl;
        }
        public bool getCanControl()
        {
            return canControl;
        }

        public int getPosColumn()
        {
            return posColumn;
        }
        public int getMovement()
        {
            return movement;
        }
        public int getHp()
        {
            return (int) hp;
        }
        public int getMaxHp()
        {
            return (int)maxHp;
        }
        public int getSpeed()
        {
            return speed;
        }
        public IObject getWeapon()
        {
            return this.pObject;
        }
        public void setHp(int _hp)
        {
            this.hp = _hp;
        }
        public int getAttackRange()
        {
            return attackRange;
        }
        public int getPosLineAttack()
        {
            return posLineAttack;
        }
        public int getPosColumnAttack()
        {
            return posLColumnAttack;
        }
        public Constants.Case getCType()
        {
            return cType;
        }

        public Vector3 GetScaleImage()
        {
            return scaleImage;
        }
        public void setPosLine(int posLine)
        {
            this.posLine = posLine;
        }
        public void setPosColumn(int posColumn)
        {
            this.posColumn = posColumn;
        }
        public void setMovement(int movement)
        {
            this.movement = movement;
        }
        public void setPosLineAttack(int posLineAttack)
        {
            this.posLineAttack = posLineAttack;
        }
        public void setPosColumnAttack(int posColumnAttack)
        {
            this.posLColumnAttack = posColumnAttack;
        }
        public void setId(int id)
        {
             this.id=id;
        }

        public void setSpell(int idSpell)
        {
            this.spell = this.tabSpells[idSpell];
        }


        public virtual string getInformations()
        {
            return "======================" + "\n" + "HERO : " + this.name + "\n" + "======================" + "\n" + "HP :" + this.hp + "\n" + "strength :" + this.strength + "\n" + "intelligence :" + this.intelligence + "\n" + "agility :" + this.agility + "\n";
        }

        public string getName()
        {
            return this.name;
        }

        public int getMana()
        {
            return this.mana;
        }

        public void setMana(int mana)
        {
            this.mana = mana;
        }


        public int getId()
        {
            return this.id;
        }

        public virtual void interact(Hero autre)
        {
            Console.Write("Hello ");
            Console.Write(autre.name);
            Console.Write(" I'm ");
            Console.Write(this.name);
            Console.Write("\n");
        }

        public int getAgility()
        {
            return 0;
        }
        public int getStrength()
        {
            return this.strength;
        }
        public int getIntelligence()
        {
            return this.intelligence;
        }

        public Spell getSpell()
        {
            return this.spell;
        }

        public Spell[] getTabSpells()
        {
            return this.tabSpells;
        }


        public List<Point> getListTargetAttack()
        {
            return spell.PointList;
        }

        public List<Point> getReferenceListTargetAttack()
        {
            return spell.PointList;
        }

        public virtual bool isSpell1InRange(int posLineToCompare, int posColumnToCompare)
        {
            return spell.isSpellInRange(posLineToCompare, posColumnToCompare, posLine, posColumn);
        }

        //canEvolve for every hero to redefinition
        public virtual bool canEvolve()
        {
            return false;
        }

    }

}
