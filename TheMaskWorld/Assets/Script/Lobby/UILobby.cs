using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    public GameObject goText;
    public GameObject toggle;

    
    public GameObject textNumberOfPlayersConnected;

    // Start is called before the first frame update
    void Start()
    {
        ClientSend.NewPlayerEntered(Client.instance.myId);
        
    }

    public void setText(string result)
	{
        goText.GetComponent<Text>().text = result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleValueChanged()
	{
      
        toggle.GetComponent<Toggle>().interactable = false;
        
        //Call CliendSend
        ClientSend.PlayerConnected(true);
    }

    public void setTextNumberOfPlayers(string result)
	{
        textNumberOfPlayersConnected.GetComponent<Text>().text = result;
    }

    

}
