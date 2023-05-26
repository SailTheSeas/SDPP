using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


//Unity Buttons cannot call from functions that already exist before the button has been instantiated in the scene.
//This script is attached to the prefab that the button is on, so that the functions on the GameController and PlayerActions scripts can be called with the button during runtime
//We can find specific gameobjects by reference 
public class LocalButtonCommands : MonoBehaviour
{
    GameController game;
    PlayerActions player;
    Button startGame;

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
