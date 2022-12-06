using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonManager : MonoBehaviour
{
    public GameObject ButtonSpell1;
    public GameObject ButtonSpell2;
    public GameObject ButtonSpell3;


    public void showButtonSpells(string Spell1, string Spell2, string Spell3)
    {
        ButtonSpell1.transform.Find("Text").GetComponent<Text>().text = Spell1;
        ButtonSpell2.transform.Find("Text").GetComponent<Text>().text = Spell2;
        ButtonSpell3.transform.Find("Text").GetComponent<Text>().text = Spell3;

        ButtonSpell1.SetActive(true);
        ButtonSpell2.SetActive(true);
        ButtonSpell3.SetActive(true);
    }


    public void unshowButtonSpells()
    {
        ButtonSpell1.SetActive(false);
        ButtonSpell2.SetActive(false);
        ButtonSpell3.SetActive(false);
    }
}
