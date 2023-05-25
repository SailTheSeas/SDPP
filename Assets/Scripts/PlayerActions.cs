using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class PlayerActions : NetworkBehaviour
{
    private GameController GC;
    private PlayerHand PH;
    private PlayerBet BT;
    [SerializeField]
    private string playerName = "Blank";
    private bool isAllIn = false;
    private bool isFolded = false;
    private bool isTurn = false;
    private bool hasTakenTurn = true;
    private bool hasTakenAction = false;
    private PlayerType playerType;
    [SerializeField]
    Button playerFoldButton, playerRaiseButton, playerCallButton, playerAllInButton;
    //public GameObject test;
    int amountBet = 0;
    int gamesfolded = 0;
    int gamesWon = 0;
    int gamesLost = 0;

    private void Start()
    {
        PH = this.GetComponent<PlayerHand>();
        BT = this.GetComponent<PlayerBet>();
        
    }
    private void Awake()
    {
        playerFoldButton = this.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Button>();
        playerRaiseButton = this.transform.GetChild(1).GetChild(0).GetChild(5).GetComponent<Button>();
        playerCallButton = this.transform.GetChild(1).GetChild(0).GetChild(4).GetComponent<Button>();
        playerAllInButton = this.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>();
    }

    public void createPlayer(TableHand newTH, GameController newGC, int newMinBet,int money)
    {
        //test.SetActive(true);
        assignTable(newTH);
        assignGameController(newGC);
        setMinBet(newMinBet);
        addMoney(money);
        PH.setCardImages(GC.getCardImages(), GC.getBackImage());
    }
     

    public void assignTable(TableHand TH)
    {
        PH.assignTable(TH);
    }

    public void assignGameController(GameController newGC)
    {
        GC = newGC;
    }

    public void assignButtons(Button foldButton, Button raiseButton,Button callButton, Button allInButton)
    {
        playerFoldButton = foldButton;
        playerRaiseButton = raiseButton;
        playerCallButton = callButton;
        playerAllInButton = allInButton;
    }

    public void setButtonsState(bool state)
    {
        playerFoldButton.interactable = state;
        playerRaiseButton.interactable = state;
        playerCallButton.interactable = state;
        playerAllInButton.interactable = state;
    }

    public void setButtonsStateEXC(bool state)
    {
        playerFoldButton.interactable = state;
        playerCallButton.interactable = state;
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
                    setButtonsState(false);
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
                setButtonsState(false);
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
                    GC.potAddMoney(amount);
                    BT.removeMoney(amount);
                    Debug.Log("Turn Over, betted " + amount);
                    setTurn(false);
                    setHasTakenTurn(true);
                    setButtonsState(false);
                    amountBet += amount;
                    GC.setNextPlayer(false);

                }
                else
                {
                    amount = GC.getMinBet();
                    if (BT.canBet(amount))
                    {
                        GC.potAddMoney(amount);
                        BT.removeMoney(amount);
                        Debug.Log("Turn Over, betted " + amount);
                        setTurn(false);
                        setHasTakenTurn(true);
                        setButtonsState(false);
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
                amount = GC.getRaiseMatchAmount();
                if (BT.canBet(amount))
                {
                    GC.potAddMoney(amount);
                    BT.removeMoney(amount);
                    Debug.Log("Called raise:  " + amount);
                    setHasTakenAction(true);
                    setButtonsState(false);
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
                    GC.potAddMoney(amount);
                    BT.removeMoney(amount);
                    setTurn(false);
                    setButtonsState(false);
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

    public void allIn()
    {

        if (isTurn)
        {
            int amount;
            if (GC.getIsFirstRound() == true)
            {
                Debug.Log("You cant go all in yet");
            }
            else
            {
                amount = BT.getWallet();
                if (BT.canBet(amount))
                {
                    GC.potAddMoney(amount);
                    BT.removeMoney(amount);
                    setTurn(false);
                    setHasAllIn(true);
                    setButtonsState(false);
                    setHasTakenTurn(true);
                    setHasTakenAction(true);
                    amountBet += amount;
                    Debug.Log("Turn Over, went all in with: " + amount);
                    GC.setNextPlayer(false);                   
                }
                else
                    Debug.Log("You cant go all in");
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

    public void setHasAllIn(bool state)
    {
        isAllIn = state;
    }

    public bool gethasAllIn()
    {
        return isAllIn;
    }

    public bool getIsFolded()
    {
        return isFolded;
    }

    public Card getCard(int num)
    {
        return PH.getCard(num);
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

    public Sprite[,] getCardImages()
    {
        return GC.getCardImages();
    }

    public Sprite getBackImage()
    {
        return GC.getBackImage();
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
