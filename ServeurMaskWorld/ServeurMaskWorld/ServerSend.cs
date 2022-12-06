using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using CSharp_CPP_FilRouge_ISCe_PERRIN_SERRA;

namespace ServeurMaskWorld
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }
        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }


        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }




        #region Packets
        //welcome the client
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }
        //Spawn new player on client
        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPData(_toClient, _packet);
            }
        }

		

		public static void HelloWorldSendToEveryone(Player _player,string _texte)
        {
            using (Packet _packet = new Packet((int)ServerPackets.helloWorldSendToEveryone))
            {
                _packet.Write(_player.id);
                _packet.Write(_texte);
                SendUDPDataToAll(_packet);
            }
        }
        //change scene to everyone
        public static void ChangeScene(int _idScene)
        {
            using (Packet _packet = new Packet((int)ServerPackets.changeScene))
            {
                _packet.Write(_idScene);
                SendUDPDataToAll(_packet);
            }
        }
        //send to client to increase number in lobby
        public static void NewPlayerEntered(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.newPlayerEntered))
            {
                _packet.Write(_player.id);
                SendUDPDataToAll(_packet);
            }
        }
        //player connected send to everyone
        public static void PlayerConnectedSendToEveryOne(Player _player, bool _isConnected)
		{
            using (Packet _packet = new Packet((int)ServerPackets.playerConnectedSendToEveryone))
            {
                _packet.Write(_isConnected);
                SendUDPDataToAll(_packet);
            }
        }
        //send to cleint the hero that he choose
        public static void PlayerChooseHero(Player _player, string _heroName)
		{
            using (Packet _packet = new Packet((int)ServerPackets.playerSelectedHero))
            {
                _packet.Write(_heroName);
                SendUDPDataToAll(_packet);
            }
        }

        //say to client that he can spawn ennemies
        public static void SpawnHero(Hero hero,int heroRemaining)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnHero))
            {
               _packet.Write(hero);
               _packet.Write(heroRemaining);
               SendUDPDataToAll(_packet);
            }
        }

        //send hero palying to client
        public static void SendHeroPlaying(Hero hero)
        {
            bool evolve= hero.canEvolve();
            if (evolve)
            {
                hero.setHp(hero.getHp() + 10);
                hero.setMana(hero.getMana() + 10);
                hero.getWeapon().setPower(6);
            }
            using (Packet _packet = new Packet((int)ServerPackets.sendHeroPlaying))
            {
                _packet.Write(evolve);
                _packet.Write(hero);
              
                SendUDPDataToAll(_packet);
               
            }
        }

        //send a list of all case of mouvement
        public static void SendMovement(int _fromClient, bool isMovement,Map map)
        {

            using (Packet _packet = new Packet((int)ServerPackets.sendMovement))
            {
                _packet.Write(isMovement);
                for (int i = 0; i < map.line; i++)
                {
                    for (int j = 0; j < map.column; j++)
                    {
                        if (map.tab2D[i, j] == (int)Constants.Case.cTarget)
                        {
                            _packet.Write(i);
                            _packet.Write(j);
                            
                        }
                    }
                }
                _packet.Write(-1);
                _packet.Write(-1);


                SendUDPData(_fromClient, _packet);
            }
        }
        //send a list of case of attack
        public static void SendTargetAttack(int _fromClient)
        {
            ProgramFilRouge.actualMap.mCopy.show();
            using (Packet _packet = new Packet((int)ServerPackets.sendTargetAttack))
            {
                for (int i = 0; i < ProgramFilRouge.actualMap.line; i++)
                {
                    for (int j = 0; j < ProgramFilRouge.actualMap.column; j++)
                    {
                        if (ProgramFilRouge.actualMap.mCopy.tab2D[i, j] == (int)Constants.Case.cTargetAttack)
                        {
                            _packet.Write((int)Constants.Case.cTargetAttack);
                            _packet.Write(i);
                            _packet.Write(j);
                        }
                        if (ProgramFilRouge.actualMap.mCopy.tab2D[i, j] == (int)Constants.Case.cTarget)
                        {
                            _packet.Write((int)Constants.Case.cTarget);
                            _packet.Write(i);
                            _packet.Write(j);
                        }
                    }
                }
                _packet.Write(-1);
                _packet.Write(-1);
                _packet.Write(-1);

                SendUDPData(_fromClient, _packet);
            }
        }

        //send new pos Hero to client
        public static void SendNewPosHero(Tuple <int, List<Vector2>> tuple)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendNewPosHero))
            {
                for (int i = tuple.Item2.Count - 1; i >= tuple.Item1; i--)
                {
                    _packet.Write((int)tuple.Item2[i].Y);
                    _packet.Write((int)tuple.Item2[i].X);
                }
                _packet.Write(-1);
                _packet.Write(-1);
                SendUDPDataToAll(_packet);
            }
            ProgramFilRouge.actualMap.clearTarget();
            ProgramFilRouge.actualMap.show();
        }
        //update sat hero to client
        public static void SendUpdateStatHero(Hero hero,bool heroDied,int damageDealed)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendUpdateStatHero))
            {
                _packet.Write(damageDealed);
                _packet.Write(heroDied);
                _packet.Write(hero);
                SendUDPDataToAll(_packet);
            }
            if (heroDied)
            {
                //ServerSend.SendHeroPlaying(hero);
                //System.Threading.Thread.Sleep(1000);
            }
        }


        public static void SendNextSentence()
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendNextSentence))
            {
                SendUDPDataToAll(_packet);
            }
        }


        public static void SendAttacksAvailableToClient(int _fromClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendsAttacksAvailableToClient))
            {
                Spell[] tabSpells = ProgramFilRouge.actualMap.heroPlaying.getTabSpells();
                _packet.Write(tabSpells[0].ToString());
                _packet.Write(tabSpells[1].ToString());
                _packet.Write(tabSpells[2].ToString());
                SendUDPData(_fromClient, _packet);
            }
        }

        #endregion
    }
}
