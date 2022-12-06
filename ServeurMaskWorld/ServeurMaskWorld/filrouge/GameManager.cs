using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using ServeurMaskWorld;


namespace CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA
{
    class GameManager
    {
        public List<Hero> listOfHeroTurn;

        public GameManager()
        {

        }

        /*
		* Function that will manage the Action Movement Of an Hero 
		*/
        public void ActionMovement(Hero h, Map m)
        {

            Console.Write("Au tour de ");
            Console.Write(h.getName());
            Console.Write("\n");
            Console.Write("Action deplacement");
            Console.Write("\n");
            //creation of a copy of the map
            Map mCopy = new Map(m);
            mCopy.lookWhichCaseHeroCanMove(h.getPosLine(), h.getPosColumn(), h);

            m.lookWhichCaseHeroCanMove(h.getPosLine(), h.getPosColumn(), h);
            m.show();


            while (true)
            {
                ConsoleKey input;
                input = Console.ReadKey().Key;

                // Console.Clear();

                if (!(input == ConsoleKey.UpArrow || input == ConsoleKey.DownArrow || input == ConsoleKey.LeftArrow || input == ConsoleKey.RightArrow))
                {
                    break;
                }

                InputUser.InputMovement(input, mCopy, h);

                Console.Write("Au tour de ");
                Console.Write(h.getName());
                Console.Write("\n");
                Console.Write("Action deplacement");
                Console.Write("\n");
                mCopy.show();
                //}
                m.copyValuesFromOtherMap(mCopy);
                m.show();
            }
        }

        //Movement IA
        public Tuple<int, List<Vector2>> movementIA(Hero h, Map m)
        {

           //if not IA in Range

            Console.WriteLine("!!! Aucun hero dans la range de l'ennemi !!!");

            //hero goes near Ally
            Hero hNearestAlly = nearestAlly(h, m);

            m.lookWhichCaseHeroCanMove(h.getPosLine(), h.getPosColumn(), h); 





            //get path of Astar
            List<Vector2> astar = m.FindPath(hNearestAlly.getPosColumn(), hNearestAlly.getPosLine());

            int range = h.getMovement();
            int indexToMove = -1; // astar index to move 

            if (astar.Count - 2 <= range)
            {
                // move to max near ally
                indexToMove = 1;
            }
            else
            {
                //movement less range to obtain where to go
                indexToMove = astar.Count - 1 - range;
            }


            // ennemy goes near Ally --> index to move

            m.moveHeroTo((int)astar[indexToMove].Y, (int)astar[indexToMove].X, h);

            return new Tuple<int, List<Vector2>>(indexToMove, astar);


        }

        //Attack of IA
        public void attackIA(Hero h, Map m)
        {
            
            for (int i = 0; i < m.getListAllAllyHeroOnMap().Count; i++) // --> hero that we want to attack
           {
                Hero hAlly = m.getListAllAllyHeroOnMap()[i];
                bool heroDied = false;
                if (h.getSpell().isSpellInRange(hAlly.getPosLine(), hAlly.getPosColumn(), h.getPosLine(), h.getPosColumn())) //look if spell in range
                {
                    if (h.getMana() - h.getSpell().ManaCost > 0)
                    {
                        h.setMana(h.getMana() - h.getSpell().ManaCost);
                        ServerSend.SendUpdateStatHero(h, false, 0);
                    }
                    else
                    {
                        h.HasAlreadyAttack = true;
                        return;
                    }
                    //write in server
                    Console.Write("les points de vie actuel de: ");
                    Console.Write(hAlly.getName());
                    Console.Write(" : ");
                    Console.Write(hAlly.getHp());
                    Console.Write(" HP");
                    Console.Write("\n");
                    Console.Write("l'attaque lui a fait :");
                    Console.Write(Constants.ANSI_COLOR_RED);
                    Console.Write("-");
                    Console.Write(h.getWeapon().getPower());
                    Console.Write(Constants.ANSI_COLOR_RESET);
                    Console.Write(" HP");
                    Console.Write("\n");
                    h.getSpell().doDamage(h, hAlly, 0);
                    //hAlly.setHp(hAlly.getHp() - h.getWeapon().getPower());

                    if (hAlly.getHp() > 0)
                    {
                        Console.Write("Nouveaux points de vie fixee e :");
                        Console.Write(Constants.ANSI_COLOR_GREEN);
                        Console.Write(hAlly.getHp());
                        Console.Write(Constants.ANSI_COLOR_RESET);
                        Console.Write(" HP");
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write(Constants.ANSI_COLOR_RED);
                        Console.Write("Le hero est mort par ce coup fatal");
                        Console.Write(Constants.ANSI_COLOR_RESET);
                        Console.Write("\n");

                        // remove le hero mort de la map
                        m.tab2D[hAlly.getPosLine(), hAlly.getPosColumn()] = (int)Constants.Case.cEmpty;

                        // list.Remove(hero);
                        m.getListAllAllyHeroOnMap().Remove(hAlly);
                        //new 
                        //m.getListAllHeroOnMap().Remove(hAlly);

                     //   Console.Read();
                        //Console.Clear()
                        heroDied = true; ;
                        //break;
                    }

                    h.HasAlreadyAttack = true;
                    
                    ServerSend.SendUpdateStatHero(hAlly, heroDied, h.getWeapon().getPower());
                    break; // attaque seulement 1 allié
                }

            }
        }

