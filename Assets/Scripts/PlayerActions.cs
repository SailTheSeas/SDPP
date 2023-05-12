using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    Pot betting;
    PlayerHand PH;
    PlayerBet BT;

    private void Start()
    {
        PH = this.GetComponent<PlayerHand>();
        BT = this.GetComponent<PlayerBet>();
    }

    public void assignPot(Pot newPot)
    {
        betting = newPot;
    }

    public void fold()
    {

    }
}
