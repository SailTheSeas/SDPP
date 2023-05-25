using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RecordsLink : MonoBehaviour
{
    //Location of php document on the server side
    private string _connURL = "http://localhost:81/mysite/update.php";
    
    public void UpdateRecords()
    {
        StartCoroutine(RecordData());
    }

    IEnumerator RecordData()
    {
        //Create a Form that takes in username and password
        WWWForm form = new WWWForm();
        form.AddField("username",DBController.Username);
        form.AddField("played",DBController.NoGames);
        form.AddField("won",DBController.NoWins);
        //Then pass the form into the web request
      

        //Unity Web Request posts data to the URL
        UnityWebRequest www = UnityWebRequest.Post(_connURL, form);
        yield return www.SendWebRequest();

        //Check if successfully connects to the URL, SUCCESS could mean a log in or an account exists
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Failure"); //Fail
            Debug.Log(www.downloadHandler.text);

        }
        else
        {
            Debug.Log("Success");
            Debug.Log(www.downloadHandler.text);

        }
    }

    public void AffectStats()
    {
        DBController.NoGames += 2;
        DBController.NoWins += 1;
    }
    public void RetrieveRecords()
    {
        
    }

    public void ViewRecords()
    {
        Debug.Log(DBController.Username);
        Debug.Log(DBController.UserID);
        Debug.Log(DBController.NoGames);
    }
    
    
}
