using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerTimeout : NetworkBehaviour
{
    public int timeAmount;
    private IEnumerator coroutine;
    NetworkConnectionToClient conn;
    // Start is called before the first frame update
    [Client]void Awake()
    {
        Debug.Log(conn);
       // OnButtonPress();
        if (isLocalPlayer)
        {
            Debug.Log("this isn't my item");

            return;
        }
        coroutine = counter(timeAmount);
        StartCoroutine(coroutine);
    }
    public void PlayerTurn()
    {
        StartCoroutine(coroutine);
    }
    // Update is called once per frame
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
    }

    [Client]IEnumerator counter(int duration)
    {
        for(int i=timeAmount; i>=0;i--)
        {
            if(i<=(timeAmount/2))
            {
                Debug.Log(i);
            }
            if(i==0)
            {
                cmdPlayerTimeout();
            }
            yield return new WaitForSeconds(1);
        }
    }
    [Command (requiresAuthority =false)] void cmdPlayerTimeout()
    {
        Debug.Log("time up");
    }
    
   /* public void OnButtonPress()
    {
        Debug.Log("OnButtonPress()");
        
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
    }*/
}
