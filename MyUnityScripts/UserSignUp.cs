using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UserSignUp : MonoBehaviour
{   
    //Location of php document on Server side
    private string _connURL = "http://localhost:81/mysite/signup.php";
    
    //Create variables for the form, (username and password) 
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    
    //Function on a button, will start coroutines
    public void ConnectOnClick()
    {
        StartCoroutine(SignUp());
    }
    
    IEnumerator SignUp()
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
            Debug.Log("Failure"); //Fail
            Debug.Log(www.downloadHandler.text);
           
        } 
        else
        {   
            Debug.Log("Success");
            Debug.Log(www.downloadHandler.text);
            SceneManager.LoadScene(3);
        }
 
    }
}
