using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBet : MonoBehaviour
{
    int wallet;

    void initialize()
    {
        wallet = 0;
    }

    void addMoney(int amount)
    {
        wallet += amount;
    }

    void removeMonet(int amount)
    {
        wallet -= amount; ;
    }

   public bool canBet(int amount)
    {
        if (amount > wallet || amount <= 0)
            return false;
        else
            return true;
    }
}
