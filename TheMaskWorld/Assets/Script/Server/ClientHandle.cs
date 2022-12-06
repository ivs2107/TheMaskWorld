using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();
       // GameManager.instance.LoadNewScene();
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);

        
        GameManager.instance.LoadNewScene();
        
    }



    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void HelloWorldToEveryone(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _msg = _packet.ReadString();
        Debug.Log($"Message from server of User :{ GameManager.players[_id].username} with message : {_msg}");
    }

    public static void SpawnHero(Packet _packet)
    {
        string name = _packet.ReadString();
        int agility = _packet.ReadInt();
        int intelligence = _packet.ReadInt();
        int hp = _packet.ReadInt();
        int mana = _packet.ReadInt();
        int posLine = _packet.ReadInt();
        int posColumn = _packet.ReadInt();
        GameManager.typeHero type = (GameManager.typeHero)_packet.ReadInt();
        int id = _packet.ReadInt();
        Vector3 scaleImage = _packet.ReadVector3();
        bool canControl = _packet.ReadBool();
        int heroRemaining = _packet.ReadInt();
        while (GameManager.instance == null) { Debug.Log("s"); }
        try
        {
            GameManager.instance.CreateHero(name, hp, mana, posColumn, posLine, id, scaleImage, canControl, type);
        }
        catch (Exception e)
        {
        }
        //GameManager.instance.CreateHero(name, hp, mana, posColumn, posLine,id,scaleImage, canControl,type);
        if (heroRemaining == 1)
        {
            GameManager.instance.SpawnHero();
        }
    }
        public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }
    public static void ChangeScene(Packet _packet)
    {
        int SceneID = _packet.ReadInt();
        GameManager.instance.LoadNewScene(SceneID);
    }
   

    public static void NewPlayerEntered(Packet _packet)
	{
        int ClientID = _packet.ReadInt();

        GameManager.instance.NewPlayerEntered(ClientID);

    }

    public static void PlayerConnectedToEveryone(Packet _packet)
	{
        bool isConnected = _packet.ReadBool();
		if (isConnected)
		{
            GameManager.instance.CountNumberOfPlayers();
        }
       
	}

    public static void PlayerChooseHero(Packet _packet)
	{
        string heroSelected = _packet.ReadString();
        GameManager.instance.PlayerSelectedHero(heroSelected);
    }

    public static void GetHeroPlaying(Packet _packet)
    {
        bool canEvolve = _packet.ReadBool();
        

        string name = _packet.ReadString();
        int agility = _packet.ReadInt();
        int intelligence = _packet.ReadInt();
        int hp = _packet.ReadInt();
        int mana = _packet.ReadInt();
        int posLine = _packet.ReadInt();
        int posColumn = _packet.ReadInt();
        GameManager.typeHero type = (GameManager.typeHero)_packet.ReadInt();
        int id = _packet.ReadInt();
        Vector3 scaleImage = _packet.ReadVector3();
        bool canControl = _packet.ReadBool();
        Hero hero = new Hero();
        hero.heroName = name;
        hero.id = id;
        hero.type = type;
        hero.x = posColumn;
        hero.y = posLine;
        hero.canControl = canControl;

        GameManager.instance.UpdateHeroPlaying(hero,canEvolve);

    }

    public static void UpdateStatHero(Packet _packet)
    {
        int damageDealed = _packet.ReadInt();
        bool heroDied = _packet.ReadBool();

        

        string name = _packet.ReadString();
        int agility = _packet.ReadInt();
        int intelligence = _packet.ReadInt();
        int hp = _packet.ReadInt();
        int mana = _packet.ReadInt();
        int posLine = _packet.ReadInt();
        int posColumn = _packet.ReadInt();
        GameManager.typeHero type = (GameManager.typeHero)_packet.ReadInt();
        int id = _packet.ReadInt();
        Vector3 scaleImage = _packet.ReadVector3();
        bool canControl = _packet.ReadBool();
        Hero hero = new Hero();
        hero.id = id;
        hero.mana = mana.ToString();
        hero.health = hp.ToString();
        hero.type = type;
        hero.x = posColumn;
        hero.y = posLine;
        hero.canControl = canControl;
        GameManager.instance.UpdateStatHero(hero,heroDied,damageDealed);
    }


    public static void GetMovement(Packet _packet)
    {
        int col = 0;
        int row = 0;
        Debug.Log("recumove");
        bool isMovement = _packet.ReadBool();
        while (true)
        {
            col = _packet.ReadInt();
            row = _packet.ReadInt();
            Debug.Log("col : " + col);
            Debug.Log("row : " + row);
            if(col == -1)
            {
                break;
            }
            GameManager.instance.ShowCase(row, col,isMovement);
        }

       
    }

    public static void GetNewPosHero(Packet _packet)
    {
        int col = 0;
        int row = 0;
        Debug.Log("recumove");
        while (true)
        {
            col = _packet.ReadInt();
            row = _packet.ReadInt();
            Debug.Log("col : " + col);
            Debug.Log("row : " + row);
            if (col == -1)
            {
                GameManager.instance.StartAnimationMove();
                break;
            }
            GameManager.instance.AddCaseForMove(row, col);
        }
       
    }

    public static void GetTargetAttack(Packet _packet)
    {
        int col = 0;
        int row = 0;
        int type =0;
        GameManager.instance.mapManager.ClearAllCase();
        while (true)
        {
            type = _packet.ReadInt();
            row = _packet.ReadInt();
            col = _packet.ReadInt();
            Debug.Log("col : " + col);
            Debug.Log("row : " + row);
            if (col == -1)
            {
                break;
            }
            GameManager.instance.mapManager.ShowRangeSpellAttack(col, row,type);
        }
    }

    public static void SendNextSentence(Packet _packet)
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    public static void showAttacksAvailable(Packet _packet)
    {
        string name1 = _packet.ReadString();
        string name2 = _packet.ReadString();
        string name3 = _packet.ReadString();
        //FindObjectOfType<DialogueManager>().DisplayNextSentence();
        GameObject.FindGameObjectWithTag("ButtonAttackManagerTag").GetComponent<AttackButtonManager>().showButtonSpells(name1,name2,name3);
    }


}
