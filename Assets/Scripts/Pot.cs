using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    int potValue = 0;
    int remainder = 0;
    public void addMoney(int amount)
    {
        potValue += amount;
    }

    public void resetPot()
    {
        potValue = 0;
        remainder = 0;
    }

    public int getPot()
    {
        return potValue;
    }

    public int getRemainder()
    {
        return remainder;
    }

    public int getSplitPotValue(int numOfWinners)
    {
        int splitAmount;

        splitAmount = (int)(potValue / numOfWinners);
        remainder = potValue - splitAmount;

        return splitAmount;
    }
}

