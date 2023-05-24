using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBController 
{
  // static data members related to database
  private static string _username;
  
  //Making these public for now
  
  public static int NoWins;
  public static int NoGames;
  public static int UserID;
  
  // 
  public static string Username
  {
      get => _username;
      set => _username = value ; 
  }

  //A check to see if logged in or not
  public static bool LoggedIn()
  {
      return (_username != null);
  }
  
  public static void LogOut()
  {
      _username = null;
  }


}
