using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;
using CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA;

namespace ServeurMaskWorld
{
    class ServerHandle
    {
        //request welcome handle
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
            
        }

        public static void HelloWorld(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _texte = _packet.ReadString();

            Console.WriteLine("Server recieved from Player " + Server.clients[_fromClient].id + " with username " + Server.clients[_fromClient].player.username + "tries to say " + _texte);

            ServerSend.HelloWorldSendToEveryone(Server.clients[_fromClient].player,_texte);
        }
        //client requested movement
        public static void RequestedMovement(int _fromClient, Packet _packet)
        {
            int s = _packet.ReadInt();
            Console.WriteLine("recievedddd"); 
            ProgramFilRouge.actualMap.lookWhichCaseHeroCanMove(ProgramFilRouge.actualMap.heroPlaying.getPosLine(), ProgramFilRouge.actualMap.heroPlaying.getPosColumn(), ProgramFilRouge.actualMap.heroPlaying);
            ServerSend.SendMovement(_fromClient,true, ProgramFilRouge.actualMap);
        }

        //client request attack
        public static void RequestAttack(int _fromClient, Packet _packet)
        {

            int idSpell = _packet.ReadInt();
            Console.WriteLine("recieved Attack");
            ProgramFilRouge.actualMap.mCopy = new Map(ProgramFilRouge.actualMap);
            ProgramFilRouge.actualMap.heroPlaying.setSpell(idSpell);
            ProgramFilRouge.actualMap.mCopy.lookWhichCaseTargetCanMove(ProgramFilRouge.actualMap.heroPlaying.getPosLine(), ProgramFilRouge.actualMap.heroPlaying.getPosColumn(), ProgramFilRouge.actualMap.heroPlaying);
            ServerSend.SendMovement(_fromClient, false, ProgramFilRouge.actualMap.mCopy);
        }
        //client requested to move hero into new pos
        public static void RequestMoveHero(int _fromClient, Packet _packet)
        {
            int x = _packet.ReadInt();
            int y = _packet.ReadInt();
            ProgramFilRouge.actualMap.findPathNewPosHero(x,y);
        }

        //client requested to see the target attack
        public static void RequestTargetAttack(int _fromClient, Packet _packet)
        {
            int x = _packet.ReadInt();
            int y = _packet.ReadInt();
            ProgramFilRouge.actualMap.mouvementOfTargetAttack(y, x);
            ServerSend.SendTargetAttack(_fromClient);
            ProgramFilRouge.actualMap.mCopy.copyValuesFromOtherMap(ProgramFilRouge.actualMap);
            ProgramFilRouge.actualMap.mCopy.lookWhichCaseTargetCanMove(ProgramFilRouge.actualMap.heroPlaying.getPosLine(), ProgramFilRouge.actualMap.heroPlaying.getPosColumn(), ProgramFilRouge.actualMap.heroPlaying);
        }

        //client requested to do action of attack
        public static void RequestAttackHero(int _fromClient, Packet _packet)
        {
            int x = _packet.ReadInt();
            int y = _packet.ReadInt();

            if (ProgramFilRouge.actualMap.heroPlaying.getMana() - ProgramFilRouge.actualMap.heroPlaying.getSpell().ManaCost > 0)
            {
                ProgramFilRouge.actualMap.heroPlaying.setMana(ProgramFilRouge.actualMap.heroPlaying.getMana() - ProgramFilRouge.actualMap.heroPlaying.getSpell().ManaCost);
               // ServerSend.SendUpdateStatHero(ProgramFilRouge.actualMap.heroPlaying, false, 0);
                ProgramFilRouge.gameManager.attackAlly(ProgramFilRouge.actualMap.heroPlaying, ProgramFilRouge.actualMap);
                ServerSend.SendUpdateStatHero(ProgramFilRouge.actualMap.heroPlaying, false, 0);
            }
            if (ProgramFilRouge.actualMap.heroPlaying.getSpell() == Spell.ResetSpell)
            {
                if (ProgramFilRouge.actualMap.heroPlaying.getName() == "Keirowz")
                {
                    for (int i = 0; i < ProgramFilRouge.actualMap.heroPlaying.getTabSpells().Length; i++)
                    {
                        ProgramFilRouge.actualMap.heroPlaying.getTabSpells()[i].Damage = -ProgramFilRouge.actualMap.heroPlaying.getTabSpells()[i].Damage;
                    }
                }
                ProgramFilRouge.gameManager.newTurn(ProgramFilRouge.actualMap);
                return;
            }
            ProgramFilRouge.gameManager.endTurn(ProgramFilRouge.actualMap);
        }


        //client requested end turn
        public static void RequestEndTurn(int _fromClient, Packet _packet)
        {
            if(ProgramFilRouge.actualMap.heroPlaying.getCType() == Constants.Case.cEnnemyHero && ProgramFilRouge.actualMap.heroPlaying.getCanControl() == false)
            {
                ProgramFilRouge.gameManager.attackIA(ProgramFilRouge.actualMap.heroPlaying, ProgramFilRouge.actualMap);
                System.Threading.Thread.Sleep(500);
            }
            ProgramFilRouge.gameManager.endTurn(ProgramFilRouge.actualMap);
        }

        //request to change send
        public static void ChangeScene(int _fromClient, Packet _packet)
		{
            int _idScene = _packet.ReadInt();
            ServerSend.ChangeScene( _idScene);
           
        }
        // a new player has entered in game
        public static void NewPlayerEntered(int _fromClient, Packet _packet)
		{
            int _idClient = _packet.ReadInt();
            ServerSend.NewPlayerEntered(Server.clients[_fromClient].player);
        }
        //the player is connected
        public static void PlayerConnected(int _fromClient, Packet _packet)
		{
            bool _isClientConnected = _packet.ReadBool();
            Console.WriteLine("Client Connecté ? : " + Server.clients[_fromClient].player.username + ": " + _isClientConnected);
            ServerSend.PlayerConnectedSendToEveryOne(Server.clients[_fromClient].player, _isClientConnected);
        }

        //intialise a level
        public static void InitialiseLevel(int _fromClient, Packet _packet)
        {        
            if (_fromClient == Server.clients[1].id)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Spawn heros");
                int index = _packet.ReadInt();
                ProgramFilRouge.Levels[index - 1]();
            }
        }

        private static int counterPlayerSelectionHero = 0;
        //wait that everyone is connected
        public static void PlayerSelectHero(int _fromClient, Packet _packet) 
        {
            
            string _heroName = _packet.ReadString();
            Console.WriteLine("Nom du héro selectionné par " + Server.clients[_fromClient].player.username + ": " + _heroName);
            ServerSend.PlayerChooseHero(Server.clients[_fromClient].player, _heroName);
            Server.clients[_fromClient].player.character = _heroName;
            ++counterPlayerSelectionHero;

            Console.WriteLine("NB JOUEURS QUI ONT VALIDER LE HERO : " + counterPlayerSelectionHero);
            if(counterPlayerSelectionHero == 8)//to change
			{
                ServerSend.ChangeScene(2);
			}
            
        }



        public static void RequestSentence(int _fromClient, Packet _packet)
        {
            ServerSend.SendNextSentence();
        }



        public static void ShowAttacksAvailableToClient(int _fromClient, Packet _packet)
        {
            ServerSend.SendAttacksAvailableToClient(_fromClient);
        }
    }
}
