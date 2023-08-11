using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

//KE - This script was supposed to be implemented more in the integration phase. It is supposed to 
public class PlayerTimeout : NetworkBehaviour
{
    public int timeAmount;
    private IEnumerator coroutine;
    NetworkConnectionToClient conn;
    public Text timer;
    // Start is called before the first frame update
    [Client]void Awake()
    {

    }

    //KE- When the client joins the game, a timer will decrement, until it reaches zero, which is indicated to the player through a text component
    public override void OnStartAuthority()
    {
        // OnButtonPress();
        if (isLocalPlayer)
        {
            coroutine = counter(timeAmount);
            StartCoroutine(coroutine);
            return;
        }
    }

    public void PlayerTurn()
    {
        StartCoroutine(coroutine);
    }
    
    //KE - Checks if the player inputs the spacebar. If they do, stop the timer
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EndTurnTimer();
        }
    }

     
    public void EndTurnTimer()  //KE - I am hoping that the player will have to press a button to decide their action - this function will be added to the button to stop the timer for the player
    {
        StopCoroutine(coroutine);
        timer.text = " ";
    }

    [Client]IEnumerator counter(int duration)
    {
        for(int i=timeAmount; i>=0;i--)
        {
            if(i<=(timeAmount/2))
            {
                timer.text = i.ToString();
                //Debug.Log(i);
                if(i<=(timeAmount/4))
                {
                    timer.color = Color.red;
                }
            }
            if(i==0)
            {
                cmdPlayerTimeout();
            }
            yield return new WaitForSeconds(1);
        }
        timer.text = " ";
    }
    [Command (requiresAuthority =false)] void cmdPlayerTimeout()
    {
        Debug.Log("time up");
    }
    
    public void OnButtonPress()
    {        
            // Send a command to the server to display the message
            CmdDisplayMessage("This is your special message!", connectionToClient);
    }

    [Command]
    private void CmdDisplayMessage(string message, NetworkConnection targetConnection)
    {
        // Update the message on the server
        RpcDisplayMessage(message, targetConnection);
    }

    [ClientRpc]
    private void RpcDisplayMessage(string message, NetworkConnection targetConnection)
    {
        // Update the message on all clients
        if (connectionToClient==targetConnection)
        {
            // Display the message on the client who pressed the button
            Debug.Log(message);
        }
    }
}
