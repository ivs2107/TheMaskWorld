﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;


namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
    [Serializable]
    class Laetithia : Hero
    {
        //creation new Hero
        public Laetithia(string _name, int _strength, int _agility, int _intelligence, double _hp, IObject _pObject, int mana, int _posLine, int _posColumn, int _movement, int _attackRange, int _speed, Constants.Case _cType, Spell[] _tabSpells,Vector3 _scaleImage)
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
            this.tabSpells = _tabSpells;
            this.spell = this.tabSpells[0];
            this.scaleImage = _scaleImage;
            this.mana = mana;
            HasAlreadyAttack = false;

        }

        //condition to Evolve
        public override bool canEvolve()
        {
            if (ProgramFilRouge.actualMap.getListAllEnnemyHeroOnMap().Count <= 5 && hasEvolved==false)
            {
                hasEvolved = true;
                return true;
            }
            return false;
        } 
    }
}
