using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Mirror;

public class SpawnCheck : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] string playerName="Player";
    [SerializeField] Text displayName;
    [SerializeField] InputField input;
    /*public override void OnStartAuthority()
    {
        Instantiate(cube);
    }*/
    private void Start()
    {
        input = GameObject.FindGameObjectWithTag("Login").GetComponentInChildren<InputField>();
        displayName = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>();
        changeName();
    }

    public void enterName()
    {
        if(input.text.Length>=15)
        {
            Debug.Log("Name too long");
        }
        playerName = input.text;
        input.text = " ";
        changeName();
    }
    private void changeName()
    {
        displayName.text = playerName;
    }
}
