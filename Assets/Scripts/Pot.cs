using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    int potValue = 0;
    public void addMoney(int amount)
    {
        potValue += amount;
    }    

    public int getPot()
    {
        return potValue;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
