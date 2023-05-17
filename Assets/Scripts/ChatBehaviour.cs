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

    private static event Action<string> OnMessage;

    // Called when the a client is connected to the server
    /*public override void OnStartAuthority()
    {
        canvas.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    // Called when a client has exited the server
    [ClientCallback] private void OnDestroy()
    {
        if (!isOwned) { 
            return; 
        }

        OnMessage -= HandleNewMessage;
    }*/
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

    // When a client hits the enter button, send the message in the InputField
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

    [ClientRpc] void RpcChatUpdate(string msg)
    {
        GameObject sent = Instantiate(message);
        sent.GetComponent<Text>().text = "Player name: " + msg;
        sent.transform.parent = chatBox.transform;
        Debug.Log(chatBox.transform.childCount);
    }

[Command]
    private void CmdSendMessage(string message)
    {
        // Validate message
        RpcHandleMessage(connectionToClient.connectionId+":"+ message);
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }

}