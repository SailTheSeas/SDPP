using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    GameController GC;
    Pot betting;
    PlayerHand PH;
    PlayerBet BT;
    private bool isFolded = false;
    private bool isTurn = false;
    private PlayerType playerType;

    private void Start()
    {
        PH = this.GetComponent<PlayerHand>();
        BT = this.GetComponent<PlayerBet>();
    }

    public void assignPot(Pot newPot)
    {
        betting = newPot;
    }

    public void assignTable(TableHand TH)
    {
        PH.assignTable(TH);
    }

    public void assignGameController(GameController newGC)
    {
        GC = newGC;
    }
    public void addMoney(int amount)
    {
        BT.addMoney(amount);
    }

    public void drawCards()
    {
        PH.drawCards();
    }

    public void fold()
    {
        if (isTurn)
        {
            setFolded(true);
            setTurn(false);
            GC.turnHandler();
        }
        else
            Debug.Log("NOT YOUR TURN!!!!");
    }

    public void bet()
    {
        if (isTurn)
        {
            if (BT.canBet(50))
            {
                betting.addMoney(50);
                Debug.Log("Turn Over, betted 50");
                setTurn(false);
                GC.turnHandler();
            }
            else
                Debug.Log("You cant bet that");
        }
        else
            Debug.Log("NOT YOUR TURN!!!!");
    }

    public void setPlayerType(PlayerType newPlayerType)
    {
        playerType = newPlayerType;
    }

    public void setTurn(bool state)
    {
        isTurn = state;
    }

    public void setFolded(bool state)
    {
        isFolded = state;
    }

    public bool getIsFolded()
    {
        return isFolded;
    }

    public PlayerType getPlayerType()
    {
        return playerType;
    }

    public bool getPlayerFolded()
    {
        return isFolded;
    }

}