        // attack of an ally
        public void attackAlly(Hero h, Map m)
        {
            int count = 0;
            if (h.getSpell() == Spell.DeathhMark)
            {
                for (int i = 0; i < m.getListAllHeroOnMap().Count; i++) // --> hero that we want to attack
                {
                    Hero hero = m.getListAllHeroOnMap()[i];

                    if (hero.mishkaHasAttacked)
                    {
                        count++;
                    }
                }
             }



            for (int i = 0; i < m.getListAllHeroOnMap().Count; i++) // --> hero that we want to attack
            {
                Hero hero = m.getListAllHeroOnMap()[i];
                /*if (h.getSpell() == Spell.DeathhMark)
                {
                    if (hero.mishkaHasAttacked)
                    {
                        hero.setHp(hero.getHp() - 5 * count);
                    }
                    continue;
                }*/
                foreach (Point p in h.getListTargetAttack())
                {
                    int oldHp = hero.getHp();
                    if(h.getSpell() == Spell.SpawnPlant)
                    {
                        ProgramFilRouge.SpawnPlantOberli(m, p.getX()+h.getPosLineAttack(), p.getY()+h.getPosColumnAttack());
                        return;
                    }

                    bool heroDied = false;
                    if (hero.getPosColumn() == p.getY() + h.getPosColumnAttack() && hero.getPosLine() == p.getX() + h.getPosLineAttack() || hero.mishkaHasAttacked && h.getSpell() == Spell.DeathhMark)
                    {
                        if (h.getId() % 2 == hero.getId() % 2 && ProgramFilRouge.isLevel2 && h.getCType() == Constants.Case.cAllyHero)
                        {
                            continue;
                        }
                        //goes here if range spell touch hero
                        Console.Write("les points de vie actuel de: ");
                        Console.Write(hero.getName());
                        Console.Write(" : ");
                        Console.Write(hero.getHp());
                        Console.Write(" HP");
                        Console.Write("\n");
                        Console.Write("l'attaque lui a fait :");
                        Console.Write(Constants.ANSI_COLOR_RED);
                        Console.Write("-");
                        Console.Write(h.getWeapon().getPower());
                        Console.Write(Constants.ANSI_COLOR_RESET);
                        Console.Write(" HP");
                        Console.Write("\n");

                        //hero.setHp(hero.getHp() - h.getWeapon().getPower());
                        h.getSpell().doDamage(h, hero,count);


                        if (hero.getHp() > 0)
                        {
                            Console.Write("Nouveaux points de vie fixee e :");
                            Console.Write(Constants.ANSI_COLOR_GREEN);
                            Console.Write(hero.getHp());
                            Console.Write(Constants.ANSI_COLOR_RESET);
                            Console.Write(" HP");
                            Console.Write("\n");
                        }
                        else
                        {
                            Console.Write(Constants.ANSI_COLOR_RED);
                            Console.Write("Le hero est mort par ce coup fatal");
                            Console.Write(Constants.ANSI_COLOR_RESET);
                            Console.Write("\n");

                            // remove hero on the map
                            m.tab2D[hero.getPosLine(), hero.getPosColumn()] = (int)Constants.Case.cEmpty;

                            //kill hero
                            if (hero.getCType() == Constants.Case.cAllyHero)
                            {
                                m.getListAllAllyHeroOnMap().Remove(hero);
                            }
                            else
                            {
                                m.getListAllEnnemyHeroOnMap().Remove(hero);
                            }
                            //m.getListAllHeroOnMap().Remove(hero);
                            heroDied = true;
                        }

                        //send in client the death and stats status
                        ServerSend.SendUpdateStatHero(hero, heroDied, oldHp - hero.getHp()) ;
                    }
                    h.HasAlreadyAttack = true;

                }

            }
           /* if (h.getSpell() == Spell.DeathhMark)
            {
                for (int i = 0; i < m.getListAllHeroOnMap().Count; i++) // --> hero that we want to attack
                {
                    Hero hero = m.getListAllHeroOnMap()[i];

                    if (hero.mishkaHasAttacked)
                    {
                        hero.setHp(hero.getHp() - 5*count);
                        ServerSend.SendUpdateStatHero(hero, heroDied, h.getWeapon().getPower());
                    }
                }
            }*/

        }


