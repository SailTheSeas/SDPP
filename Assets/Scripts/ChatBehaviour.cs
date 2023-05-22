using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private InputField inputField = null;
    [SerializeField] private GameObject chatBox = null;
    [SerializeField] private GameObject message=null;
    [SerializeField] private Button sendButton =null;

    //KE - runs similarly to update - checks if the chat message is too long and deactivates the send button when it is
    private void FixedUpdate()
    {
        if (inputField.text.Length > 50)
        {
            sendButton.interactable = false;
        }else
        {
            sendButton.interactable = true;
        }
    }

    //KE - When a client hits the send button, send the message in the InputField
    [Client] public void Send()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) {
            return; 
        }

        string msg = inputField.text;
        inputField.text = string.Empty;

        CmdSpawnText(msg);
    }
    
    //KE - Sends the message to the server
    [Command (requiresAuthority = false)] private void CmdSpawnText(string msg)
    {
        RpcChatUpdate(msg);
    }

    //KE - Send the message from the client to the clients
    [ClientRpc] void RpcChatUpdate(string msg)
    {
        GameObject sent = Instantiate(message);
        sent.GetComponent<Text>().text = "Player name: " + msg;
        sent.transform.parent = chatBox.transform;
        Debug.Log(chatBox.transform.childCount);

        int numMessages = chatBox.transform.childCount;
        if(numMessages>5)
        {
            Debug.Log("this are to many mesage");
            Deletemessage();
        }
    }
    void Deletemessage()
    {
        Destroy(chatBox.GetComponent<Transform>().GetChild(0).gameObject);
    }

    
}