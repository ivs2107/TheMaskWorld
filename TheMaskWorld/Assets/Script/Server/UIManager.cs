using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;
    public InputField ipField;

    public GameObject Curtain;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ConnectedToServer()
    {
        //  Client.instance.ip = ipField.text;
      //  Client.instance.setIp(ipField.text);
        Client.instance.ConnectToServer();
    }
    
    public void newValueIp()
    {
        Client.instance.setIp(ipField.text);

    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