        //look nearest Ally
        public Hero nearestAlly(Hero h, Map m)
        {
            Hero hNearestAlly = m.getListAllAllyHeroOnMap()[0]; // first in the list by default
            double hNearestDistance = Math.Sqrt(Math.Pow(hNearestAlly.getPosColumn() - h.getPosColumn(), 2) + Math.Pow(hNearestAlly.getPosLine() - h.getPosLine(), 2));

            foreach (Hero hAlly in m.getListAllAllyHeroOnMap()) 
            {
                // Distance between ennemy and ally to attack
                double dist = Math.Sqrt(Math.Pow(hAlly.getPosColumn() - h.getPosColumn(), 2) + Math.Pow(hAlly.getPosLine() - h.getPosLine(), 2));

                if (dist < hNearestDistance)
                {
                    hNearestDistance = dist;
                    hNearestAlly = hAlly;
                }

            }

            return hNearestAlly;
        }

        /*
        * to put a log of the heros on the map on a file
        */
        public void Logger(string texte)
        {
            string path = Path.GetFullPath(Constants.LOG_FILE_PATH);
            using (StreamWriter fs = new StreamWriter(path))
            {
                fs.WriteLine(texte);
                fs.Close();
            }
        }


        /*
		* Function that will manage the Action Atack Of an Hero
		*/
        public void ActionAttack(Hero h, Map m, List<Hero> list)
        {
            if (!h.HasAlreadyAttack)
            {
                Console.Write("Au tour de ");
                Console.Write(h.getName());
                Console.Write("\n");
                Console.Write("Action attaque");
                Console.Write("\n");
                Map mCopy = new Map(m);
                mCopy.lookWhichCaseTargetCanMove(h.getPosLine(), h.getPosColumn(), h);
                mCopy.show();
                h.setPosLineAttack(h.getPosLine());
                h.setPosColumnAttack(h.getPosColumn());
                while (true)
                {

                    ConsoleKey input;
                    input = Console.ReadKey().Key;

                    if (!(input == ConsoleKey.UpArrow || input == ConsoleKey.DownArrow || input == ConsoleKey.LeftArrow || input == ConsoleKey.RightArrow))
                    {
                        break;
                    }

                    InputUser.InputMovementTarget(input, mCopy, h);

                    Console.Write("Au tour de ");
                    Console.Write(h.getName());
                    Console.Write("\n");
                    Console.Write("Action attaque");
                    Console.Write("\n");
                    mCopy.lookWhichCaseTargetCanMove(h.getPosLine(), h.getPosColumn(), h);
                    mCopy.show();
                    mCopy.copyValuesFromOtherMap(m);

                }
                // Console.Clear();
                //look if the attack of an hero touch something

                for (int i = 0; i < m.getListAllHeroOnMap().Count; i++)
                {
                    Hero hero = m.getListAllHeroOnMap()[i];
                    foreach (Point p in h.getListTargetAttack())
                    {
                        if (hero.getPosColumn() == p.getY() + h.getPosColumnAttack() && hero.getPosLine() == p.getX() + h.getPosLineAttack())
                        {
                            Console.Write("les points de vie actuel de: ");
                            Console.Write(hero.getName());
                            Console.Write(" : ");
                            Console.Write(hero.getHp());
                            Console.Write(" HP");
                            Console.Write("\n");
                            Console.Write("l'attaque lui a fait :");
                            Console.Write(Constants.ANSI_COLOR_RED);
                            Console.Write("-");
                            Console.Write(h.getWeapon().getPower());
                            Console.Write(Constants.ANSI_COLOR_RESET);
                            Console.Write(" HP");
                            Console.Write("\n");
                            hero.setHp(hero.getHp() - h.getWeapon().getPower());

                            if (hero.getHp() > 0)
                            {
                                Console.Write("Nouveaux points de vie fixee e :");
                                Console.Write(Constants.ANSI_COLOR_GREEN);
                                Console.Write(hero.getHp());
                                Console.Write(Constants.ANSI_COLOR_RESET);
                                Console.Write(" HP");
                                Console.Write("\n");
                            }
                            else
                            {
                                Console.Write(Constants.ANSI_COLOR_RED);
                                Console.Write("Le hero est mort par ce coup fatal");
                                Console.Write(Constants.ANSI_COLOR_RESET);
                                Console.Write("\n");
                                m.tab2D[hero.getPosLine(), hero.getPosColumn()] = (int)Constants.Case.cEmpty;
                                list.Remove(hero);
                                if (hero.getCType() == Constants.Case.cAllyHero)
                                {
                                    m.getListAllAllyHeroOnMap().Remove(hero);
                                }
                                else
                                {
                                    m.getListAllEnnemyHeroOnMap().Remove(hero);
                                }
                                //m.getListAllHeroOnMap().Remove(hero);

                                Console.Read();
                                //Console.Clear();
                                break;
                            }
                            Console.Read();
                            //Console.Clear();
                        }
                        else
                        {
                        }
                    }
                    if (hero != null)
                    {
                        //Logger("Name: " + hero.getName() + " Hp:" + Convert.ToString(hero.getHp()) + " PosLine: " + Convert.ToString(hero.getPosLine()) + " PosColumn: " + Convert.ToString(hero.getPosColumn()));
                    }
                }
            }
        }



