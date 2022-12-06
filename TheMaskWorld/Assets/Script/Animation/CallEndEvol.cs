using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEndEvol : MonoBehaviour
{
    // function to use in animation to call hero evolution
    public void CallEndEvolution()
    {
        GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>().EndEvolutionHero() ;
    }
}
