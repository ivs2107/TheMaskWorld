using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallNewTurn : MonoBehaviour
{
    // function in animation to begin turn
    public void CallAnimationNewTurn()
    {
        if (GameManager.players[Client.instance.myId].Character == GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>().HeroPlaying.heroName)
        {
            GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>().beginTurnUI.SetActive(true);
        }
    }
}
