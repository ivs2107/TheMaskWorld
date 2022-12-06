using System;
using System.Collections.Generic;
using System.Numerics;
using ServeurMaskWorld;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
    class ProgramFilRouge
    {

		public static int comboAdrien = 0;
		public static int attackOberli = 0;
		//public static int taille = 0;
		public static bool isLevel1 = false;
		public static bool isLevel2 = false;
		public static bool isLevel3 = false;
		public static bool isLevel4 = false;


		public static Map actualMap;
		public static GameManager gameManager;

		public static List<Action> SpawnLevel1 = new List<Action>();
		public static List<Action> SpawnLevel3 = new List<Action>();
		public static List<Action> SpawnLevel4 = new List<Action>();
		public static void CreationOfHeros1(Map Level)
		{
			SpawnLevel1.Add(SpawnLevel1Wave1);
			SpawnLevel1.Add(SpawnLevel1Wave2);
			SpawnLevel1.Add(SpawnLevel1Wave3);
			comboAdrien = 0;
			attackOberli = 0;

			isLevel2 = false;
			isLevel1 = true;
			isLevel3 = false;

			Sword swordCardBoard = new Sword(5);
			Sword arrow = new Sword(4);
			Potion potionFire = new Potion(3);
			Potion potionLatetithia = new Potion(3);
			Sword spiderClaws = new Sword(10);


			Spell[] tabSpellMow = { Spell.DefaultSpell, Spell.ResetSpell, Spell.CrossSpellMow };
			Spell[] tabSpellSyonara = { Spell.DefaultSpell, Spell.HealDefaultSpell, Spell.DemaciaSpell };
			Spell[] tabSpellKeirowz = { Spell.CrossSpell, Spell.ResetSpell, Spell.MeteorCubeSpell };

			Spell[] tabSpellVogby = { Spell.HorizontalSpell, Spell.GriffeJul, Spell.ZoneArround };
			Spell[] tabSpellMishka = { Spell.HorizontalSpellMelee, Spell.DeathCrossMishka, Spell.DeathhMark };
			Spell[] tabSpellAlucard = { Spell.VerticalSpell, Spell.DeathCrossMishka, Spell.MudaMuda };

			Spell[] tabSpellOberli = { Spell.HorizontalSpell, Spell.SpawnPlant, Spell.AttackUltiOberli };

			Spell[] tabSpellSanglier = { Spell.SanglierBasique, Spell.SanglierOneShot, Spell.ResetSpell };


			Hero WarriorCardBoard = new Hero("Syonara", 5, 3, 2, 25, swordCardBoard, 30, 12, 10, 3, 4, 8, Constants.Case.cAllyHero, tabSpellSyonara, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero pArcher = new Hero("Oberli", 4, 10, 5, 15, arrow, 40, 13, 9, 2, 3, 12, Constants.Case.cAllyHero, tabSpellOberli, new Vector3(0.159536406f, 0.161120921f, 0.120466255f));
			Hero pWizard1 = new Hero("Keirowz", 3, 5, 10, 15, potionFire, 40, 13, 11, 2, 4, 12, Constants.Case.cAllyHero, tabSpellKeirowz, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard2 = new Hero("VogBy", 3, 5, 10, 20, arrow, 30, 12, 8, 3, 1, 12, Constants.Case.cAllyHero, tabSpellVogby, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero mow = new Hero("Mow", 3, 5, 10, 23, swordCardBoard, 40, 12, 14, 4, 4, 19, Constants.Case.cAllyHero, tabSpellMow, new Vector3(0.294578046f, 0.297503799f, 0.222436458f));
			Hero pWizard4 = new Hero("Alucard", 3, 5, 10, 20, arrow, 30, 12, 12, 3, 3, 11, Constants.Case.cAllyHero, tabSpellAlucard, new Vector3(0.230201259f, 0.232487619f, 0.173825428f));
			Hero pWizard5 = new Hero("Mishka", 3, 5, 10, 20, arrow, 30, 12, 6, 3, 1, 12, Constants.Case.cAllyHero, tabSpellMishka, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));


			//30,0,5
			//Hero laetithia = new Laetithia("Laetithia", 2, 3, 4, 30, potionLatetithia, 40, 0, 10, 2, 2, 12, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.24688001f, 0.24688001f, 0.24688001f));
			Hero spider5 = new Hero("SanglierNormal", 2, 3, 4, 20, spiderClaws, 35, 1, 9, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider4 = new Hero("SanglierNormal", 2, 3, 4, 20, spiderClaws, 35, 1, 11, 2, 1, 8, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider = new Hero("SanglierNormal", 2, 3, 4, 20, spiderClaws, 35, 3, 8, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider2 = new Hero("SanglierNormal", 2, 3, 4, 20, spiderClaws, 35, 3, 12, 2, 1, 8, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			/*Hero spider3 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 4, 10, 2, 1, 13, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider6 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 5, 16, 2, 1, 9, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider7 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 7, 14, 2, 1, 13, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider8 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 4, 13, 2, 1, 9, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider9 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 3, 5, 2, 1, 13, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider10 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 7, 9, 2, 1, 9, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider11 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 8, 7, 2, 1, 13, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			*/

			spider.setCanControl(true);
			spider5.setCanControl(true);
			spider4.setCanControl(true);
			spider2.setCanControl(true);

			Level.putInTheMap(WarriorCardBoard);
			Level.putInTheMap(pArcher);
			Level.putInTheMap(pWizard1);
			Level.putInTheMap(pWizard2);
			Level.putInTheMap(mow);
			Level.putInTheMap(pWizard4);
			Level.putInTheMap(pWizard5);

			Level.putInTheMap(spider5);
			Level.putInTheMap(spider4);
			Level.putInTheMap(spider);
			Level.putInTheMap(spider2);
			/*Level.putInTheMap(spider3);
			Level.putInTheMap(spider6);
			Level.putInTheMap(spider7);
			Level.putInTheMap(spider8);
			Level.putInTheMap(spider9);
			Level.putInTheMap(spider10);
			Level.putInTheMap(spider11);*/

			//Level.putInTheMap(laetithia);

			int taille = Level.getListAllHeroOnMap().Count;



			for (int i = 0; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}



		}


		public static void CreationOfHeros2(Map Level)
		{
			comboAdrien = 0;
			attackOberli = 0;

			isLevel2 = true;
			isLevel1 = false;
			isLevel3 = false;

			Sword swordCardBoard = new Sword(5);
			Sword arrow = new Sword(4);
			Potion potionFire = new Potion(3);
			Potion potionLatetithia = new Potion(3);
			Sword spiderClaws = new Sword(10);


			Spell[] tabSpellMow = { Spell.DefaultSpell, Spell.ResetSpell, Spell.CrossSpellMow };
			Spell[] tabSpellSyonara = { Spell.DefaultSpell, Spell.HealDefaultSpell, Spell.DemaciaSpell };
			Spell[] tabSpellKeirowz = { Spell.CrossSpell, Spell.ResetSpell, Spell.MeteorCubeSpell };

			Spell[] tabSpellVogby = { Spell.HorizontalSpell, Spell.GriffeJul, Spell.ZoneArround };
			Spell[] tabSpellMishka = { Spell.HorizontalSpellMelee, Spell.DeathCrossMishka, Spell.DeathhMark };
			Spell[] tabSpellAlucard = { Spell.VerticalSpell, Spell.DeathCrossMishka, Spell.MudaMuda };

			Spell[] tabSpellOberli = { Spell.HorizontalSpell, Spell.SpawnPlant, Spell.AttackUltiOberli };

			Spell[] tabSpellSanglier = { Spell.SanglierBasique, Spell.SanglierOneShot, Spell.ResetSpell };


			Hero WarriorCardBoard = new Hero("Syonara", 5, 3, 2, 25, swordCardBoard, 30, 12, 10, 3, 4, 8, Constants.Case.cAllyHero, tabSpellSyonara, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero pArcher = new Hero("Oberli", 4, 10, 5, 15, arrow, 40, 13, 9, 2, 3, 12, Constants.Case.cAllyHero, tabSpellOberli, new Vector3(0.159536406f, 0.161120921f, 0.120466255f));
			Hero pWizard1 = new Hero("Keirowz", 3, 5, 10, 15, potionFire, 40, 13, 11, 2, 4, 12, Constants.Case.cAllyHero, tabSpellKeirowz, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard2 = new Hero("VogBy", 3, 5, 10, 20, arrow, 30, 12, 8, 3, 1, 12, Constants.Case.cAllyHero, tabSpellVogby, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero mow = new Hero("Mow", 3, 5, 10, 23, swordCardBoard, 40, 12, 14, 4, 4, 19, Constants.Case.cAllyHero, tabSpellMow, new Vector3(0.294578046f, 0.297503799f, 0.222436458f));
			Hero pWizard4 = new Hero("Alucard", 3, 5, 10, 20, arrow, 30, 12, 12, 3, 3, 11, Constants.Case.cAllyHero, tabSpellAlucard, new Vector3(0.230201259f, 0.232487619f, 0.173825428f));
			Hero pWizard5 = new Hero("Mishka", 3, 5, 10, 20, arrow, 30, 12, 6, 3, 1, 12, Constants.Case.cAllyHero, tabSpellMishka, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));


			Hero spider3 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 4, 10, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider6 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 5, 16, 2, 1, 9, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider7 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 7, 14, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider8 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 4, 13, 2, 1, 9, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider9 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 3, 5, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider10 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 7, 9, 2, 1, 9, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider11 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 8, 7, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));

			Hero soleil = new Hero("Soleil", 2, 3, 4, 50, spiderClaws, 35, 0, 10, 2, 4, 13, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero lune = new Hero("Lune", 2, 3, 4, 50, spiderClaws, 35, 0, 11, 2, 4, 13, Constants.Case.cEnnemyHero, tabSpellKeirowz,new Vector3(0.198435947f, 0.20040682f, 0.149839386f));



			Level.putInTheMap(WarriorCardBoard);
			Level.putInTheMap(pArcher);
			Level.putInTheMap(pWizard1);
			Level.putInTheMap(pWizard2);
			Level.putInTheMap(mow);
			Level.putInTheMap(pWizard4);
			Level.putInTheMap(pWizard5);


			Level.putInTheMap(spider3);
			Level.putInTheMap(spider6);
			Level.putInTheMap(spider7);
			Level.putInTheMap(spider8);
			Level.putInTheMap(spider9);
			Level.putInTheMap(spider10);
			Level.putInTheMap(spider11);

			Level.putInTheMap(soleil);
			Level.putInTheMap(lune);

			//Level.putInTheMap(laetithia);

			int taille = Level.getListAllHeroOnMap().Count;



			for (int i = 0; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}



		}


		public static void CreationOfHeros3(Map Level)
		{

			SpawnLevel3.Add(SpawnLevel3Wave1);
			SpawnLevel3.Add(SpawnLevel3Wave2);
			SpawnLevel3.Add(SpawnLevel3Wave3);
			comboAdrien = 0;
			attackOberli = 0;

			isLevel2 = false;
			isLevel1 = false;
			isLevel3 = true;

			Sword swordCardBoard = new Sword(5);
			Sword arrow = new Sword(4);
			Potion potionFire = new Potion(3);
			Potion potionLatetithia = new Potion(3);
			Sword spiderClaws = new Sword(10);


			Spell[] tabSpellMow = { Spell.DefaultSpell, Spell.ResetSpell, Spell.CrossSpellMow };
			Spell[] tabSpellSyonara = { Spell.DefaultSpell, Spell.HealDefaultSpell, Spell.DemaciaSpell };
			Spell[] tabSpellKeirowz = { Spell.CrossSpell, Spell.ResetSpell, Spell.MeteorCubeSpell };

			Spell[] tabSpellVogby = { Spell.HorizontalSpell, Spell.GriffeJul, Spell.ZoneArround };
			Spell[] tabSpellMishka = { Spell.HorizontalSpellMelee, Spell.DeathCrossMishka, Spell.DeathhMark };
			Spell[] tabSpellAlucard = { Spell.VerticalSpell, Spell.DeathCrossMishka, Spell.MudaMuda };

			Spell[] tabSpellOberli = { Spell.HorizontalSpell, Spell.SpawnPlant, Spell.AttackUltiOberli };

			Spell[] tabSpellSanglier = { Spell.SanglierBasique, Spell.SanglierOneShot, Spell.ResetSpell };


			Hero WarriorCardBoard = new Hero("Syonara", 5, 3, 2, 25, swordCardBoard, 30, 12, 10, 3, 4, 8, Constants.Case.cAllyHero, tabSpellSyonara, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero pArcher = new Hero("Oberli", 4, 10, 5, 15, arrow, 40, 13, 9, 2, 3, 12, Constants.Case.cAllyHero, tabSpellOberli, new Vector3(0.159536406f, 0.161120921f, 0.120466255f));
			Hero pWizard1 = new Hero("Keirowz", 3, 5, 10, 15, potionFire, 40, 13, 11, 2, 4, 12, Constants.Case.cAllyHero, tabSpellKeirowz, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard2 = new Hero("VogBy", 3, 5, 10, 20, arrow, 30, 12, 8, 3, 1, 12, Constants.Case.cAllyHero, tabSpellVogby, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero mow = new Hero("Mow", 3, 5, 10, 23, swordCardBoard, 40, 12, 14, 4, 4, 19, Constants.Case.cAllyHero, tabSpellMow, new Vector3(0.294578046f, 0.297503799f, 0.222436458f));
			Hero pWizard4 = new Hero("Alucard", 3, 5, 10, 20, arrow, 30, 12, 12, 3, 3, 11, Constants.Case.cAllyHero, tabSpellAlucard, new Vector3(0.230201259f, 0.232487619f, 0.173825428f));
			Hero pWizard5 = new Hero("Mishka", 3, 5, 10, 20, arrow, 30, 12, 6, 3, 1, 12, Constants.Case.cAllyHero, tabSpellMishka, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));


			//30,0,5
			//Hero laetithia = new Laetithia("Laetithia", 2, 3, 4, 30, potionLatetithia, 40, 0, 10, 2, 2, 12, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.24688001f, 0.24688001f, 0.24688001f));
			/*Hero spider5 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 1, 9, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider4 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 1, 11, 2, 1, 8, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 3, 8, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider2 = new Hero("Spider", 2, 3, 4, 5, spiderClaws, 35, 3, 12, 2, 1, 8, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			*/
			Hero spider3 = new Hero("MaskCat", 2, 3, 4, 80, spiderClaws, 35, 4, 10, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.22f, 0.154406816f, 0.112630531f));
			Hero spider6 = new Hero("MaskBunny", 2, 3, 4, 80, spiderClaws, 35, 5, 3, 2, 1, 9, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.22f, 0.154406816f, 0.112630531f));
			Hero spider7 = new Hero("MaskCat", 2, 3, 4, 80, spiderClaws, 35, 7, 13, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.22f, 0.154406816f, 0.112630531f));
			/*Hero spider8 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 4, 13, 2, 1, 9, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider9 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 3, 5, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider10 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 7, 9, 2, 1, 9, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
			Hero spider11 = new Hero("Apotre", 2, 3, 4, 25, spiderClaws, 35, 8, 7, 2, 1, 13, Constants.Case.cEnnemyHero, tabSpellMow, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));
*/

			Level.putInTheMap(WarriorCardBoard);
			Level.putInTheMap(pArcher);
			Level.putInTheMap(pWizard1);
			Level.putInTheMap(pWizard2);
			Level.putInTheMap(mow);
			Level.putInTheMap(pWizard4);
			Level.putInTheMap(pWizard5);


			Level.putInTheMap(spider3);
			Level.putInTheMap(spider6);
			Level.putInTheMap(spider7);

			//Level.putInTheMap(laetithia);

			int taille = Level.getListAllHeroOnMap().Count;



			for (int i = 0; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}



			/*

			Sword swordCardBoard = new Sword(5);
			Sword arrow = new Sword(4);
			Potion potionFire = new Potion(3);
			Potion potionGuerald = new Potion(8);
			Sword arrowEnnemy = new Sword(3);
			Sword swordEnnemy = new Sword(4);

			Hero WarriorCardBoard = new Hero("Syonara", 5, 3, 2, 25, swordCardBoard, 50, 10, 10, 3, 4, 8, Constants.Case.cAllyHero, Spell.DefaultSpell, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero pArcher1 = new Hero("Oberli", 4, 10, 5, 15, arrow, 50, 11, 9, 2, 3, 12, Constants.Case.cAllyHero, Spell.HorizontalSpell, new Vector3(0.159536406f, 0.161120921f, 0.120466255f));
			Hero pWizard1 = new Hero("Keirowz", 3, 5, 10, 15, potionFire, 50, 11, 11, 2, 4, 12, Constants.Case.cAllyHero, Spell.CrossSpell, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard2 = new Hero("VogBy", 3, 5, 10, 20, arrow, 50, 10, 8, 3, 1, 12, Constants.Case.cAllyHero, Spell.HorizontalSpellMelee, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard3 = new Hero("Mow", 3, 5, 10, 20, swordCardBoard, 50, 10, 14, 4, 4, 19, Constants.Case.cAllyHero, Spell.DefaultSpell, new Vector3(0.294578046f, 0.297503799f, 0.222436458f));
			Hero pWizard4 = new Hero("Alucard", 3, 5, 10, 20, arrow, 50, 10, 12, 3, 3, 11, Constants.Case.cAllyHero, Spell.VerticalSpell, new Vector3(0.230201259f, 0.232487619f, 0.173825428f));
			Hero pWizard5 = new Hero("Mishka", 3, 5, 10, 20, arrow, 50, 10, 6, 3, 1, 12, Constants.Case.cAllyHero, Spell.HorizontalSpellMelee, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));

			Hero Guerald = new Hero("Guerrald", 2, 3, 4, 25, potionGuerald, 35, 1, 10, 3, 5, 11, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.100459576f, 0.10145735f, 0.0758572221f));
			Hero pArcher = new Hero("Archer", 2, 3, 4, 5, arrowEnnemy, 35, 1, 8, 2, 1, 8, Constants.Case.cEnnemyHero, Spell.HorizontalSpell, new Vector3(0.175352722f, 0.17709434f, 0.132409185f));
			Hero pArcher2 = new Hero("Archer", 2, 3, 4, 5, arrowEnnemy, 35, 1, 12, 2, 1, 8, Constants.Case.cEnnemyHero, Spell.HorizontalSpell, new Vector3(0.175352722f, 0.17709434f, 0.132409185f));
			Hero pWarrior3 = new Hero("knight", 2, 3, 4, 7, swordEnnemy, 35, 3, 12, 2, 1, 8, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.116782211f, 0.11794211f, 0.0881824866f));
			Hero pWarrior4 = new Hero("knight", 2, 3, 4, 7, swordEnnemy, 35, 3, 8, 2, 1, 8, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.116782211f, 0.11794211f, 0.0881824866f));
			Hero pWarrior5 = new Hero("knight", 2, 3, 4, 7, swordEnnemy, 35, 4, 10, 2, 1, 8, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.116782211f, 0.11794211f, 0.0881824866f));

			Level.putInTheMap(WarriorCardBoard);
			Level.putInTheMap(pArcher1);
			Level.putInTheMap(pWizard1);
			Level.putInTheMap(pWizard2);
			Level.putInTheMap(pWizard3);
			Level.putInTheMap(pWizard4);
			Level.putInTheMap(pWizard5);
			//Level.putInTheMap(pWizard);
			Level.putInTheMap(Guerald);
			Level.putInTheMap(pArcher);
			Level.putInTheMap(pArcher2);
			Level.putInTheMap(pWarrior3);
			Level.putInTheMap(pWarrior4);
			Level.putInTheMap(pWarrior5);

			int taille = Level.getListAllHeroOnMap().Count;

			for (int i = 0; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
			*/
		}
		public static void CreationOfHeros4(Map Level)
		{
			SpawnLevel4.Add(SpawnLevel4Wave1);
			comboAdrien = 0;
			attackOberli = 0;

			isLevel2 = false;
			isLevel1 = false;
			isLevel3 = false;
			isLevel4 = true;

			Sword swordCardBoard = new Sword(5);
			Sword arrow = new Sword(4);
			Potion potionFire = new Potion(3);
			Potion potionLatetithia = new Potion(3);
			Sword spiderClaws = new Sword(10);


			Spell[] tabSpellMow = { Spell.DefaultSpell, Spell.ResetSpell, Spell.CrossSpellMow };
			Spell[] tabSpellSyonara = { Spell.DefaultSpell, Spell.HealDefaultSpell, Spell.DemaciaSpell };
			Spell[] tabSpellKeirowz = { Spell.CrossSpell, Spell.ResetSpell, Spell.MeteorCubeSpell };

			Spell[] tabSpellVogby = { Spell.HorizontalSpell, Spell.GriffeJul, Spell.ZoneArround };
			Spell[] tabSpellMishka = { Spell.HorizontalSpellMelee, Spell.DeathCrossMishka, Spell.DeathhMark };
			Spell[] tabSpellAlucard = { Spell.VerticalSpell, Spell.DeathCrossMishka, Spell.MudaMuda };

			Spell[] tabSpellOberli = { Spell.HorizontalSpell, Spell.SpawnPlant, Spell.AttackUltiOberli };

			Spell[] tabSpellSanglier = { Spell.SanglierBasique, Spell.SanglierOneShot, Spell.ResetSpell };


			Hero WarriorCardBoard = new Hero("Syonara", 5, 3, 2, 25, swordCardBoard, 30, 0, 10, 3, 4, 8, Constants.Case.cAllyHero, tabSpellSyonara, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero pArcher = new Hero("Oberli", 4, 10, 5, 15, arrow, 40, 3, 3, 2, 3, 12, Constants.Case.cAllyHero, tabSpellOberli, new Vector3(0.159536406f, 0.161120921f, 0.120466255f));
			Hero pWizard1 = new Hero("Keirowz", 3, 5, 10, 15, potionFire, 40, 4, 17, 2, 4, 12, Constants.Case.cAllyHero, tabSpellKeirowz, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard2 = new Hero("VogBy", 3, 5, 10, 20, arrow, 30, 10, 6, 5, 1, 12, Constants.Case.cAllyHero, tabSpellVogby, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero mow = new Hero("Mow", 3, 5, 10, 23, swordCardBoard, 40, 12, 14, 4, 4, 19, Constants.Case.cAllyHero, tabSpellMow, new Vector3(0.294578046f, 0.297503799f, 0.222436458f));
			Hero pWizard4 = new Hero("Alucard", 3, 5, 10, 20, arrow, 30, 8, 12, 3, 3, 11, Constants.Case.cAllyHero, tabSpellAlucard, new Vector3(0.230201259f, 0.232487619f, 0.173825428f));
			Hero pWizard5 = new Hero("Mishka", 3, 5, 10, 20, arrow, 30, 5, 8, 3, 1, 12, Constants.Case.cAllyHero, tabSpellMishka, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			

			Hero spider5 = new Hero("TheMask", 2, 3, 4, 200, spiderClaws, 35, 0, 0, 13, 4, 1, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.127624482f, 0.154406816f, 0.112630531f));


			spider5.setCanControl(true);


			Level.putInTheMap(WarriorCardBoard);
			Level.putInTheMap(pArcher);
			Level.putInTheMap(pWizard1);
			Level.putInTheMap(pWizard2);
			Level.putInTheMap(mow);
			Level.putInTheMap(pWizard4);
			Level.putInTheMap(pWizard5);

			Level.putInTheMap(spider5);


			int taille = Level.getListAllHeroOnMap().Count;



			for (int i = 0; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
		}

		public static void CreationOfHeros5(Map Level)
		{
			/*
			Sword swordCardBoard = new Sword(5);
			Sword arrow = new Sword(4);
			Potion potionFire = new Potion(3);
			Potion potionGuerald = new Potion(5);
			Sword arrowEnnemy = new Sword(2);
			Sword swordEnnemy = new Sword(3);


			Hero WarriorCardBoard = new Hero("Syonara", 5, 3, 2, 25, swordCardBoard, 50, 11, 14, 3, 4, 8, Constants.Case.cAllyHero, Spell.DefaultSpell, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero pArcher1 = new Hero("Oberli", 4, 10, 5, 15, arrow, 50, 11, 6, 2, 3, 12, Constants.Case.cAllyHero, Spell.HorizontalSpell, new Vector3(0.159536406f, 0.161120921f, 0.120466255f));
			Hero pWizard1 = new Hero("Keirowz", 3, 5, 10, 15, potionFire, 50, 10, 13, 2, 4, 12, Constants.Case.cAllyHero, Spell.CrossSpell, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard2 = new Hero("VogBy", 3, 5, 10, 20, arrow, 50, 9, 8, 3, 1, 12, Constants.Case.cAllyHero, Spell.HorizontalSpellMelee, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard3 = new Hero("Mow", 3, 5, 10, 20, swordCardBoard, 50, 13, 14, 4, 4, 19, Constants.Case.cAllyHero, Spell.DefaultSpell, new Vector3(0.294578046f, 0.297503799f, 0.222436458f));
			Hero pWizard4 = new Hero("Alucard", 3, 5, 10, 20, arrow, 50, 10, 15, 3, 3, 11, Constants.Case.cAllyHero, Spell.VerticalSpell, new Vector3(0.230201259f, 0.232487619f, 0.173825428f));
			Hero pWizard5 = new Hero("Mishka", 3, 5, 10, 20, arrow, 50, 9, 10, 3, 1, 12, Constants.Case.cAllyHero, Spell.HorizontalSpellMelee, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));

			Hero Guerald = new RobotTheMask("RobotTheMask", 2, 3, 4, 20, potionGuerald, 35, 7, 10, 3, 5, 14, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero RobotTheMask1 = new Hero("RobotTheMask", 2, 3, 4, 20, potionGuerald, 35, 10, 5, 3, 5, 14, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero RobotTheMask2 = new Hero("RobotTheMask", 2, 3, 4, 20, potionGuerald, 35, 11, 15, 3, 5, 14, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero RobotTheMask3 = new Hero("RobotTheMask", 2, 3, 4, 20, potionGuerald, 35, 13, 13, 3, 5, 14, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero RobotTheMask4 = new Hero("RobotTheMask", 2, 3, 4, 20, potionGuerald, 35, 10, 11, 3, 5, 14, Constants.Case.cEnnemyHero, Spell.DefaultSpell, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));


			Level.putInTheMap(WarriorCardBoard);
			Level.putInTheMap(pArcher1);
			Level.putInTheMap(pWizard1);
			Level.putInTheMap(pWizard2);
			Level.putInTheMap(pWizard3);
			Level.putInTheMap(pWizard4);
			Level.putInTheMap(pWizard5);

			//Level.putInTheMap(pWizard);
			Level.putInTheMap(Guerald);
			Level.putInTheMap(RobotTheMask1);
			Level.putInTheMap(RobotTheMask2);
			Level.putInTheMap(RobotTheMask3);
			Level.putInTheMap(RobotTheMask4);

			int taille = Level.getListAllHeroOnMap().Count;

			for (int i = 0; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
			*/

		}



		public static void SpawnPlantOberli(Map Level, int posLine,int posColumn)
        {
			
			Spell[] tabSpellOberli = { Spell.HorizontalSpell, Spell.DefaultSpell, Spell.DefaultSpell };


			Sword swordCardBoard = new Sword(5);
			Hero plant = new Hero("Oberli", 5, 3, 2, 1, swordCardBoard, 50, posLine, posColumn, 3, 4, 11, Constants.Case.cAllyHero, tabSpellOberli, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));

			Level.putInTheMap(plant);

			int taille = Level.getListAllHeroOnMap().Count;
			attackOberli++;


			for (int i = taille-1; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
			System.Threading.Thread.Sleep(500);
		}

		
		public static void SpawnLevel1Wave1() 
		{
			Map Level = ProgramFilRouge.actualMap;
			Spell[] tabSpellSanglier = { Spell.SanglierBasique, Spell.SanglierOneShot, Spell.ResetSpell };


			Sword swordCardBoard = new Sword(10);
			Hero sanglier = new Hero("SanglierNormal", 5, 3, 2, 20, swordCardBoard, 50, 4, 1, 3, 4, 8, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier2 = new Hero("SanglierNormal", 5, 3, 2, 20, swordCardBoard, 50, 7, 10, 3, 4, 8, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier3 = new Hero("SanglierNormal", 5, 3, 2, 20, swordCardBoard, 50, 7, 16, 3, 4, 8, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier4 = new Hero("SanglierNormal", 5, 3, 2, 20, swordCardBoard, 50, 5, 7, 3, 4, 8, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier5 = new Hero("SanglierNormal", 5, 3, 2, 20, swordCardBoard, 50, 6, 9, 3, 4, 8, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));

			sanglier.setCanControl(true);
			sanglier2.setCanControl(true);
			sanglier3.setCanControl(true);
			sanglier4.setCanControl(true);
			sanglier5.setCanControl(true);

			Level.putInTheMap(sanglier);
			Level.putInTheMap(sanglier2);
			Level.putInTheMap(sanglier3);
			Level.putInTheMap(sanglier4);
			Level.putInTheMap(sanglier5);

			int taille = Level.getListAllHeroOnMap().Count;


			for (int i = taille - 5; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
		}


		public static void SpawnLevel1Wave2()
		{
			Map Level = ProgramFilRouge.actualMap;
			Spell[] tabSpellSanglier = { Spell.SanglierBasique, Spell.SanglierOneShot, Spell.ResetSpell };


			Sword swordCardBoard = new Sword(5);
			Hero sanglier = new Hero("SanglierRapide", 5, 3, 2, 10, swordCardBoard, 50, 0, 4, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier2 = new Hero("SanglierRapide", 5, 3, 2, 10, swordCardBoard, 50, 0, 17, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier3 = new Hero("SanglierRapide", 5, 3, 2, 10, swordCardBoard, 50, 0, 13, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));

			sanglier.setCanControl(true);
			sanglier2.setCanControl(true);
			sanglier3.setCanControl(true);
			Level.putInTheMap(sanglier);
			Level.putInTheMap(sanglier2);
			Level.putInTheMap(sanglier3);

			int taille = Level.getListAllHeroOnMap().Count;


			for (int i = taille - 3; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
		}

		public static void SpawnLevel1Wave3()
		{
			Map Level = ProgramFilRouge.actualMap;
			Spell[] tabSpellSanglier = { Spell.SanglierBasique, Spell.SanglierOneShot, Spell.ResetSpell };


			Sword swordCardBoard = new Sword(5);
			Hero sanglier = new Hero("SanglierBoss", 5, 3, 2, 80, swordCardBoard, 50, 0, 7, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier2 = new Hero("SanglierRapide", 5, 3, 2, 10, swordCardBoard, 50, 1, 9, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier3 = new Hero("SanglierRapide", 5, 3, 2, 10, swordCardBoard, 50, 1, 5, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellSanglier, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));

			sanglier.setCanControl(true);
			sanglier2.setCanControl(true);
			sanglier3.setCanControl(true);
			Level.putInTheMap(sanglier);
			Level.putInTheMap(sanglier2);
			Level.putInTheMap(sanglier3);

			int taille = Level.getListAllHeroOnMap().Count;


			for (int i = taille - 3; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
		}


		public static void SpawnLevel3Wave1()
		{
			Map Level = ProgramFilRouge.actualMap;
			Spell[] tabSpellMishka = { Spell.HorizontalSpellMelee, Spell.DefaultSpell, Spell.DeathhMark };


			Sword swordCardBoard = new Sword(5);
			Hero sanglier = new Hero("MaskBunny", 5, 3, 2, 50, swordCardBoard, 50, 4, 9, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier2 = new Hero("MaskCat", 5, 3, 2, 50, swordCardBoard, 50, 3, 1, 1, 4, 18, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.22f, 0.142585784f, 0.106607974f));
			Hero sanglier3 = new Hero("MaskCat", 5, 3, 2, 50, swordCardBoard, 50, 13, 17, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.22f, 0.142585784f, 0.106607974f));

			Level.putInTheMap(sanglier);
			Level.putInTheMap(sanglier2);
			Level.putInTheMap(sanglier3);

			int taille = Level.getListAllHeroOnMap().Count;


			for (int i = taille-3; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
		}

		public static void SpawnLevel3Wave2()
		{
			Map Level = ProgramFilRouge.actualMap;
			Spell[] tabSpellMishka = { Spell.HorizontalSpellMelee, Spell.DefaultSpell, Spell.DeathhMark };


			Sword swordCardBoard = new Sword(5);
			Hero sanglier = new Hero("MaskCat", 5, 3, 2, 50, swordCardBoard, 50, 2, 7, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.22f, 0.142585784f, 0.106607974f));
			Hero sanglier2 = new Hero("MaskCat", 5, 3, 2, 50, swordCardBoard, 50, 8, 15, 1, 4, 18, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.22f, 0.142585784f, 0.106607974f));
			Hero sanglier3 = new Hero("MaskBunny", 5, 3, 2, 50, swordCardBoard, 50, 4, 5, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));

			Level.putInTheMap(sanglier);
			Level.putInTheMap(sanglier2);
			Level.putInTheMap(sanglier3);

			int taille = Level.getListAllHeroOnMap().Count;


			for (int i = taille-3; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
		}


		public static void SpawnLevel3Wave3()
		{
			Map Level = ProgramFilRouge.actualMap;
			Spell[] tabSpellMishka = { Spell.HorizontalSpellMelee, Spell.DefaultSpell, Spell.DeathhMark };
			Spell[] tabSpellKeirowz = { Spell.CrossSpell, Spell.VerticalSpell, Spell.MeteorCubeSpell };

			Sword swordCardBoard = new Sword(5);
			Hero sanglier = new Hero("MaskBunny", 5, 3, 2, 50, swordCardBoard, 50, 8, 9, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier2 = new Hero("DernierRire", 5, 3, 2, 200, swordCardBoard, 50, 2, 17, 1, 4, 18, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero sanglier3 = new Hero("MaskBunny", 5, 3, 2, 50, swordCardBoard, 50, 12, 17, 6, 4, 18, Constants.Case.cEnnemyHero, tabSpellMishka, new Vector3(0.22f, 0.142585784f, 0.106607974f));

			Level.putInTheMap(sanglier);
			Level.putInTheMap(sanglier2);
			Level.putInTheMap(sanglier3);

			int taille = Level.getListAllHeroOnMap().Count;


			for (int i = taille-3; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
		}


		public static void SpawnLevel4Wave1()
		{
			Map Level = ProgramFilRouge.actualMap;
			//Spell[] tabSpellMishka = { Spell.HorizontalSpellMelee, Spell.DefaultSpell, Spell.DeathhMark };
			Spell[] tabSpellKeirowz = { Spell.SanglierBasique, Spell.VerticalSpell, Spell.MeteorCubeSpell };

			Sword swordCardBoard = new Sword(5);
			Hero WarriorCardBoard = new Hero("MaskBunny", 5, 3, 2, 100, swordCardBoard, 30, 0, 10, 3, 4, 3, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.141183555f, 0.142585784f, 0.106607974f));
			Hero pArcher = new Hero("MaskBunny", 4, 10, 5, 100, swordCardBoard, 40, 3, 3, 2, 3, 3, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.159536406f, 0.161120921f, 0.120466255f));
			Hero pWizard1 = new Hero("MaskBunny", 3, 5, 10, 100, swordCardBoard, 40, 4, 17, 2, 4, 3, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero pWizard2 = new Hero("MaskBunny", 3, 5, 10, 100, swordCardBoard, 30, 10, 6, 5, 1, 3, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));
			Hero mow = new Hero("MaskBunny", 3, 5, 10, 100, swordCardBoard, 40, 12, 14, 4, 4, 3, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.294578046f, 0.297503799f, 0.222436458f));
			Hero pWizard4 = new Hero("MaskBunny", 3, 5, 10, 100, swordCardBoard, 30, 8, 12, 3, 3, 3, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.230201259f, 0.232487619f, 0.173825428f));
			Hero pWizard5 = new Hero("MaskBunny", 3, 5, 10, 100, swordCardBoard, 30, 5, 8, 3, 1, 3, Constants.Case.cEnnemyHero, tabSpellKeirowz, new Vector3(0.18252559f, 0.184338436f, 0.137825429f));

			Level.putInTheMap(WarriorCardBoard);
			Level.putInTheMap(pArcher);
			Level.putInTheMap(pWizard1);
			Level.putInTheMap(pWizard2);
			Level.putInTheMap(mow);
			Level.putInTheMap(pWizard4);
			Level.putInTheMap(pWizard5);

			int taille = Level.getListAllHeroOnMap().Count;


			for (int i = taille - 7; i < taille; i++)
			{
				Level.getListAllHeroOnMap()[i].setId(i);
				//send in client
				ServerSend.SpawnHero(Level.getListAllHeroOnMap()[i], taille - i);
			}
		}


		public static Action[] Levels = { InitialiseLevel1,InitialiseLevel2, InitialiseLevel3, InitialiseLevel4, InitialiseLevel5 };

		public static void InitialiseLevel1()
        {
			StoryManager sm = new StoryManager();
			actualMap = new Map(14, 18);
			GameManager gm = new GameManager();
			gameManager = gm;

			CreationOfHeros1(actualMap);
			gm.fightOnLevel(actualMap);


		}
		public static void InitialiseLevel2()
		{
			StoryManager sm = new StoryManager();
			actualMap = new Map(14, 18);
			GameManager gm = new GameManager();
			gameManager = gm;

			CreationOfHeros2(actualMap);
			gm.fightOnLevel(actualMap);


		}

		public static void InitialiseLevel3()
		{
			StoryManager sm = new StoryManager();
			actualMap = new Map(14, 18);
			GameManager gm = new GameManager();
			gameManager = gm;

			CreationOfHeros3(actualMap);
			gm.fightOnLevel(actualMap);

		}

		public static void InitialiseLevel4()
		{
			StoryManager sm = new StoryManager();
			actualMap = new Map(14, 18);
			GameManager gm = new GameManager();
			gameManager = gm;

			CreationOfHeros4(actualMap);
			gm.fightOnLevel(actualMap);

		}

		public static void InitialiseLevel5()
		{
			StoryManager sm = new StoryManager();
			actualMap = new Map(14, 18);
			GameManager gm = new GameManager();
			gameManager = gm;

			CreationOfHeros5(actualMap);
			gm.fightOnLevel(actualMap);

		}




	}
}
