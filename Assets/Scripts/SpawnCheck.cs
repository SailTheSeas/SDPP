using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Mirror;


//KE - used to check if chat function would work. This updates the player's username if they change it
public class SpawnCheck : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] string playerName="Player";
    [SerializeField] Text displayName;
    [SerializeField] InputField input;

    private void Start()
    {
        /*input = GameObject.FindGameObjectWithTag("Login").GetComponentInChildren<InputField>();
        displayName = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>();*/
        changeName();
    }

    //KE - The usernames have a character max length of 15. This is to avoid problems from occuring in the chat
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
