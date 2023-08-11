using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


//KE - My mock script for testing if player actions could be dsplayed inthe chat
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
        spawn = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PlayerName>().playerName;
    }
    public void Raise()
    {
        spawn = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>().text;
        chat.UpdateMessageChat(spawn, " has chosen to raise");
    }
    public void Fold()
    {
        spawn = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>().text;
        chat.UpdateMessageChat(spawn, " has chosen to fold");
    }
    public void AllIn()
    {
        spawn = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>().text;
        chat.UpdateMessageChat(spawn, " has gone all in");
    }
    public void Call()
    {
        spawn = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>().text;
        chat.UpdateMessageChat(spawn, " has chosen to call");
    }
}
