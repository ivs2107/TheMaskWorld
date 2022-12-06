using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void HelloWorld(string scene)
    {
        using (Packet _packet = new Packet((int)ClientPackets.helloWorld))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(scene);

            SendTCPData(_packet);
        }
    }




    /*
     * UDP -> Bonne perf mais pas sécurisé
     * TCP -> Moins bonne perf mais + sécurisé
     */
    public static void ChangeScene(int sceneID)
    {
        using (Packet _packet = new Packet((int)ClientPackets.changeScene))
        {
            _packet.Write(sceneID);
            SendUDPData(_packet);
        }
    }

    public static void RequestMovement()
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestMovement))
        {
            _packet.Write(1);
            SendUDPData(_packet);
        }
    }

    public static void RequestAttack(int idSpell)
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestAttack))
        {
            // _packet.Write(1);
            _packet.Write(idSpell);
            SendUDPData(_packet);
        }
    }

    public static void ShowAttacksAvailable()
    {
        using (Packet _packet = new Packet((int)ClientPackets.showAttacksAvailable))
        {
            // _packet.Write(1);
            SendUDPData(_packet);
        }
    }

    public static void RequestEndTurn()
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestEndTurn))
        {
            SendUDPData(_packet);
        }
    }

    public static void MoveHero(int x,int y)
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestMoveHero))
        {
            _packet.Write(x);
            _packet.Write(y);
            SendUDPData(_packet);
        }
    }

    public static void NewPlayerEntered(int clientID)
    {
        using (Packet _packet = new Packet((int)ClientPackets.newPlayerEntered))
        {
            _packet.Write(clientID);
            SendUDPData(_packet);
        }
    }
    public static void AttackZone(int x, int y)
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestAttackHero))
        {
            _packet.Write(x);
            _packet.Write(y);
            SendUDPData(_packet);
        }
    }

    public static void RequestTargetAttack(int x, int y)
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestTargetAttack))
        {
            _packet.Write(x);
            _packet.Write(y);
            SendUDPData(_packet);
        }
    }

    public static void PlayerConnected(bool isConnected)
	{
        using (Packet _packet = new Packet((int)ClientPackets.playerConnected))
        {
            _packet.Write(isConnected);
            SendUDPData(_packet);
        }
	}

    public static void InitialiseLevel(int number)
    {
        using (Packet _packet = new Packet((int)ClientPackets.initialiseLevel))
        {
            _packet.Write(number);
            SendUDPData(_packet);
        }
    }

    public static void HeroSelected(string heroName)
	{
        using (Packet _packet = new Packet((int)ClientPackets.playerSelectedHero))
        {
            _packet.Write(heroName);
            SendUDPData(_packet);
        }
    }
    public static void DisplayNextSentenceForAll()
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestNextSentence))
        {
            SendUDPData(_packet);
        }
    }

    #endregion

}