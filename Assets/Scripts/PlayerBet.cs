using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBet : MonoBehaviour
{
    int wallet;

    void start()
    {
        wallet = 0;
    }

    public void addMoney(int amount)
    {
        wallet += amount;
    }

    public void removeMoney(int amount)
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
