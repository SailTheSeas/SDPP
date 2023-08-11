using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//KE - Integration script
//KE - Sets the tag of the player. This is used to determine the order of turns and roles of the clients when they first connect
public class SetTag : MonoBehaviour
{
    public string player = "Player1";
    // Start is called before the first frame update
    GameController controller;

    //KE - Called when the client connects, (called on the client)
    private void Awake()
    {
        SetPlayerTag();
        SetPlayer();
    }
    //KE - Checks which tags are taken already - since this is a 4 player game, we only need to worry about there being 4 tags
    void SetPlayerTag()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (GameObject.FindGameObjectsWithTag("Player1").Length==1)
        {
            player = "Player2";
        }
        if (GameObject.FindGameObjectsWithTag("Player2").Length== 1)
        {
            player = "Player3";
        }
        if (GameObject.FindGameObjectsWithTag("Player3").Length == 1)
        {
            player = "Player4";
        }
        if (GameObject.FindGameObjectsWithTag("Player4").Length == 1)
        {
            player = "Player1";
        }
         this.gameObject.tag = player;
    }

    //KE - communicates the tag and gameobject component info to the GameController script which handles turns and game logic
    void SetPlayer()
    {
        Debug.Log((this.gameObject.transform.GetChild(2).GetComponent<PlayerActions>()));
        Debug.Log(this.gameObject.GetComponent<DealerSelect>());
        controller.SetPlayer(this.gameObject.transform.GetChild(2).GetComponent<PlayerActions>(), this.gameObject.GetComponent<DealerSelect>());
    }
}
