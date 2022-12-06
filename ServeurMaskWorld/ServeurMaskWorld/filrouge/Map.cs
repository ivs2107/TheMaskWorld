using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
//to remover maybe
using ServeurMaskWorld;

namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
    /*
    * Class Map is the class that manage the position of heros and ennemies
    */
    class Map
    {
        public int[,] tab2D;
		public Map mCopy;
		public static int[,] tab2DAStar;
		private List<Hero> listAllHeroOnMap = new List<Hero>();
		private List<Hero> listAllAllyHeroOnMap = new List<Hero>();
		private List<Hero> listAllEnnemyHeroOnMap = new List<Hero>();
		public int line;
		public int column;
		public Hero heroPlaying;

		public Map(int _line, int _column)
        {
            this.line = _line;
            this.column = _column;
            //look if not enough memory
            try
            {
                //creation of the array 2D
                tab2D = new int[line, column];
				//creation of the array Astar2D
				tab2DAStar = new int[line, column];
				//fill of values for tab2D
				for (int i = 0; i < line; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        tab2D[i, j] = (int)Constants.Case.cEmpty;
						tab2DAStar[i, j] = int.MaxValue;

					}
                }
            }
            catch (Exception e)
            {
                Console.Write("  catched in Map(int, int) >>> ");
                Console.Write(e.Message);
                Console.Write("\n");
                cleanData();
            }

			listAllHeroOnMap = new List<Hero>();
			listAllAllyHeroOnMap = new List<Hero>();
			listAllEnnemyHeroOnMap = new List<Hero>();

		}

		public Map(Map map)
        {
			tab2D = arrayCopy(map.tab2D);
			column = map.column;
			line = map.line;
			listAllHeroOnMap = listCopy(map.listAllHeroOnMap);
			listAllAllyHeroOnMap = listCopy(map.listAllAllyHeroOnMap);
			listAllEnnemyHeroOnMap = listCopy(map.listAllEnnemyHeroOnMap);
		}

		public void cleanData()
		{
			Array.Clear(tab2D, 0, column * line);
		}

		public List<Hero> listCopy(List<Hero> listToCopy)
        {
			List<Hero> copy = new List<Hero>();

			foreach(Hero h in listToCopy){
				// use serialization to deep copy the object
				copy.Add(Hero.CreateDeepCopy(h));
            }

			return copy;
        }

		// deep copy array
		public int[,] arrayCopy(int[,] input)
		{
			int[,] result = new int[input.GetLength(0), input.GetLength(1)]; 
			for (int x = 0; x < input.GetLength(0); ++x) 
			{
				for (int y = 0; y < input.GetLength(1); ++y) 
				{
					result[x, y] = input[x, y];
				}
			}
			return result;
		}

		public void show()
		{
			for (int i = 0; i < line; i++)
			{
				for (int j = 0; j < column; j++)
				{
					//Determine which characters needs to be written in the console and colors
					if (tab2D[i, j] == (int)Constants.Case.cEmpty)
					{
						Console.Write(" . ");
					}
					else if (tab2D[i, j] == (int) Constants.Case.cAllyHero)
					{
						Console.Write(Constants.ANSI_COLOR_GREEN);
						Console.Write(" # ");
						Console.Write(Constants.ANSI_COLOR_RESET);
					}
					else if (tab2D[i, j] == (int)Constants.Case.cTarget)
					{
						Console.Write(" O ");
					}
					else if (tab2D[i, j] == (int)Constants.Case.cTargetAttack)
					{
						Console.Write(Constants.ANSI_COLOR_CYAN);
						Console.Write(" X ");
						Console.Write(Constants.ANSI_COLOR_RESET);
					}
					else if (tab2D[i, j] == (int)Constants.Case.cEnnemyHero)
					{
						Console.Write(Constants.ANSI_COLOR_RED);
						Console.Write(" # ");
						Console.Write(Constants.ANSI_COLOR_RESET);
					}
				}
				Console.Write("\n");

			}
		}


		public void putInTheMap(Hero h)
		{
			tab2D[h.getPosLine(), h.getPosColumn()] = (int) h.getCType();
			listAllHeroOnMap.Add(h);
			//put on list ally or ennemy
			if ((int)h.getCType() == (int)Constants.Case.cAllyHero)
			{
				listAllAllyHeroOnMap.Add(h);
			}
			else if ((int)h.getCType() == (int)Constants.Case.cEnnemyHero)
			{
				listAllEnnemyHeroOnMap.Add(h);
			}
		}

		//to remove
		public void movementOfHero(int posLineToAdd, int posColumnToAdd, Hero h)
		{
			int posLine = h.getPosLine();
			int posColumn = h.getPosColumn();
			//Security to look if player is out of the level or he is out of his range of movement
			if (isOutOfMap(posLine + posLineToAdd, posColumn + posColumnToAdd))
			{
				return;
			}
			//look if he is in 'O' case
			if (tab2D[posLine + posLineToAdd, posColumn + posColumnToAdd] != (int)Constants.Case.cTarget)
			{
				return;
			}
			//change values
			tab2D[posLine, posColumn] = (int)Constants.Case.cTarget;
			if ((int)h.getCType() == (int)Constants.Case.cAllyHero)
			{
				tab2D[posLine + posLineToAdd, posColumn + posColumnToAdd] = (int)Constants.Case.cAllyHero;
			}
			else if ((int)h.getCType() == (int)Constants.Case.cEnnemyHero)
			{
				tab2D[posLine + posLineToAdd, posColumn + posColumnToAdd] = (int)Constants.Case.cEnnemyHero;
			}
			h.setPosLine(posLine + posLineToAdd);
			h.setPosColumn(posColumn + posColumnToAdd);
		}

		public void moveHeroTo(int line, int column, Hero h)
        {
			int posLine = h.getPosLine();
			int posColumn = h.getPosColumn();

			//Security to look if player is out of the level or he is out of his range of movement
			if (isOutOfMap(line, column))
			{
				return;
			}
			
			//change values
			tab2D[posLine, posColumn] = (int)Constants.Case.cEmpty;
			tab2D[line, column] = (int)Constants.Case.cEnnemyHero;
			h.setPosLine(line);
			h.setPosColumn(column);
		}
		public void findPathNewPosHero(int x, int y)
        {
			List<Vector2> path = FindPath(x, y);
			setNewPosOfHero(y, x, heroPlaying);
			ServerSend.SendNewPosHero(new Tuple<int, List<Vector2>>(0 ,path));

			
		}
		public void setNewPosOfHero(int posLine, int posColumn, Hero h)
		{
			//Security to look if player is out of the level or he is out of his range of movement
			if (isOutOfMap(posLine, posColumn))
			{
				return;
			}
			//look if he is in 'O' case
			if (tab2D[posLine, posColumn] != (int)Constants.Case.cTarget)
			{
				return;
			}
			//change values
			tab2D[h.getPosLine(), h.getPosColumn()] = (int)Constants.Case.cEmpty;
			if ((int)h.getCType() == (int)Constants.Case.cAllyHero)
			{
				tab2D[posLine, posColumn] = (int)Constants.Case.cAllyHero;
			}
			else if ((int)h.getCType() == (int)Constants.Case.cEnnemyHero)
			{
				tab2D[posLine, posColumn] = (int)Constants.Case.cEnnemyHero;
			}


			h.setPosLine(posLine);
			h.setPosColumn(posColumn);
		}

		/*
		* Function that will regroup the same code that are used when LookWhichCaseHeroCanMove and LookWhichCaseTargetCanMove
		*/
		public void LookWhichCaseSomethingCanMove(int posLine, int posColumn, Hero h, Map m, int line, int column, int SizeCanMove, bool isMouvement)
		{
			for (int i = 0; i < line; i++)
			{
				for (int j = 0; j < column; j++)
				{

					if (h.isSpell1InRange(i, j) && !isMouvement)
					{
						m.tab2D[i, j] = (int)Constants.Case.cTarget;
						continue;
					}
					if ((int)h.getCType() == (int)Constants.Case.cAllyHero || h.getCanControl() == true)
					{
						//put valuue  max in Astar
						Map.tab2DAStar[i, j] = int.MaxValue;
					}
					else if ((int)h.getCType() == (int)Constants.Case.cEnnemyHero && h.getCanControl() == false)
					{

						//put valuue  max in Astar
						// Map.tab2DAStar[i, j] = int.MaxValue;

						Map.tab2DAStar[i, j] = (Math.Abs(i - posLine) + Math.Abs(j - posColumn));
						//put value in Astar
						if ((m.tab2D[i, j] == (int)Constants.Case.cEnnemyHero))
						{
							Map.tab2DAStar[i, j] = int.MaxValue;
						}
					}
					

					//With the absolute value we can see if the Hero can move around him positively or negatively
					if ((Math.Abs(i - posLine) + Math.Abs(j - posColumn) <= SizeCanMove) && isMouvement)
					{
						if (m.tab2D[i, j] == (int)Constants.Case.cEmpty)
						{
							//put value in Astar
							if ((int)h.getCType() == (int)Constants.Case.cAllyHero || h.getCanControl() == true)
							{

								Map.tab2DAStar[i, j] = (Math.Abs(i - posLine) + Math.Abs(j - posColumn));
								
								m.tab2D[i, j] = (int)Constants.Case.cTarget;
							}

                        }
					}
				}
			}
			
		}


		public void lookWhichCaseHeroCanMove(int posLine, int posColumn, Hero h)
		{
			LookWhichCaseSomethingCanMove(posLine, posColumn, h, this, line, column, h.getMovement(), true);
		}

		public void lookWhichCaseTargetCanMove(int posLine, int posColumn, Hero h)
		{
			LookWhichCaseSomethingCanMove(posLine, posColumn, h, this, line, column, h.getSpell().AttackRange, false);
		}

		//AStar Function
		public List<Vector2> FindPath(int x, int y)
		{

			List<Vector2> listCaseChoisi = new List<Vector2>();
			int caseActX = x;
			int caseActY = y;
			int nombreMagique = 1;
			int stop = 0;

			listCaseChoisi.Add(new Vector2(caseActX, caseActY));
			while (Map.tab2DAStar[caseActY, caseActX] > 1 && stop < 10)
			{
				int noSuivant = (Map.tab2DAStar[caseActY, caseActX]) - nombreMagique;
				if (Map.tab2DAStar.GetLength(1) - 1 >= caseActX + 1 && noSuivant == Map.tab2DAStar[caseActY, caseActX + 1] && Map.tab2DAStar.GetLength(1) - 1 >= caseActX + 1)
				{
					Map.tab2DAStar[caseActY, caseActX] = Map.tab2DAStar[caseActY, caseActX] + 2;
					listCaseChoisi.Add(new Vector2(caseActX + 1,caseActY));
					caseActX = caseActX + 1;
					nombreMagique=1;
				}
				else if (Map.tab2DAStar.GetLength(0) - 1 >= caseActY + 1 && noSuivant == Map.tab2DAStar[caseActY + 1, caseActX] && Map.tab2DAStar.GetLength(0) - 1 >= caseActY + 1)
				{
					Map.tab2DAStar[caseActY, caseActX] = Map.tab2DAStar[caseActY, caseActX] + 2;
					listCaseChoisi.Add(new Vector2(caseActX,caseActY + 1));
					caseActY = caseActY + 1;
					nombreMagique=1;
				}
				else if (0 <= caseActX - 1 && noSuivant == Map.tab2DAStar[caseActY, caseActX - 1] && 0 <= caseActX - 1)
				{
					Map.tab2DAStar[caseActY, caseActX] = Map.tab2DAStar[caseActY, caseActX] + 2;
					listCaseChoisi.Add(new Vector2(caseActX - 1,caseActY));
					caseActX = caseActX - 1;
					nombreMagique=1;
				}
				else if (0 <= caseActY - 1 && noSuivant == Map.tab2DAStar[caseActY - 1, caseActX] && 0 <= caseActY - 1)
				{
					Map.tab2DAStar[caseActY, caseActX] = Map.tab2DAStar[caseActY, caseActX] + 2;
					listCaseChoisi.Add(new Vector2(caseActX, caseActY - 1));
					caseActY = caseActY - 1;
					nombreMagique=1;
				}
				else
				{
					nombreMagique--;
				}
				stop++;
			}

			return listCaseChoisi;

		}

		
		






		public void movementOfTargetAttackWithKey(int posLineToAdd, int posColumnToAdd, Hero h)
		{
			int posLine = h.getPosLineAttack();
			int posColumn = h.getPosColumnAttack();
			//Security to look if player is out of the level
			if (isOutOfMap(posLine + posLineToAdd, posColumn + posColumnToAdd))
			{
				tab2D[posLine, posColumn] = (int)Constants.Case.cTargetAttack;
			}

			else if (!(h.isSpell1InRange(posLine + posLineToAdd, posColumn + posColumnToAdd)))
			{
				tab2D[posLine, posColumn] = (int)Constants.Case.cTargetAttack;
			}
			else
			{
				if (h.getSpell().IsSpellInLine)
				{
					InLineModifyPoint(h, posLineToAdd, posColumnToAdd);
				}
				//change values
				tab2D[posLine + posLineToAdd, posColumn + posColumnToAdd] = (int)Constants.Case.cTargetAttack;
				h.setPosLineAttack(posLine + posLineToAdd);
				h.setPosColumnAttack(posColumn + posColumnToAdd);
			}
			movemendOfPoint(h);


		}

		public void mouvementOfTargetAttack(int posLine, int posColumn)
        {
			//Security to look if player is out of the level
			if (isOutOfMap(posLine, posColumn))
			{
				//goes here if out of map
			}

			else if (!(heroPlaying.isSpell1InRange(posLine, posColumn)))
			{
				//goes here if spell not in range
			}
			else
			{
				if (heroPlaying.getSpell().IsSpellInLine && heroPlaying.getSpell().isVertical == false )// heroPlaying.getSpell() != Spell.VerticalSpell)
				{
					InLineModifyPoint(heroPlaying, posLine, posColumn);
				}
				else if(/*heroPlaying.getSpell() == Spell.VerticalSpell && */heroPlaying.getSpell().isVertical == true)
                {
					InLineVerticalModifyPoint(heroPlaying, posLine, posColumn); ;
                }
				//change values
				mCopy.tab2D[posLine, posColumn] = (int)Constants.Case.cTargetAttack;
				heroPlaying.setPosLineAttack(posLine);
				heroPlaying.setPosColumnAttack(posColumn);
			}
			movemendOfPoint(heroPlaying);
		}

		public bool isOutOfMap(int posLine, int posColumn)
		{
			if (posLine >= this.line || posLine < 0 || posColumn >= this.column || posColumn < 0)
			{
				return true;
			}
			return false;
		}

		/*
		* Manage the points of attack in line
		*/
		public void InLineModifyPoint(Hero h, int posLine, int posColumn)
		{
			//to modify
			foreach (Point p in h.getListTargetAttack())
			{
				if (p.getX() != 0 && posLine!= heroPlaying.getPosLine())
				{
					int x = p.getX();
					p.setY(x);
					p.setX(0);
				}
				else if (p.getY() != 0 && posColumn != heroPlaying.getPosColumn())
				{
					int y = p.getY();
					p.setX(y);
					p.setY(0);
				}
			}
		}

		public void InLineVerticalModifyPoint(Hero h, int posLine, int posColumn)
		{
			//to modify
			foreach (Point p in h.getListTargetAttack())
			{
				if (p.getX() != 0 && posColumn != heroPlaying.getPosColumn() )
				{
					int x = p.getX();
					p.setY(x);
					p.setX(0);
				}
				else if (p.getY() != 0 && posLine != heroPlaying.getPosLine())
				{
					int y = p.getY();
					p.setX(y);
					p.setY(0);
				}
				if((p.getX()>0 && posLine < heroPlaying.getPosLine()) || (p.getX() < 0 && posLine> heroPlaying.getPosLine()))
                {
					p.setX(-p.getX());
                }
				if ((p.getY() > 0 && posColumn < heroPlaying.getPosColumn()) || (p.getY() < 0 && posColumn > heroPlaying.getPosColumn()))
				{
					p.setY(-p.getY());
				}
			}
		}

		//move point
		public void movemendOfPoint(Hero h)
		{
			foreach (Point p in h.getListTargetAttack())
			{
				if (isOutOfMap(p.getX() + h.getPosLineAttack(), p.getY() + h.getPosColumnAttack()))
				{
					continue;
				}
				Console.WriteLine("tab2D[" + (p.getX() + h.getPosLineAttack()) + ";" + (p.getY() + h.getPosColumnAttack()) + "]");
				mCopy.tab2D[p.getX() + h.getPosLineAttack(), p.getY() + h.getPosColumnAttack()] = (int)Constants.Case.cTargetAttack;
			}
		}
		//copy values of map
		public void copyValuesFromOtherMap(Map OtherMap)
		{
			for (int i = 0; i < line; i++)
			{
				for (int j = 0; j < column; j++)
				{
					//We dont want to copy the targets 'O' of one map
					if (tab2D[i, j] == (int)Constants.Case.cTarget)
					{

					}
					else if (OtherMap.tab2D[i, j] != (int)Constants.Case.cTarget)
					{
						tab2D[i, j] = OtherMap.tab2D[i, j];
					}
					else if (tab2D[i, j] != (int)Constants.Case.cEmpty)
					{
						tab2D[i, j] = (int)Constants.Case.cEmpty;
					}
				}
			}
		}
		//clear all target
		public void clearTarget()
		{
			for (int i = 0; i < line; i++)
			{
				for (int j = 0; j < column; j++)
				{
					if (tab2D[i, j] == (int)Constants.Case.cTarget)
					{
						tab2D[i, j] = (int)Constants.Case.cEmpty;
					}
				}
			}
		}


		public List<Hero> getListAllHeroOnMap()
        {
            return listAllHeroOnMap;
        }

        public List<Hero> getListAllAllyHeroOnMap()
        {
            return listAllAllyHeroOnMap;
        }

        public List<Hero> getListAllEnnemyHeroOnMap()
        {
            return listAllEnnemyHeroOnMap;
        }
    }

}
