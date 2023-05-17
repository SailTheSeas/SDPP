using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private Text chatText = null;
    [SerializeField] private Text spawnMessage = null;
    [SerializeField] private InputField inputField = null;
    [SerializeField] private GameObject canvas = null;
    [SerializeField] private GameObject chatBox = null;
    [SerializeField] private GameObject message=null;
    [SerializeField] private Button sendButton =null;

    //runs similarly to update - checks if the chat message is too long and deactivates the send button when it is
    private void FixedUpdate()
    {
        if (inputField.text.Length > 100)
        {
            sendButton.interactable = false;
        }else
        {
            sendButton.interactable = true;
        }
    }

    // When a client hits the send button, send the message in the InputField
    [Client] public void Send()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) {
            return; 
        }
        string msg = inputField.text;
        Debug.Log(msg.Length);
        Debug.Log("msg got");
        CmdSpawnText(msg);
        inputField.text = string.Empty;   
    }
    
    //Sends the message to the server
    [Command (requiresAuthority = false)] private void CmdSpawnText(string msg)
    {
        RpcChatUpdate(msg);
    }

    //Send the message from the client to the clients
    [ClientRpc] void RpcChatUpdate(string msg)
    {
        GameObject sent = Instantiate(message);
        sent.GetComponent<Text>().text = "Player name: " + msg;
        sent.transform.parent = chatBox.transform;
        Debug.Log(chatBox.transform.childCount);
    }
}