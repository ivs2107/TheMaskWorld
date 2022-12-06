using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Case : MonoBehaviour
{
    public GameObject SelectImage;

    public int x;
    public int y;
    public int numberAStar;
    public Color colorCaseNormal;
    public bool showAttack = false;
    public bool hasRequestTargetAttack = false;

    private MapManager mapManager;

    private void Awake()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
    }
    void OnMouseOver()
    {
        //show image on enter
        SelectImage.SetActive(true);
        //call function map
        if (showAttack && !hasRequestTargetAttack && SelectImage.activeSelf)
        {
            ClientSend.RequestTargetAttack(x, y);
            hasRequestTargetAttack = true;
        }
    }

    void OnMouseExit()
    {
        //show off image on exit
        SelectImage.SetActive(false);
        if (showAttack && hasRequestTargetAttack)
        {
            hasRequestTargetAttack = false;
        }
    }

    void OnMouseDown()
    {
        Debug.Log( x+";"+ y);
        //look if he is attackking or mouvement
        if (!showAttack)
        {
            ClientSend.MoveHero(x, y);
        }
        else
        {
            ClientSend.AttackZone(x,y);
        }
        mapManager.ClearAllCase();
    }
}
