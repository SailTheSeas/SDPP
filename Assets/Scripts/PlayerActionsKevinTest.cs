using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerActionsKevinTest : MonoBehaviour

{
    ChatBehaviour chat;
    string spawn;
    private void Start()
    {
        
        chat = GameObject.FindGameObjectWithTag("Chat").GetComponent<ChatBehaviour>();
    }
    private void OnConnectedToServer()
    {

        Debug.Log("this is a test"  + spawn);
    }
    public void Raise()
    {
        spawn = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>().text;
        chat.UpdateMessageChat(spawn, " has chosen raise");
    }
    public void Fold()
    {
        spawn = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>().text;
        chat.UpdateMessageChat(spawn, " has chosen fold");
    }
}
