using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBet : MonoBehaviour
{
    int wallet;
    int minBet;

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

    public bool canAllIn(int amount)
    {
        if (amount > (minBet * 4))
            return true;
        else
            return false;
    }

    public void setMinBet(int amount)
    {
        minBet = amount;
    }

    public int getMinBet()
    {
        return minBet;
    }

    public int getWallet()
    {
        return wallet;
    }
}