        /*
		* Function that will manage the Actions Of an Hero
		*/
        public void heroActionPlay(Hero h, Map m)
        {
            h.HasAlreadyAttack = false; // reset attack state
            ActionMovement(h, m);
            ActionAttack(h, m, listOfHeroTurn);
        }


        /*
		* display Victory on screen
		*/
        public void ShowVictory()
        {
            Console.Write(":::     ::: :::::::::::  ::::::::  :::::::::::  ::::::::  :::::::::  :::   ::: ");
            Console.Write("\n");
            Console.Write(":+:     :+:     :+:     :+:    :+:     :+:     :+:    :+: :+:    :+: :+:   :+: ");
            Console.Write("\n");
            Console.Write("+:+     +:+     +:+     +:+            +:+     +:+    +:+ +:+    +:+  +:+ +:+");
            Console.Write("\n");
            Console.Write("+#+     +:+     +#+     +#+            +#+     +#+    +:+ +#++:++#:    +#++:");
            Console.Write("\n");
            Console.Write(" +#+   +#+      +#+     +#+            +#+     +#+    +#+ +#+    +#+    +#+");
            Console.Write("\n");
            Console.Write("  #+#+#+#       #+#     #+#    #+#     #+#     #+#    #+# #+#    #+#    #+#");
            Console.Write("\n");
            Console.Write("    ###     ###########  ########      ###      ########  ###    ###    ###");
            Console.Write("\n");
        }



