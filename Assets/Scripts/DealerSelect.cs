using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DealerSelect : NetworkBehaviour
{
    GameObject dealer;
    GameObject playerView;
    GameObject playerActions;
    [SerializeField]
    PlayerActions thisPlayer;
    // Start is called before the first frame update
    public override void OnStartAuthority()
    {
        this.gameObject.SetActive(true);
        playerView = this.transform.GetChild(1).gameObject;
        playerView.SetActive(true);
        if (this.gameObject.CompareTag("Player1"))
        {
            dealer = this.transform.GetChild(1).GetChild(1).gameObject;
            dealer.SetActive(true);
        }
        for (int i = 1; i <= 4; i++)
        {
            if (this.gameObject.CompareTag("Player" + i.ToString()))
            {
                playerActions = this.transform.GetChild(2).gameObject;
                thisPlayer = playerActions.GetComponent<PlayerActions>();
                playerActions.SetActive(true);
                EnableOnServer();
            }
        }
        
    }

    [Command(requiresAuthority = false)] public void EnableOnServer()
    {
        playerActions = this.transform.GetChild(2).gameObject;
        playerActions.SetActive(true);
    }

    [ClientRpc]
    public void CreatePlayer(GameController newGC, int minBet, int money, int playerNum)
    {
        //thisPlayer = playerActions.GetComponent<PlayerActions>();
        //if (this.gameObject.CompareTag("Player" + (playerNum + 1).ToString()))
        thisPlayer.createPlayer(newGC, minBet, money, playerNum);
    }
}
