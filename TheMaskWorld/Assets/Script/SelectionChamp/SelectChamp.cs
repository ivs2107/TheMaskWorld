using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class SelectChamp : MonoBehaviour
{
    public GameObject gameObjectBorder;
    public Color colorSelected;
    
    public bool isSelected = false;
    public bool isSelectedByOthers = false;
    public string nameHero;

    // Start is called before the first frame update
    void Start()
    {
        gameObjectBorder.SetActive(false);
        nameHero = this.GetComponent<Button>().image.sprite.name;
    }

    public void Hover()
	{

        if (GameObject.Find("Validate_btn").GetComponent<Button>().interactable && !isSelectedByOthers)
        {
            gameObjectBorder.SetActive(true);
        }
    }

    public void Exit()
	{
		if (!isSelected)
		{
            gameObjectBorder.SetActive(false);
        }
    }
    
    public void Select()
	{
        if (GameObject.Find("Validate_btn").GetComponent<Button>().interactable && !isSelectedByOthers)
        {
            
            SelectManager.instance.ChangeBtnHeroSelection();
            gameObjectBorder.SetActive(true);

            isSelected = true;
            Debug.Log("btn selected");
        }
    }
}
