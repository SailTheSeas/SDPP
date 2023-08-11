using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


//KE - This script is used to increase the player count on the server as more clients join the game
//Ke - For the sake of the 
public class AddPlayerToMultiplayer : NetworkBehaviour
{
    [SyncVar]public string playerName;
    [SerializeField] GameObject playerNameText;
    Transform playerInChatDisplay;


    //KE - OnStartClient is called whenever a new client joins the game.
    //This function happens on the side of the client who has just joined
    public override void OnStartClient()
    {
        base.OnStartClient();

        playerInChatDisplay = GameObject.FindGameObjectWithTag("Players").GetComponent<Transform>();
        playerName = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PlayerName>().playerName;

        this.gameObject.tag = "Client";

        UpdateClientUI(playerName);
        GetExistingPlayers();
    }

    //KE - Runs through an array of all GameObjects in the scene with the tag "client".
    //KE - Whenever a new Client is added to the online scene, they are added to the back of the Array
    void GetExistingPlayers()
    {
        bool uptoPlayer = false;
        string newPlayerName;
        string playerName;

        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Client"))
        {
            newPlayerName = player.GetComponent<AddPlayerToMultiplayer>().playerName;
            if (this.playerName==newPlayerName)
            {
                Debug.Log("This is the player");
                uptoPlayer = true;
                break;
            }
            if(!uptoPlayer)
            { 
                UpdateClientUI(newPlayerName);
            }
        }
    }

    //KE - Depreciated, used to update the server side, but this became redundant
    /*[Command (requiresAuthority =false)] void CMDAddPlayerName(string name)
    {
        RpcUpdatePlayerUI(name);
        //updateServerPlayers(name);
        GameObject newPlayer = Instantiate(playerNameText, playerInChatDisplay);
        newPlayer.GetComponent<Text>().color = Color.green;
        newPlayer.GetComponent<Text>().text = name;
    }*/

    //KE - used to push ClientUI to all active clients
    [ClientRpc] void RpcUpdatePlayerUI(string name)
    {
        UpdateClientUI(name);
    }
   /* [Server] void updateServerPlayers(string name)
    {
        Debug.Log("this is working");

    }*/

    //KE - Updates each client's UI to display the names of each other client in the scene
    void UpdateClientUI(string name)
    {
        GameObject newPlayer = Instantiate(playerNameText, playerInChatDisplay);
        newPlayer.GetComponent<Text>().color = Color.green;
        newPlayer.GetComponent<Text>().text = name;
    }
}
