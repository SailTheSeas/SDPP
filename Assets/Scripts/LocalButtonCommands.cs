using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

//KE - Unfinished networking/gameplay integration script

//Unity Buttons cannot call from functions that already exist before the button has been instantiated in the scene.
//This script is attached to the prefab that the button is on,
//The functions on the GameController and PlayerActions scripts can be called with the button during runtime
public class LocalButtonCommands : MonoBehaviour
{
    GameController game;
    PlayerActions player;
    Button startGame;

    //Fetches references to specific gameobjects that are present in the scene before the player can call them
    private void Start()
    {
        startGame=this.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Button>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = this.gameObject.GetComponent<PlayerActions>();
    }


    public void Raise()
    {
        player.raise();
    }
    public void Fold()
    {
        player.fold();
    }
    public void AllIn()
    {
        player.allIn();
    }
    public void Call()
    {
        player.call();
    }
    [Client]public void GameStart()
    {
        game.CmdGameStart(/*startGame*/);
    }
    public void HandOutCards()
    {
        game.handOutCards();
    }
}
