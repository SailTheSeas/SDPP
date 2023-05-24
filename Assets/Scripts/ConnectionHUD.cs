using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ConnectionHUD : MonoBehaviour
{
    [SerializeField] GameObject ClientButton;
    [SerializeField] GameObject ServerButton;
    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            ClientButton.SetActive(false);
            Debug.Log("I know you're playing this in the windows version of Unity");
        }
        if(Application.platform==RuntimePlatform.WebGLPlayer)
        {
            ServerButton.SetActive(false);
        }
    }
}
