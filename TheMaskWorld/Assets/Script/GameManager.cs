using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    private int nbPlayersConnected = 0;

    public MapManager mapManager;


    public enum typeHero
    {
        cEmpty = 0,
        cAllyHero = 1,
        cTarget = 2,
        cTargetAttack = 3,
        cEnnemyHero = 4
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Client.instance.myId == 1)
        {

            ClientSend.ChangeScene(2);
        }
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;

        _player = Instantiate(playerPrefab, _position, _rotation);
        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
  
        players.Add(_id, _player.GetComponent<PlayerManager>());

    }


    public void SetNewPlayerCharacters(int _id, string _username, string _character)
    {

        GameObject _player = null;
        _player = GameObject.FindGameObjectWithTag(_character);
        players[_id] = _player.GetComponent<PlayerManager>();




        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        _player.GetComponent<PlayerManager>().Character = _character;

    }

    public void ChangeScene(int SceneID)
    {
        int newID = SceneID + 1;
        Debug.Log("ChangeScene from : " + SceneID + " to " + newID);
        SceneManager.LoadScene(newID);
    }

    public void NewPlayerEntered(int ClientID)
	{
        if(SceneManager.GetActiveScene().name == "Lobby")
		{
            Debug.Log("NewPlayerEntered");
            if (GameManager.players.Count != 0)
            {
                string result = "";
                foreach (KeyValuePair<int, PlayerManager> entry in GameManager.players)
                {
                    result += entry.Value.username + "\n";
                }
                GameObject.FindGameObjectWithTag("LobbyTag").GetComponent<UILobby>().setText(result);
            }
            string res = "Number of players " + nbPlayersConnected + "/" + GameManager.players.Count;
            GameObject.FindGameObjectWithTag("LobbyTag").GetComponent<UILobby>().setTextNumberOfPlayers(res);
        }
	}

    public void CountNumberOfPlayers()
    {
        nbPlayersConnected++;
        
      

        string result = "Number of players " + nbPlayersConnected + "/" + GameManager.players.Count;

        GameObject.FindGameObjectWithTag("LobbyTag").GetComponent<UILobby>().setTextNumberOfPlayers(result);

        if(nbPlayersConnected == GameManager.players.Count  && Client.instance.myId==1)
		{
            ClientSend.ChangeScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayerSelectedHero(string heroName)
	{
        GameObject.Find("SelectManager").GetComponent<SelectManager>().HeroSelected(heroName);
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }

    }

    int valLvl2 =0;
    public void CreateHero(string nameHero, int hp, int mana, int x, int y, int id, Vector3 scaleImage, bool canControl, GameManager.typeHero type)
    {

            mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
            while(mapManager == null) { mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>(); }
            Hero hero = ScriptableObject.CreateInstance("Hero") as Hero;
            hero.heroName = nameHero;
            hero.health = hp.ToString();
            hero.mana = mana.ToString();
            hero.x = x;
            hero.y = y;
            hero.id = id;

            if (GameManager.players[Client.instance.myId].Character == nameHero)
            {
            mapManager.canShowOnLevel2 = (valLvl2 % 2==0);
            }
            valLvl2++;


            hero.scaleImage = scaleImage;
            hero.canControl = canControl;
            hero.type = type;


            mapManager.listHero.Add(hero);
    }

    public void SpawnHero()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        StartCoroutine(mapManager.SpawnHeroWithLoading());

    }
    public bool GameNotStarted = true;
    public void UpdateHeroPlaying(Hero hero,bool canEvolve)
    {
        /*if (GameNotStarted) {
           
           GameNotStarted = !GameNotStarted;
        }
        else*/
        {
            if (GameManager.players[Client.instance.myId].Character == hero.heroName  /*|| (Client.instance.myId == 1 && hero.heroName == "Merchant")*/ || (GameManager.players[Client.instance.myId].Character == "TheMask" && hero.type == typeHero.cEnnemyHero  && SceneManager.GetActiveScene().name =="V2Level1"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(hero.x + 0.5f, -(hero.y + 0.5f), -10);
                mapManager.beginTurnUI.SetActive(true);
                mapManager.ActionButtonsSetActive(true);
            }
            else
            {
                mapManager.beginTurnUI.SetActive(false);
            }
         
        }

        mapManager.UpdateHeroPlaying(hero);
        if (canEvolve)
        {
            mapManager.EvolutionHero(hero);
        }

    }

    public void UpdateStatHero(Hero hero,bool heroDied,int damageDealed)
    {
        if (!heroDied)
        {
            mapManager.SetStatHero(hero,damageDealed);
        }
        else
        {
            StartCoroutine(mapManager.DestroyHero(hero));
        }
        
    }
    public void ShowCase(int posLine,int posCol,bool isMovement)
    {
        mapManager.ShowCase(posLine, posCol, isMovement);
    }

    public void AddCaseForMove(int posLine, int posCol)
    {
        mapManager.listCaseForMove.Add(new Vector2(posCol, posLine));
    }

    public void StartAnimationMove()
    {

        if (mapManager.dictHero[mapManager.listHero[mapManager.HeroPlaying.id]].active == false)
        {
            return;
        }
        StartCoroutine(mapManager.MoveThroughCase());
       
    }

    public void LoadNewScene(int sceneId = -1)
    {
        StartCoroutine(WaitForLoading(sceneId));
    }
    public IEnumerator WaitForLoading(int sceneId =-1)
    {
        Animator anim = GameObject.FindGameObjectWithTag("CurtainTag").GetComponent<Animator>();
        anim.SetInteger("Statement", 1);
        yield return new WaitForSeconds(2f);
        int sceneID;
        sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneId == 23)
        {
            sceneID = sceneId-1;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID + 1);
        anim.SetInteger("Statement", 0);
        while (!operation.isDone)
        {
            yield return null;
        }
        if(SceneManager.GetActiveScene().name.Contains("V2Level"))
        {
            ClientSend.InitialiseLevel(int.Parse(SceneManager.GetActiveScene().name.Replace("V2Level", string.Empty)));
            GameManager.instance.GameNotStarted = true;
        }
        anim.SetInteger("Statement", 0);
    }

}
