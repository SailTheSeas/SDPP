using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public InputField input;
    public string playerName;
    public Button applyName;
    public void SetName()
    {
        playerName = input.text;
        GameObject.FindGameObjectWithTag("Login").GetComponent<ConnectionHUD>().clientButton.interactable = true;
        GameObject.FindGameObjectWithTag("Login").GetComponent<ConnectionHUD>().clientButton.transform.GetChild(0).GetComponent<Text>().text = "Connect as Client";
    }
    private void FixedUpdate()
    {
        if (input.text.Length > 10||input.text.Contains(" ")||input.text=="")
        {
            applyName.interactable = false;
        }
        else
        {
            applyName.interactable = true;
        }
    }
}