        /*
        * display Defeat on screen
        */
        public void ShowDefeat()
        {
            Console.Write(":::::::::  :::::::::: :::::::::: ::::::::::     :::     :::::::::::");
            Console.Write("\n");
            Console.Write(":+:    :+: :+:        :+:        :+:          :+: :+:       :+:    ");
            Console.Write("\n");
            Console.Write("+:+    +:+ +:+        +:+        +:+         +:+   +:+      +:+    ");
            Console.Write("\n");
            Console.Write("+#+    +:+ +#++:++#   :#::+::#   +#++:++#   +#++:++#++:     +#+");
            Console.Write("\n");
            Console.Write("+#+    +#+ +#+        +#+        +#+        +#+     +#+     +#+");
            Console.Write("\n");
            Console.Write("#+#    #+# #+#        #+#        #+#        #+#     #+#     #+#");
            Console.Write("\n");
            Console.Write("#########  ########## ###        ########## ###     ###     ###");
            Console.Write("\n");
        }



        /*
        * Creates the battle 
        */
        public void fightOnLevel(Map level)
        {

            //order on the map
            listOfHeroTurn = level.getListAllHeroOnMap();
            listOfHeroTurn.Sort((Hero a, Hero b) => a.getSpeed().CompareTo(b.getSpeed()));
            listOfHeroTurn.Reverse();

            newTurn(level);
            
        }

        //begin enw turn
        public void newTurn(Map level)
        {
            System.Threading.Thread.Sleep(500);
            Hero h = null;
            h = listOfHeroTurn[0];
            level.heroPlaying = h;
            if (h.getHp() <= 0)
            {
                endTurn(level);
                return;
            }
            if (level.getListAllEnnemyHeroOnMap().Count == 0)
                //change to debug
            {
                ShowVictory();
                ServerSend.ChangeScene(9);
            }
            //look if there is no ally on the map
            else if (level.getListAllAllyHeroOnMap().Count == 0)
            {
                ShowDefeat();
                ServerSend.ChangeScene(23);
            }
            else
            {
                ServerSend.SendHeroPlaying(h);
                if ((int)h.getCType() == (int)Constants.Case.cEnnemyHero && h.getCanControl() == false)
                {
                    
                     h.HasAlreadyAttack = false;
                     attackIA(h, level);
                     if (!h.HasAlreadyAttack)
                     {
                         ServerSend.SendNewPosHero(movementIA(h, level));
                     }
                     else
                     {
                         endTurn(level);
                     }
                }
            }
        }

        //end turn of hero
        public void endTurn(Map level)
        {
            bool pass = false;
            if(listOfHeroTurn[1].getName() == "Mow")
            {
                if (ProgramFilRouge.isLevel1 && ProgramFilRouge.SpawnLevel1.Count != 0)
                {
                    ProgramFilRouge.SpawnLevel1[0]();
                    ProgramFilRouge.SpawnLevel1.RemoveAt(0);
                    pass = true;


                }
                if (ProgramFilRouge.isLevel3 && ProgramFilRouge.SpawnLevel3.Count != 0)
                {
                    ProgramFilRouge.SpawnLevel3[0]();
                    ProgramFilRouge.SpawnLevel3.RemoveAt(0);
                    pass = true;
                }
                if (ProgramFilRouge.isLevel4 && ProgramFilRouge.SpawnLevel4.Count != 0)
                {
                    ProgramFilRouge.SpawnLevel4[0]();
                    ProgramFilRouge.SpawnLevel4.RemoveAt(0);
                    pass = true;
                }
            }
            level.clearTarget();
            /*if (level.heroPlaying.getHp() > 0)
            {
                listOfHeroTurn.Add(level.heroPlaying);
                listOfHeroTurn.RemoveAt(0);
            }*/
            listOfHeroTurn.Add(level.heroPlaying);
            listOfHeroTurn.RemoveAt(0);
            if (pass)
            {
                listOfHeroTurn = level.getListAllHeroOnMap();
                listOfHeroTurn.Sort((Hero a, Hero b) => a.getSpeed().CompareTo(b.getSpeed()));
                listOfHeroTurn.Reverse();
            }
            //ProgramFilRouge.InitialiseLevel1();
            newTurn(level);
        }

    }
}
