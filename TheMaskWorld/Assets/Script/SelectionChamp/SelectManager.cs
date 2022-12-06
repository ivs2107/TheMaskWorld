using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;
    private static GameObject[] tabBtn;
    public Color colorSelected;

    private void Start()
	{
        tabBtn = GameObject.FindGameObjectsWithTag("SelectHeroButtonTag");
    }

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

    public void ChangeBtnHeroSelection()
	{
        for (int i = 0; i < tabBtn.Length; i++)
        {
            tabBtn[i].GetComponent<SelectChamp>().gameObjectBorder.SetActive(false);
            tabBtn[i].GetComponent<SelectChamp>().isSelected = false;
        }
    } 

    public void ValidateChoice()
	{
        string heroName = "";
        for (int i = 0; i < tabBtn.Length; i++)
        {
			if (tabBtn[i].GetComponent<SelectChamp>().isSelected)
			{
                tabBtn[i].gameObject.GetComponent<Image>().color = colorSelected;
                Debug.Log("Nom de l'image : " + tabBtn[i].GetComponent<SelectChamp>().nameHero);
                heroName = tabBtn[i].GetComponent<SelectChamp>().nameHero;
            }
        }
        if (heroName != "")
        {
            GameManager.players[Client.instance.myId].Character = heroName;
            GameObject.Find("Validate_btn").GetComponent<Button>().interactable = false;
            SendHeroSelected(heroName);
        }
    }

    public void SendHeroSelected(string nameHeroSelected)
	{
        ClientSend.HeroSelected(nameHeroSelected);
    }

    public void HeroSelected(string heroName)
	{
        

        for (int i = 0; i < tabBtn.Length; i++)
        {
            if (tabBtn[i].GetComponent<SelectChamp>().nameHero == heroName)
            {
                tabBtn[i].gameObject.GetComponent<Image>().color = colorSelected;
                tabBtn[i].GetComponent<SelectChamp>().isSelectedByOthers = true;

                tabBtn[i].GetComponent<SelectChamp>().gameObjectBorder.SetActive(false);
                tabBtn[i].GetComponent<SelectChamp>().nameHero = "";

                Debug.Log("meme nom");
            }
        }

    }
}
