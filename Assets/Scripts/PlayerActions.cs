using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private GameController GC;
    private Pot betting;
    private PlayerHand PH;
    private PlayerBet BT;
    [SerializeField]
    private string playerName = "Blank";
    private bool isFolded = false;
    private bool isTurn = false;
    private bool hasTakenTurn = true;
    private bool hasTakenAction = false;
    private PlayerType playerType;

    int amountBet = 0;
    int gamesfolded = 0;
    int gamesWon = 0;
    int gamesLost = 0;

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

    public void setMinBet(int amount)
    {
        BT.setMinBet(amount);
    }
    public void addMoney(int amount)
    {
        BT.addMoney(amount);
    }

    public void drawCards()
    {
        PH.drawCards();
    }

    public void clearDisplay()
    {
        PH.clearDisplay();
    }

    public void fold()
    {
        if (!GC.getIsRaised())
            if (isTurn)
            {
                if (GC.getIsFirstRound() == true && (playerType == PlayerType.SMALL_BLIND || playerType == PlayerType.BIG_BLIND))
                {
                    Debug.Log("You must bet");
                }
                else
                {
                    setFolded(true);
                    setTurn(false);
                    setHasTakenTurn(true);
                    gamesfolded++;
                    GC.setNextPlayer(false);
                }
            }
            else
                Debug.Log("NOT YOUR TURN!!!!");
        else
        {
            if (hasTakenTurn && !hasTakenAction)
            {
                Debug.Log("Didnt match raise, folded");
                setHasTakenAction(true);
                setFolded(true);
                gamesfolded++;
                GC.waitForActions();
            }
        }
    }

    public void call()
    {
        int amount;
        if (!GC.getIsRaised())
            if (isTurn)
            {
                
                if (GC.getIsFirstRound() == true && (playerType == PlayerType.SMALL_BLIND || playerType == PlayerType.BIG_BLIND))
                {
                    if (playerType == PlayerType.BIG_BLIND)
                        amount = GC.getBigBlind();
                    else
                        amount = GC.getSmallBlind();
                    betting.addMoney(amount);
                    Debug.Log("Turn Over, betted " + amount);
                    setTurn(false);
                    setHasTakenTurn(true);
                    amountBet += amount;
                    GC.setNextPlayer(false);

                }
                else
                {
                    amount = GC.getMinBet();
                    if (BT.canBet(amount))
                    {
                        betting.addMoney(amount);
                        Debug.Log("Turn Over, betted " + amount);
                        setTurn(false);
                        setHasTakenTurn(true);
                        amountBet += amount;
                        GC.setNextPlayer(false);
                    }
                    else
                        Debug.Log("You cant bet that");
                }
            }
            else
                Debug.Log("NOT YOUR TURN!!!!");
        else
        {
            if (hasTakenTurn && !hasTakenAction)
            {
                amount = GC.getMinBet();
                if (BT.canBet(amount))
                {
                    betting.addMoney(amount);
                    Debug.Log("Called raise:  " + amount);
                    setHasTakenAction(true);
                    amountBet += amount;
                    GC.waitForActions();
                }
                else
                    Debug.Log("You cant bet that");
            }
        }
    }

    public void raise()
    {
        
        if (isTurn)
        {
            int amount;
            if (GC.getIsFirstRound() == true && (playerType == PlayerType.SMALL_BLIND || playerType == PlayerType.BIG_BLIND))
            {
                Debug.Log("You cant raise yet");
            }
            else
            {
                amount = GC.getMinRaise();
                if (BT.canBet(amount))
                {
                    betting.addMoney(amount);
                    setTurn(false);
                    setHasTakenTurn(true);  
                    setHasTakenAction(true);
                    amountBet += amount;
                    GC.raise(amount);
                    Debug.Log("Turn Over, raised by " + amount + ". All players must either call or fold now. The min bet is: " + GC.getMinBet() + " and the min raise is now: " + GC.getMinRaise());
                    GC.setNextPlayer(true);
                    GC.waitForActions();
                }
                else
                    Debug.Log("You cant raise");
            }
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

    public void setPlayerName(string newName)
    {
        playerName = newName;
    }

    public void setHasTakenAction(bool state)
    {
        hasTakenAction = state;
    }

    public void setHasTakenTurn(bool state)
    {
        hasTakenTurn = state;
    }

    public bool getIsFolded()
    {
        return isFolded;
    }

    public bool getHasTakenAction()
    {
        return hasTakenAction;
    }

    public PlayerType getPlayerType()
    {
        return playerType;
    }

    public bool getPlayerFolded()
    {
        return isFolded;
    }

    public bool getHasTakenTurn()
    {
        return hasTakenTurn;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public int getPlayerWallet()
    {
        return BT.getWallet();
    }

    public void isWinner()
    {
        gamesWon++;
    }

    public void isLower()
    {
        gamesLost++;
    }

}
