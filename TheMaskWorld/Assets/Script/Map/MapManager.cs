using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<Hero,GameObject> dictHero = new Dictionary<Hero, GameObject>();

    public List<Hero> listHero = new List<Hero>();

    public Hero HeroPlaying=null;

    public static List<GameObject> listCase = new List<GameObject>();

    public static GameObject[,] tabCase;

    public static int[,] tabCaseAStar;
    public List<Vector2> listCaseForMove = new List<Vector2>();
    public GameObject HeroPrefab;
    public GameObject MainCamera;
    public GameObject UIFight;
    public GameObject beginTurnUI;
    public GameObject CasePrefab;
    public GameObject CanvasParent;

    public Color colorCaseAttacked;
    public Color colorAttack;
    public Color colorMovement;


    static int col = 18;//11;
    static int row = 14;//8;

    public MapManager instance;


    public bool isRobotTheMaskEvolve = false;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
       
        LoadCase();

    }


    public bool canShowOnLevel2 = true;
    //creation hero
    public void CreateHero(Hero hero)
    {

        


        GameObject GameObjectHero = Instantiate(HeroPrefab, new Vector3(hero.x + 0.5f, -(hero.y + 0.5f), 0), Quaternion.identity/*no rotation*/);
        hero.artwork = Resources.Load<Sprite>("Images/Character/" + hero.heroName);
        HeroDisplay hd = GameObjectHero.GetComponent<HeroDisplay>();
        hd.hero = hero;
        hd.textHeroName = hero.heroName;
        hd.spriteRenderer.transform.localScale = hero.scaleImage;
        dictHero.Add(hero, GameObjectHero);
        hd.InitDisplay();
        hd.displayHero();

       // if ((Client.instance.myId % 2 == 0 && hero.id % 2 == 0 || Client.instance.myId % 2 != 0 && hero.id % 2 != 0) && SceneManager.GetActiveScene().name == "V2Level2")
       if(!canShowOnLevel2 && SceneManager.GetActiveScene().name == "V2Level2" && GameManager.players[Client.instance.myId].Character != "TheMask")
        {

            GameObjectHero.SetActive(false);
        }
        else
        {
            MainCamera.transform.position = new Vector3(
           GameObjectHero.transform.position.x,
           GameObjectHero.transform.position.y,
           MainCamera.transform.position.z);
        }

        canShowOnLevel2 = !canShowOnLevel2;

    }

    int numberSpawn = 0;

    //spawn hero animation
    public IEnumerator SpawnHeroWithLoading()
    {
        ActionButtonsSetActive(false);
        for (; numberSpawn < listHero.Count; numberSpawn++)
        {
            CreateHero(listHero[numberSpawn]);
            yield return new WaitForSeconds(0.7f);
        }
        //UIFight.SetActive(true);
        /*if(GameManager.players[Client.instance.myId] == null)
        {
        }
        else if (GameManager.players[Client.instance.myId].Character == HeroPlaying.heroName  /*(Client.instance.myId ==1 && HeroPlaying.heroName == "Merchant")(GameManager.players[Client.instance.myId].Character == "TheMask" && HeroPlaying.type == GameManager.typeHero.cEnnemyHero) && SceneManager.GetActiveScene().name =="V2Level1"*/
      /*  {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = true;
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(HeroPlaying.x + 0.5f, -(HeroPlaying.y + 0.5f), -10);
            ActionButtonsSetActive(true);
            
        }*/
    }

    //load map
    void LoadCase()
    {
        tabCase = new GameObject[col, row];
        tabCaseAStar = new int[col,row];
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                GameObject GameObjectCase= Instantiate(CasePrefab, new Vector3(x + 0.5f, -(y + 0.5f), 0), Quaternion.identity/*no rotation*/,CanvasParent.transform);
                Case c = GameObjectCase.GetComponent<Case>();
                c.x = x;
                c.y = y;
                c.numberAStar = int.MaxValue;
                listCase.Add(GameObjectCase);
                tabCase[x, y] = GameObjectCase;
                tabCaseAStar[x, y] = int.MaxValue;
              //  GameObjectCase.SetActive(true);
            }
        }
    }




    // clear on client case
    public void ClearAllCase()
    {
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                tabCase[i, j].transform.Find("SelectImage").gameObject.SetActive(false);
                tabCase[i, j].SetActive(false);
            }
        }
    }
    
    // show a specific case
    public void ShowCase(int posLine, int posColumn, bool isMovement)
    {

        Color color = isMovement ? colorMovement : colorAttack;
        tabCase[posLine, posColumn].transform.Find("Image").gameObject.GetComponent<Image>().color = color;
        tabCase[posLine, posColumn].GetComponent<Case>().colorCaseNormal = color;
        tabCase[posLine, posColumn].GetComponent<Case>().showAttack = !isMovement;
        tabCase[posLine, posColumn].SetActive(true);
    }

    // move animation
    public IEnumerator MoveThroughCase()
    {
        while (dictHero[listHero[HeroPlaying.id]].GetComponent<HeroDisplay>().IsInEvolutionState)
        {
            yield return null;
        }

        for (int i = 0; i < listCaseForMove.Count; i++)
        {
            int x = (int)listCaseForMove[i].y;
            int y = (int)listCaseForMove[i].x;
            if (-(y+0.5f) == dictHero[listHero[HeroPlaying.id]].transform.position.y)
            {
                yield return dictHero[listHero[HeroPlaying.id]].transform.DOMoveX(x + 0.5f, 0.3f).WaitForCompletion();
            }
            else
            {
                yield return dictHero[listHero[HeroPlaying.id]].transform.DOMoveY(-(y + 0.5f), 0.3f).WaitForCompletion();
            }
            
        }

        //erreur??
        if (HeroPlaying.type == GameManager.typeHero.cEnnemyHero && GameManager.players[Client.instance.myId].Character == "TheMask" /*Client.instance.myId==1*/  && HeroPlaying.canControl == false )
        {
           ClientSend.RequestEndTurn();
        }
        listCaseForMove.Clear();
    }


    // shwo range spell on client
    public void ShowRangeSpellAttack(int posLine, int posColumn,int type)
    {
        ShowCase(posLine, posColumn, false);
        if (type == (int)GameManager.typeHero.cTargetAttack) {
            tabCase[posLine, posColumn].transform.Find("Image").gameObject.GetComponent<Image>().color = colorCaseAttacked;
        }
    }
    //set new stats
    public void SetStatHero(Hero hero, int damageDealed)
    {
        Hero h = listHero[hero.id];
        h.mana = hero.mana;
        h.health = hero.health;
        GameObject g = dictHero[listHero[hero.id]];
        if(g.active == false)
        {
            return;
        }
        g.GetComponent<HeroDisplay>().displayHero();
        if (damageDealed != 0)
        {
            g.GetComponent<HeroDisplay>().EffectDamageTaken();
            g.GetComponent<HeroDisplay>().AnimationDamageTaken(damageDealed);
        }
    }
    // destroy on client
    public IEnumerator DestroyHero(Hero  hero)
    {
        foreach (SpriteRenderer r in dictHero[listHero[hero.id]].GetComponentsInChildren<SpriteRenderer>())
            r.enabled = false;
        dictHero[listHero[hero.id]].GetComponent<HeroDisplay>().EffectDeath();
        yield return new WaitForSeconds(1f);
        Destroy(dictHero[listHero[hero.id]]);
       /* dictHero.Remove(listHero[hero.id]);
        Hero destroyHero = listHero[hero.id];
        listHero.Remove(destroyHero);*/
        //numberSpawn--;
    }
    //set new hero
    public void UpdateHeroPlaying(Hero hero)
    {
        
        HeroPlaying = hero;

    }
    // need id of hero later
    int idHeroEvol;
    public void EvolutionHero(Hero hero)
    {
        
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(hero.x + 0.5f, -(hero.y + 0.5f), -10);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = false;
        Hero h = listHero[hero.id];
        idHeroEvol = hero.id;
        GameObject g = dictHero[h];
        g.GetComponent<HeroDisplay>().IsInEvolutionState  = true;
        g.GetComponent<Animator>().SetInteger("Statement", 1);
        GameObject.FindGameObjectWithTag("EvolTag").GetComponent<Animator>().enabled = true;
        

    }
    //Adjustement of animation
    public void EndEvolutionHero()
    {
        Hero h = listHero[idHeroEvol];
        GameObject g = dictHero[h];
        g.GetComponent<Animator>().Play("HeroEndEvolution");
        g.GetComponent<HeroDisplay>().spriteRenderer.sprite = Resources.Load<Sprite>("Images/Character/" + h.heroName+"Evol");
        if(h.heroName == "RobotTheMask")
        {
            isRobotTheMaskEvolve = true;
            foreach(KeyValuePair<Hero, GameObject> element in this.dictHero)
            {
               // element.Key.heroName = h.heroName;
                element.Value.GetComponent<HeroDisplay>().spriteRenderer.sprite = Resources.Load<Sprite>("Images/Character/" + h.heroName + "Evol");
                element.Value.GetComponent<HeroDisplay>().spriteRenderer.transform.localScale = new Vector3(0.141183555f, 0.142585784f, 0.106607974f);
                element.Value.GetComponent<HeroDisplay>().displayHero();
            }
        }
        if (h.heroName != "RobotTheMask")
        {
            g.GetComponent<HeroDisplay>().spriteRenderer.transform.localScale = new Vector3(0.863451838f, 0.872027695f, 0.651994169f);
        }
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = true;
    }

    //buttons action
    public void ActionMouvement()
    {
        ClientSend.RequestMovement();
        GameObject.Find("ButtonMouvement").GetComponent<Button>().interactable = false ;
    }

    public void ActionAttack(int idSpell)
    {
        ClientSend.RequestAttack(idSpell);
        ActionButtonsSetActive(false);
        beginTurnUI.SetActive(false);
    }

    public void ActionShowAttacksAvalaible()
    {
        ClientSend.ShowAttacksAvailable();
        ActionButtonsSetActive(false);
        beginTurnUI.SetActive(false);
    }

    public void ActionEndTurn()
    {
        ClientSend.RequestEndTurn();
        ActionButtonsSetActive(false);
        beginTurnUI.SetActive(false);
    }

    //action of button set active or not
    public void ActionButtonsSetActive(bool active)
    {

        if(!active)
        {
            GameObject.FindGameObjectWithTag("ButtonAttackManagerTag").GetComponent<AttackButtonManager>().unshowButtonSpells();
        }


        GameObject[] tabBtn = GameObject.FindGameObjectsWithTag("LevelActionButtonTag");

        for (int i = 0; i < tabBtn.Length; i++)
        {
            tabBtn[i].GetComponent<Button>().interactable = active;
        }
    }
}
