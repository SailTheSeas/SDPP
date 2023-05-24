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
    [SerializeField] private Text nameCanvas;

    //KE - runs similarly to "update"
    //Checks if the chat message is too long and deactivates the send button locally when it is
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
    private void Start()
    {
        nameCanvas = GameObject.FindGameObjectWithTag("Login").GetComponentInChildren<Text>();
    }

    //KE - When a client clicks the send button, send the message in the inputfield to the server and clear the input field
    [Client] public void Send()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) {
            return; 
        }

        string msg = inputField.text;
        inputField.text = string.Empty;

        CmdSpawnText(msg, nameCanvas.text);
    }
    
    //KE - message is sent to the server. The server validates the message and communicates it to each client. 
    [Command (requiresAuthority = false)] private void CmdSpawnText(string msg, string playerName)
    {
        RpcChatUpdate(msg, playerName);
    }

    //KE - Send the message from the server to each clients.
    //Checks the number of messages sent. If the numner is too high, then the oldest message gets deleted
    [ClientRpc] void RpcChatUpdate(string msg, string playerName)
    {
        GameObject sent = Instantiate(message);
        sent.GetComponent<Text>().text = playerName + ": " + msg;
        sent.transform.parent = chatBox.transform;
        Debug.Log(chatBox.transform.childCount);

        int numMessages = chatBox.transform.childCount;
        if(numMessages>5)
        {
            Debug.Log("this are to many mesage");
            Deletemessage();
        }
    }

    //Deletes the oldest message
    void Deletemessage()
    {
        Destroy(chatBox.GetComponent<Transform>().GetChild(0).gameObject);
    } 
}