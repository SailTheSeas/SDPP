using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTag : MonoBehaviour
{
    public string player = "Player1";
    // Start is called before the first frame update
    GameController controller;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        SetPlayerTag();
        SetPlayer();
    }
    void SetPlayerTag()
    {
        if (GameObject.FindGameObjectsWithTag("Player1").Length!=1)
        {
            player = "Player2";
        }
        if (GameObject.FindGameObjectsWithTag("Player2").Length != 1)
        {
            player = "Player3";
        }
        if (GameObject.FindGameObjectsWithTag("Player3").Length != 1)
        {
            player = "Player4";
        }
        if (GameObject.FindGameObjectsWithTag("Player4").Length != 1)
        {
            player = "Player1";
        }
         this.gameObject.tag = player;
    }

    void SetPlayer()
    {
        controller.SetPlayer(this.gameObject.GetComponent<PlayerActions>());
    }
}
