using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

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
        /*if (Application.platform == RuntimePlatform.WindowsPlayer|| Application.platform==RuntimePlatform.WindowsEditor)
        {
            ClientButton.SetActive(false);
            //ServerButton.SetActive(false);
            Debug.Log("I know you're playing this in the windows version of Unity");
        }
        if(Application.platform==RuntimePlatform.WebGLPlayer)
        {
            ServerButton.SetActive(false);
        }*/
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
