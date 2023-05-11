using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerActions playerOneActions, playerTwoActions, playerThreeActions, playerFourActions;
    [SerializeField]
    TableHand TH;
    [SerializeField]
    Pot pot;

    int turnCounter, roundCounter = 0;

    private PlayerType playerOneType, playerTwoType, playerThreeType, playerFourType;
    private void Start()
    {
        playerOneType = PlayerType.Dealer;
        playerTwoType = PlayerType.Small_Bluff;
        playerThreeType = PlayerType.Big_Bluff;
        playerFourType = PlayerType.None;
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
        turnHandler();
        //roundHandler();
    }

    public void handOutTable()
    {

        Debug.Log("Table Handed Out");
    }

    public int getCurrentTurn()
    {
        return turnCounter;
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
    }

    public void roundHandler()
    {

        switch (roundCounter)
        {
            case 0:
                TH.showFlop();
                break;
            case 1:
                break;
            case 2:
                TH.showTurn();
                break;
            case 3:
                break;
            case 4:
                TH.showRiver();
                break;
            case 5:
                Debug.Log("Game End");
                break;
            default:
                break;
        }
        //turnHandler();
    }

    public void turnHandler()
    {
        switch (turnCounter)
        {
            case 0:
                roundHandler();
                roundCounter++;
                if (roundCounter != 5)
                    playerTurn(playerOneActions, 1);
                break;
            case 1:
                playerTurn(playerTwoActions, 2);
                break;
            case 2:
                playerTurn(playerThreeActions, 3);
                break;
            case 3:
                playerTurn(playerFourActions, 4);
                break;
            default:
                break;
        }
    }

    public void playerTurn(PlayerActions currentPlayer, int playerNum)
    {
        if (currentPlayer.getPlayerFolded())
        {
            if (playerNum == 4)
            {
                turnCounter = 0;
                roundHandler();
            }
            else
            {
                turnCounter++;
                turnHandler();
            }
            Debug.Log("Player " + playerNum + " is Folded, Skipping Turn");

        }
        else
        {
            if (playerNum == 4)
            {
                turnCounter = 0;
            }
            else
                turnCounter++;
            currentPlayer.setTurn(true);
            Debug.Log("Player " + playerNum + " your turn now");
        }
    }
}
