using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ConnectionHUD : MonoBehaviour
{
    [SerializeField] GameObject ClientButton;
    [SerializeField] GameObject ServerButton;
    NetworkManager manager;
    NetworkManagerHUD hud;
    [SerializeField] Text update;
    private void Start()
    {
        update.text = "";
        manager = GetComponent<NetworkManager>();
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ClientButton.SetActive(false);
            Debug.Log("I know you're playing this in the windows version of Unity");
        }
        if(Application.platform==RuntimePlatform.WebGLPlayer)
        {
            ServerButton.SetActive(false);
        }
    }
    public void StartServer()
    {

    }
    public void ConnectClient()
    {
        if(!NetworkServer.active)
        {
            update.text = "Please start the server first";
            Debug.Log("sTART THE SERVER FIRST");
        }
        else
        {
            update.text = "Connecting to the server";
            Debug.Log("the server is upand runing");
        }
    }
}
