using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

//KE - This script is used to overhaul the functionality needed from the NetworkManagerHUD.
//The NetworkManagerHUD script is used at the beginning of projects to assure that there is a quick and convenient way to set up host/server/clients
//The NetworkManagerHUD has been replaced because of its functionality became redundant (Joining as a host, and inputting an IP address)
//Some of the functionality we wanted was incomplete - closing servers, and disconnecting clients.
//It is worth noting that the showcase on Itch.io automatically kicks the clients when the server is stopped, and disconnects the clients when the tab is closed.

public class ConnectionHUD : MonoBehaviour
{
    [SerializeField] GameObject client;
    [SerializeField] GameObject server;
    NetworkManager manager;
    NetworkManagerHUD hud;
    /*[SyncVar]*/ bool serverStarted = false;
    public Button clientButton;
    //[SerializeField] Text update;
    private void Start()
    {
        clientButton.interactable = false;
        clientButton = client.GetComponent<Button>();
        //update.text = "";
        manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
        if (Application.platform == RuntimePlatform.WindowsPlayer|| Application.platform==RuntimePlatform.WindowsEditor)
        {
            client.SetActive(false);
            //ServerButton.SetActive(false);
            Debug.Log("I know you're playing this in the windows version of Unity");
        }
        if(Application.platform==RuntimePlatform.WebGLPlayer)
        {
            server.SetActive(false);
        }
        UpdateClientButton();
    }
    public void StartServer()
    {
        manager.StartServer();
        serverStarted = true;
        UpdateClientButton();
    }
    void UpdateClientButton()
    {
        //clientButton.interactable = serverStarted;
    }
    public void ConnectClient()
    {
        manager.StartClient();
        if (!NetworkServer.active)
        {

            //update.text = "Please start the server first";
            Debug.Log("sTART THE SERVER FIRST");
        }
        else
        {
            
            //update.text = "Connecting to the server";
            Debug.Log("the server is upand runing");
        }
    }
}
