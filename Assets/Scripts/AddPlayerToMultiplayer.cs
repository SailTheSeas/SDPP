using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class AddPlayerToMultiplayer : NetworkBehaviour
{
    [SyncVar]public string playerName;
    [SerializeField] GameObject playerNameText;
    Transform playerInChatDisplay;
    // Start is called before the first frame update

    public override void OnStartClient()
    {
        playerName = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PlayerName>().playerName;
        base.OnStartClient();
        playerInChatDisplay = GameObject.FindGameObjectWithTag("Players").GetComponent<Transform>();

        UpdateClientUI(playerName);
        //CMDAddPlayerName(playerName);
        this.gameObject.tag = "Client";
        GetExistingPlayers();
    }
    void GetExistingPlayers()
    {
        bool uptoPlayer = false;
        string newPlayerName;
        //Debug.Log(GameObject.FindGameObjectsWithTag("Client").Length);
        string playerName;
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Client"))
        {
            newPlayerName = player.GetComponent<AddPlayerToMultiplayer>().playerName;
            //Debug.Log(player.GetComponent<AddPlayerToMultiplayer>().playerName);
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
    /*[Command (requiresAuthority =false)] void CMDAddPlayerName(string name)
    {
        RpcUpdatePlayerUI(name);
        //updateServerPlayers(name);
        GameObject newPlayer = Instantiate(playerNameText, playerInChatDisplay);
        newPlayer.GetComponent<Text>().color = Color.green;
        newPlayer.GetComponent<Text>().text = name;
    }*/
    [ClientRpc] void RpcUpdatePlayerUI(string name)
    {
        UpdateClientUI(name);
    }
   /* [Server] void updateServerPlayers(string name)
    {
        Debug.Log("this is working");

    }*/
    void UpdateClientUI(string name)
    {
        GameObject newPlayer = Instantiate(playerNameText, playerInChatDisplay);
        newPlayer.GetComponent<Text>().color = Color.green;
        newPlayer.GetComponent<Text>().text = name;
    }
}
