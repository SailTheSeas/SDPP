using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerActions playerOneActions, playerTwoActions, playerThreeActions, playerFourActions;
    TableHand TH;
    Pot pot;
    [SerializeField]
    int bigBlind;
    int smallBlind;
    int minBet;
    int minRaise;

    bool firstRound = true;
    bool raised = false;

    int roundCounter = 0;

    private PlayerType playerOneType, playerTwoType, playerThreeType, playerFourType;
    private PlayerActions currentPlayer;
    public void gameStart(Button starter)
    {
        starter.enabled = false;
        smallBlind = (int)(bigBlind/2);
        minBet = bigBlind;
        minRaise = bigBlind *2;
        playerOneType = PlayerType.DEALER;
        playerTwoType = PlayerType.SMALL_BLIND;
        playerThreeType = PlayerType.BIG_BLIND;
        playerFourType = PlayerType.NONE;
        setPlayerTypes(playerOneType, playerTwoType, playerThreeType, playerFourType);

        TH = this.GetComponent<TableHand>();
        pot = this.GetComponent<Pot>();


        playerOneActions.assignTable(TH);
        playerTwoActions.assignTable(TH);
        playerThreeActions.assignTable(TH);
        playerFourActions.assignTable(TH);

        playerOneActions.assignPot(pot);
        playerTwoActions.assignPot(pot);
        playerThreeActions.assignPot(pot);
        playerFourActions.assignPot(pot);

        playerOneActions.assignGameController(this);
        playerTwoActions.assignGameController(this);
        playerThreeActions.assignGameController(this);
        playerFourActions.assignGameController(this);

        playerOneActions.addMoney(1000);
        playerTwoActions.addMoney(1000);
        playerThreeActions.addMoney(1000);
        playerFourActions.addMoney(1000);

        currentPlayer = findSmallBlind();

        Debug.Log("Game Started");
    }

    private void handOutCards()
    {
        TH.newRound();
        playerOneActions.drawCards();
        playerTwoActions.drawCards();
        playerThreeActions.drawCards();
        playerFourActions.drawCards();
        TH.drawHand();
        Debug.Log("Cards Handed Out");
        playerTurn();
        //roundHandler();
    }

    public void handOutTable()
    {

        Debug.Log("Table Handed Out");
    }

    public int getCurrentRound()
    {
        return roundCounter;
    }

    public void setPlayerTypes(PlayerType playerOne, PlayerType playerTwo, PlayerType playerThree, PlayerType playerFour)
    {
        playerOneType = playerOne;
        playerTwoType = playerTwo;
        playerThreeType = playerThree;
        playerFourType = playerFour;

        playerOneActions.setPlayerType(playerOneType);
        playerTwoActions.setPlayerType(playerTwoType);
        playerThreeActions.setPlayerType(playerThreeType);
        playerFourActions.setPlayerType(playerFourType);
    }

    public void resetDeck()
    {
        TH.reshuffle();
    }

    public void nextGame()
    {
        playerOneActions.clearDisplay();
        playerTwoActions.clearDisplay();
        playerThreeActions.clearDisplay();
        playerFourActions.clearDisplay();
        setPlayerTypes(playerFourType, playerOneType, playerTwoType, playerThreeType);

        assessPlayerWallet(playerOneActions);
        assessPlayerWallet(playerTwoActions);
        assessPlayerWallet(playerThreeActions);
        assessPlayerWallet(playerFourActions);

        resetPlayerActions();
        resetPlayerTurns();

        firstRound = true;
        roundCounter = 0;

        playerOneActions.setTurn(false);
        playerTwoActions.setTurn(false);
        playerThreeActions.setTurn(false);
        playerFourActions.setTurn(false);

        currentPlayer = findSmallBlind();
        TH.clearDisplay();


        Debug.Log("New Game Begin");
    }

    public void waitForActions()
    {
        if ((playerOneActions.getHasTakenAction() || !playerOneActions.getHasTakenTurn()) && 
            (playerTwoActions.getHasTakenAction() || !playerTwoActions.getHasTakenTurn()) && 
            (playerThreeActions.getHasTakenAction() || !playerThreeActions.getHasTakenTurn()) && 
            (playerFourActions.getHasTakenAction() || !playerFourActions.getHasTakenTurn()))
        {
            resetPlayerActions();
            resetPlayerTurns();

            raised = false;
            Debug.Log("Players have called or raised");

            if (currentPlayer.getPlayerType() == PlayerType.SMALL_BLIND)
            {
                roundCounter++;
                roundHandler();

            }
            else
                playerTurn();
        }
    }

    public void setNextPlayer(bool isRaised)
    {
        if (currentPlayer == playerOneActions)
            currentPlayer = playerTwoActions;
        else
            if (currentPlayer == playerTwoActions)
                currentPlayer = playerThreeActions;
            else 
                if (currentPlayer == playerThreeActions)
                    currentPlayer = playerFourActions;
                else
                    currentPlayer = playerOneActions;
        if (isRaised)
        {
            raised = true;
        }
        else
        {

            if (currentPlayer.getPlayerType() == PlayerType.SMALL_BLIND)
            {
                roundCounter++;
                roundHandler();

            }
            else
                playerTurn();
        }
    }

    public void resetPlayerActions()
    {
        playerOneActions.setHasTakenAction(false);
        playerTwoActions.setHasTakenAction(false);
        playerThreeActions.setHasTakenAction(false);
        playerFourActions.setHasTakenAction(false);
    }

    public void resetPlayerTurns()
    {
        playerOneActions.setHasTakenTurn(false);
        playerTwoActions.setHasTakenTurn(false);
        playerThreeActions.setHasTakenTurn(false);
        playerFourActions.setHasTakenTurn(false);
    }

    public void assessPlayerWallet(PlayerActions player)
    {
        if (player.getPlayerWallet() >= bigBlind)
            player.setFolded(false);
        else
        {
            Debug.Log("Player " + player.getPlayerName() +  " is broke, giving more money");
            player.addMoney(bigBlind * 2);
            player.setFolded(false);
        }
    }

    public void roundHandler()
    {

        switch (roundCounter)
        {
            case 0:
                
                break;
            case 1:
                resetPlayerTurns();
                TH.showFlop();
                firstRound = false;
                playerTurn();
                break;
            case 2:
                resetPlayerTurns();
                TH.showTurn();
                playerTurn();
                break;
            case 3:
                resetPlayerTurns();
                TH.showRiver();
                playerTurn();
                break;
            case 4:
                resetPlayerTurns();
                Debug.Log("Game End");
                endGame();
                break;
            case 5:

                break;
            default:
                break;
        }
        //turnHandler();
    }

  
    public void playerTurn()
    {
        if (currentPlayer.getPlayerFolded())
        {
            Debug.Log("Player " + currentPlayer.getPlayerName() + " is Folded, Skipping Turn");
            setNextPlayer(false);          
        }
        else
        {
            currentPlayer.setTurn(true);
            Debug.Log("Player " + currentPlayer.getPlayerName() + " your turn now");
        }
    }

    void potAddMoney(int amount)
    {
        pot.addMoney(amount);
    }

    public int getPot()
    {
        return pot.getPot();
    }

    public bool getIsFirstRound()
    {
        return firstRound;
    }

    public int getMinBet()
    {
        return minBet;
    }

    public int getMinRaise()
    { 
        return minRaise; 
    }

    public int getSmallBlind()
    {
        return smallBlind;
    }

    public int getBigBlind()
    {
        return bigBlind;
    }

    public bool getIsRaised()
    {
        return raised;
    }

    public void raise(int amount)
    {
        minBet = amount;
        minRaise = 2 * amount;
    }

    public PlayerActions findSmallBlind()
    {
        PlayerActions dealer;

        if (playerOneActions.getPlayerType() == PlayerType.SMALL_BLIND)
            dealer = playerOneActions;
        else
            if (playerTwoActions.getPlayerType() == PlayerType.SMALL_BLIND)
                dealer = playerTwoActions;
            else
                if (playerThreeActions.getPlayerType() == PlayerType.SMALL_BLIND)
                    dealer = playerThreeActions;
                else
                    dealer = playerFourActions;    
       return dealer;
    }

    public void endGame()
    {
        currentPlayer = null;
    }
}
