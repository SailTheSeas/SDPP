using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTag : MonoBehaviour
{
    public string player = "Player1";
    // Start is called before the first frame update
    GameController controller;
    GameObject dealer;
    private void Awake()
    {
        dealer = GameObject.FindGameObjectWithTag("Dealer");
        dealer.SetActive(true);
        SetPlayerTag();
        SetPlayer();
        Debug.Log(dealer);

    }
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
        /*SetPlayer();*/
    }

    void SetPlayer()
    {
        Debug.Log(this.gameObject);
        controller.SetPlayer(this.gameObject.GetComponent<PlayerActions>());
        if(!this.gameObject.CompareTag("Player1"))
        {
            dealer.SetActive(false);
        }
    }
}
