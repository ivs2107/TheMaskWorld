using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Hero", menuName ="Hero")]
public class Hero : ScriptableObject
{
    //hero attributes
    public int id;
    public GameManager.typeHero type;
    public string heroName;
    public string description;

    public Sprite artwork;

    public string mana;
    public string health;

    public int x;
    public int y;
    public Vector3 scaleImage;

    public bool canControl;

    public int idClient;
}
