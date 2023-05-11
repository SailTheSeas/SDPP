using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    int smallBluffBet = 0;
    int bigBluffBet = 0;
    int potValue = 0;
    public void addMoney(int amount)
    {
        potValue += amount;
    }    

    public int getPot()
    {
        return potValue;
    }

    public int getSmallBluffBet()
    {
        return smallBluffBet;
    }

    public int getBigBluffBet()
    {
        return bigBluffBet;
    }

    public void setSmallBluffBet(int newSmallBluffBet)
    {
        smallBluffBet = newSmallBluffBet;
    }
    public void setBigBluffBet(int newBigBluffBet)
    {
        bigBluffBet = newBigBluffBet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
