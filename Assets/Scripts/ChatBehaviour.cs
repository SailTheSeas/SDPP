using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private Text chatText = null;
    [SerializeField] private InputField inputField = null;
    [SerializeField] private GameObject canvas = null;

    private static event Action<string> OnMessage;

    // Called when the a client is connected to the server
    public override void OnStartAuthority()
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
    }

    // When a new message is added, update the Scroll View's Text to include the new message
    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    // When a client hits the enter button, send the message in the InputField
    [Client]
    public void Send()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { 
            return; 
        }
        if (string.IsNullOrWhiteSpace(inputField.text)) {
            return; 
        }
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty;
        Debug.Log("Message Sent");
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