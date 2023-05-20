using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserLogIn : MonoBehaviour
{
    //Location of php document on Server side
    private string _connURL = "http://localhost:81/mysite/login.php";
    
    private string _resultDB; //will be used to parse the database
    private string[] recordList;
    
    
    //Create variables for the form, (username and password) 
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    
    //Function on a button, will start coroutines
    public void ConnectOnClick()
    {
        StartCoroutine(LogIn());
    }
    
    IEnumerator LogIn()
    {
        //Create a Form that takes in username and password
        WWWForm form = new WWWForm();
        form.AddField("username",nameField.text);
        form.AddField("userpass",passwordField.text);
        //Then pass the form into the web request
        
        
        //Unity Web Request posts data to the URL
        UnityWebRequest www = UnityWebRequest.Post(_connURL,form);
        yield return www.SendWebRequest();
        
        //Check if successfully connects to the URL, SUCCESS could mean a log in or an account exists
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log("Connection was not made");
            Debug.Log( www.error);
            Debug.Log(www.downloadHandler.text); // We want to see if the results of the posted form are available to use
        } 
        else
        {
            Debug.Log("Login was a success");
            // Storing the resultant output into an array to access its members
            _resultDB = www.downloadHandler.text;
            
            Debug.Log(www.downloadHandler.text);

            recordList = _resultDB.Split('\t');
            DBController.Username = nameField.text;
            DBController.UserID = int.Parse(recordList[1]);
            DBController.NoGames = int.Parse(recordList[2]);
            DBController.NoWins = int.Parse(recordList[3]);
            
            Debug.Log("His name is " + DBController.Username);

            /* foreach (string s in recordList)
            {
                Debug.Log(s);
            }*/
            
            //DBController.Username = 
            SceneManager.LoadScene(3);
        }
        //StartCoroutine(ProofCon());
    }
}
