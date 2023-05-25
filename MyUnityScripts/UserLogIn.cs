using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UserLogIn : MonoBehaviour
{
    //Location of php document on Server side
    private string _connURL = "http://localhost:81/mysite/login.php";
    
    private string _resultDB; //will be used to parse the database
    private string[] recordList; //array of strings that will store database records
    
    
    //Created variables for the form, (username and password) 
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    
    //Function on a button, will start coroutine to Log in
    public void ConnectOnClick()
    {
        StartCoroutine(LogIn());
    }
    
    IEnumerator LogIn()
    {
        //Creates a Form that takes in username and password from the input fields
        WWWForm form = new WWWForm();
        form.AddField("username",nameField.text);  
        form.AddField("userpass",passwordField.text);
        //Then passes the form into the web request and our PHP script has access to the data
        
        
        //Unity Web Request posts data to the URL
        UnityWebRequest www = UnityWebRequest.Post(_connURL,form);
        yield return www.SendWebRequest();
        
        //Check if successfully connects to the URL, SUCCESS could mean a log in or an account exists
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log( www.error + "Connection was not made");
            Debug.Log(www.downloadHandler.text); // We want to see if the results of the posted form are available to use
        } 
        else
        {
            Debug.Log("Login was a success");
            // Storing the resultant output into an array to access its members
            _resultDB = www.downloadHandler.text; //each column of the database is separated with a \t
            
            Debug.Log(www.downloadHandler.text);

            //Makes use of the \t  as a separator to form an array that stores the database information according to an index
            recordList = _resultDB.Split('\t');
            
            //passes the database records into a static class that will store user records while they are logged in.
            DBController.Username = nameField.text;
            DBController.UserID = int.Parse(recordList[1]);
            DBController.NoGames = int.Parse(recordList[2]);
            DBController.NoWins = int.Parse(recordList[3]);
            
            Debug.Log("His name is " + DBController.Username);

            //Proceed to the in game lobby
            SceneManager.LoadScene(3);
        }
       
    }
}
