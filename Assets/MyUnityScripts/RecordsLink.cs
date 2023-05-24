using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsLink : MonoBehaviour
{
    private string _connURL = "http://localhost:81/mysite/update.php";
    
    public void UpdateRecords()
    {
        
    }


    public void RetrieveRecords()
    {
        
    }

    public void ViewRecords()
    {
        Debug.Log(DBController.Username);
        Debug.Log(DBController.UserID);
    }
    
    
}
