using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerActions[] playerActions = new PlayerActions[4];
    TableHand TH;
    Pot pot;
    GameWin GW;
    [SerializeField]
    Sprite[] hearts = new Sprite[13];
    [SerializeField]
    Sprite[] clubs = new Sprite[13];
    [SerializeField]
    Sprite[] spades = new Sprite[13];
    [SerializeField]
    Sprite[] diamonds = new Sprite[13];
    Sprite[,] cardImages = new Sprite[4, 13];
    [SerializeField]
    Sprite backImage;

    [SerializeField]
    int bigBlind;
    int smallBlind;
    int previousBet;
    int minBet;
    int minRaise;

    bool firstRound = true;
    bool raised = false;

    int roundCounter = 0;

    [SerializeField]
    Button newGame, drawCards;
    private PlayerType playerOneType, playerTwoType, playerThreeType, playerFourType;
    private PlayerActions currentPlayer;

    public void SetPlayer(PlayerActions player)
    {
        //Debug.Log("pLAYER: " + player);
        for(int i=0; i<4; i++)
        {
            if (playerActions[i] == null)
            {
                //Debug.Log("cheese");
                playerActions[i] = player;
                break;
            }
        }
    }
    public void gameStart(Button starter)
    {
        for (int i = 0; i < 13; i++)
        {
            cardImages[0, i] = hearts[i];
            cardImages[1, i] = clubs[i];
            cardImages[2, i] = spades[i];
            cardImages[3, i] = diamonds[i];
        }
        starter.interactable = false;
        drawCards = GameObject.FindGameObjectWithTag("DrawCards").GetComponent<Button>();
        drawCards.interactable = true;
        previousBet = 0;
        smallBlind = (int)(bigBlind / 2);
        minBet = bigBlind;
        minRaise = bigBlind * 2;
        playerOneType = PlayerType.DEALER;
        playerTwoType = PlayerType.SMALL_BLIND;
        playerThreeType = PlayerType.BIG_BLIND;
        playerFourType = PlayerType.NONE;
        setPlayerTypes(playerOneType, playerTwoType, playerThreeType, playerFourType);

        TH = this.GetComponent<TableHand>();
        pot = this.GetComponent<Pot>();
        GW = this.GetComponent<GameWin>();

        TH.setCardImages(cardImages, backImage);

        playerActions[0].createPlayer(TH, this, minBet, 1000);
        playerActions[1].createPlayer(TH, this, minBet, 1000);
        playerActions[2].createPlayer(TH, this, minBet, 1000);
        playerActions[3].createPlayer(TH, this, minBet, 1000);

        currentPlayer = findSmallBlind();

        Debug.Log("Game Started");
    }

    public void handOutCards()
    {
        TH = GameObject.FindGameObjectWithTag("GameController").GetComponent<TableHand>();
        drawCards.interactable = false;
        TH.newRound();
        playerActions[0].drawCards();
        playerActions[1].drawCards();
        playerActions[2].drawCards();
        playerActions[3].drawCards();
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

        playerActions[0].setPlayerType(playerOneType);
        playerActions[1].setPlayerType(playerTwoType);
        playerActions[2].setPlayerType(playerThreeType);
        playerActions[3].setPlayerType(playerFourType);
    }

    public void resetDeck()
    {
        TH.reshuffle();
    }

    public void nextGame()
    {
        newGame.interactable = false;
        drawCards.interactable = true;
        minBet = 5;
        minRaise = minBet * 2;
        previousBet = minBet;

        for (int i = 0; i < 4; i++)
        {
            playerActions[i].clearDisplay();
            playerActions[i].setTurn(false);
            playerActions[i].setMinBet(minBet);
            assessPlayerWallet(playerActions[i]);
        }

        setPlayerTypes(playerFourType, playerOneType, playerTwoType, playerThreeType);

        resetPlayerActions();
        resetPlayerTurns();
        resetPlayerAllIn();
        resetPlayerButtons();

        firstRound = true;
        roundCounter = 0;

        currentPlayer = findSmallBlind();
        TH.clearDisplay();
        pot.resetPot();

        Debug.Log("New Game Begin");
    }

    public void waitForActions()
    {
        if ((playerActions[0].getHasTakenAction() || !playerActions[0].getHasTakenTurn() || playerActions[0].getIsFolded() || playerActions[0].gethasAllIn()) &&
            (playerActions[1].getHasTakenAction() || !playerActions[1].getHasTakenTurn() || playerActions[1].getIsFolded() || playerActions[1].gethasAllIn()) &&
            (playerActions[2].getHasTakenAction() || !playerActions[2].getHasTakenTurn() || playerActions[2].getIsFolded() || playerActions[2].gethasAllIn()) &&
            (playerActions[3].getHasTakenAction() || !playerActions[3].getHasTakenTurn() || playerActions[3].getIsFolded() || playerActions[3].gethasAllIn()))
        {
            resetPlayerActions();
            resetPlayerButtons();

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
        if (currentPlayer == playerActions[0])
            currentPlayer = playerActions[1];
        else
            if (currentPlayer == playerActions[1])
            currentPlayer = playerActions[2];
        else
                if (currentPlayer == playerActions[2])
            currentPlayer = playerActions[3];
        else
            currentPlayer = playerActions[0];
        if (isRaised)
        {
            raised = true;
            for (int i = 0; i < 4; i++)
            {
                setButtonState(playerActions[i]);
            }
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

    public void setButtonState(PlayerActions player)
    {
        if (player.getHasTakenTurn() && !player.getHasTakenAction() && !player.gethasAllIn())
            player.setButtonsStateEXC(!player.getIsFolded());
    }

    public void resetPlayerAllIn()
    {
        for (int i = 0; i < 4; i++)
        {
            playerActions[i].setHasAllIn(false);
        }
    }

    public void resetPlayerActions()
    {
        for (int i = 0; i < 4; i++)
        {
            playerActions[i].setHasTakenAction(false);
        }
    }

    public void resetPlayerTurns()
    {
        for (int i = 0; i < 4; i++)
        {
            playerActions[i].setHasTakenTurn(false);
        }
    }

    public void resetPlayerButtons()
    {
        for (int i = 0; i < 4; i++)
        {
            playerActions[i].setButtonsState(false);
        }
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
                resetPlayerActions();
                resetPlayerButtons();
                TH.showFlop();
                firstRound = false;
                playerTurn();
                break;
            case 2:
                resetPlayerTurns();
                resetPlayerActions();
                resetPlayerButtons();
                TH.showTurn();
                playerTurn();
                break;
            case 3:
                resetPlayerTurns();
                resetPlayerActions();
                resetPlayerButtons();
                TH.showRiver();
                playerTurn();
                break;
            case 4:
                resetPlayerTurns();
                resetPlayerActions();
                resetPlayerButtons();
                Debug.Log("Game End");
                endGame();
                break;
            default:
                break;
        }
        //turnHandler();
    }

  
    public void playerTurn()
    {
        if (currentPlayer.getPlayerFolded() || currentPlayer.gethasAllIn())
        {
            Debug.Log("Player " + currentPlayer.getPlayerName() + " is Folded/All In, Skipping Turn");
            setNextPlayer(false);          
        }
        else
        {
            currentPlayer.setTurn(true);
            currentPlayer.setButtonsState(true);
            Debug.Log("Player " + currentPlayer.getPlayerName() + " your turn now");
        }
    }

    public void potAddMoney(int amount)
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

    public Sprite[ , ] getCardImages()
    {
        return cardImages;
    }

    public Sprite getBackImage()
    {
        return backImage;
    }

    public bool getIsRaised()
    {
        return raised;
    }

    public int getRaiseMatchAmount()
    {
        return minBet - previousBet;
    }

    public void raise(int amount)
    {
        previousBet = minBet;
        minBet = amount;
        minRaise = 2 * amount;
        for (int i = 0; i < 4; i++)
        {
            playerActions[i].setMinBet(minBet);
        }
    }

    public PlayerActions findSmallBlind()
    {
        PlayerActions smallBlind = playerActions[0];

        for (int i = 0; i < 4; i++)
        {
            if (playerActions[i].getPlayerType() == PlayerType.SMALL_BLIND)
                smallBlind = playerActions[i];
        }  
       return smallBlind;
    }

    public void endGame()
    {
        if (playerActions[0].getIsFolded() && playerActions[1].getIsFolded() && playerActions[2].getIsFolded() && playerActions[3].getIsFolded())
        {
            Debug.Log("No one wins, the pot is taken for the house");
        }
        else
        {
            Card[] playerOneStrongest, playerTwoStrongest, playerThreeStrongest, playerFourStrongest;


            int[] playerStrengths = new int[4];
            int[] strongest = new int[4];
            int[] possibleWinners = { -1 , -1 , -1 , -1 };
            int numOfStrongest = 0;

            currentPlayer = null;
            Card[] cards = new Card[7];
            for (int i = 0; i < 5; i++)
            {
                cards[i] = TH.getCard(i);
            }

            playerOneStrongest = getPlayerStrongestHand(playerActions[0], cards);
            playerTwoStrongest = getPlayerStrongestHand(playerActions[1], cards);
            playerThreeStrongest = getPlayerStrongestHand(playerActions[2], cards);
            playerFourStrongest = getPlayerStrongestHand(playerActions[3], cards);


            playerStrengths[0] = getPlayerHandStrength(playerOneStrongest, playerActions[0].getIsFolded());
            playerStrengths[1] = getPlayerHandStrength(playerTwoStrongest, playerActions[1].getIsFolded());
            playerStrengths[2] = getPlayerHandStrength(playerThreeStrongest, playerActions[2].getIsFolded());
            playerStrengths[3] = getPlayerHandStrength(playerFourStrongest, playerActions[3].getIsFolded());

            strongest[0] = 0;
            for (int i = 0; i < 4; i++)
            {
                if (strongest[0] < playerStrengths[i])
                {
                    if (numOfStrongest > 0)
                    {
                        strongest = new int[4];
                        for (int a = 0; a < 4; a++)
                        {
                            possibleWinners[a] = -1;
                        }
                        numOfStrongest = 0;
                    }
                    strongest[0] = playerStrengths[i];
                    possibleWinners[numOfStrongest] = i;

                }
                else
                    if (strongest[0] == playerStrengths[i])
                {
                    numOfStrongest++;
                    strongest[numOfStrongest] = playerStrengths[i];
                    possibleWinners[numOfStrongest] = i;
                }
            }

            int index = 0;
            Card[,] possibleWinnerHands = new Card[numOfStrongest + 1, 5];
            if (possibleWinners[0] == 0)
            {
                for (int j = 0; j < 5; j++)
                    possibleWinnerHands[index, j] = playerOneStrongest[j];
                index++;
            }
            if (possibleWinners[0] == 1 || possibleWinners[1] == 1)
            {
                for (int j = 0; j < 5; j++)
                    possibleWinnerHands[index, j] = playerTwoStrongest[j];
                index++;
            }
            if (possibleWinners[0] == 2 || possibleWinners[1] == 2 || possibleWinners[2] == 2)
            {
                for (int j = 0; j < 5; j++)
                    possibleWinnerHands[index, j] = playerThreeStrongest[j];
                index++;
            }
            if (possibleWinners[0] == 3 || possibleWinners[1] == 3 || possibleWinners[2] == 3 || possibleWinners[3] == 3)
            {
                for (int j = 0; j < 5; j++)
                    possibleWinnerHands[index, j] = playerFourStrongest[j];
                index++;
            }

            bool allTied = false;
            index = 0;
            int numTied = 0;
            int tieBreaker;
            if (numOfStrongest > 0)
            {

                while (!allTied && numOfStrongest > 0)
                {
                    tieBreaker = getTieBreaker(possibleWinnerHands, strongest[0], index, index + 1);
                    if (tieBreaker == 0)
                    {
                        numTied++;
                        index++;
                    }
                    else
                        if (tieBreaker == 1)
                    {
                        if (numOfStrongest > 1)
                        {
                            for (int m = index; (m < numOfStrongest - 1) && (m < numOfStrongest - index); m++)
                            {
                                for (int k = 0; k < 5; k++)
                                {
                                    possibleWinnerHands[m + 1, k] = possibleWinnerHands[m + 2, k];
                                }
                                possibleWinners[m + 1] = possibleWinners[m + 2];
                                possibleWinners[m + 2] = -1;


                            }
                            index++;
                        }
                        else
                        {
                            possibleWinners[index + 1] = -1;
                        }
                        numOfStrongest--;
                    }
                    else
                    {
                        if (numOfStrongest > 1)
                        {
                            for (int m = index; m < numOfStrongest ; m++)
                            {
                                for (int k = 0; k < 5; k++)
                                {
                                    possibleWinnerHands[m, k] = possibleWinnerHands[m + 1, k];
                                }
                                possibleWinners[m] = possibleWinners[m + 1];
                                possibleWinners[m + 1] = -1;
                            }
                            index++;
                        }
                        else
                        {
                            possibleWinners[index] = possibleWinners[index + 1];
                            possibleWinners[index + 1] = -1;
                        }
                        numOfStrongest--;
                    }
                    if (index == numOfStrongest)
                        index = 0;


                    if (numTied > numOfStrongest)
                        allTied = true;
                }


            }

            if (allTied)
            {

                for (int s = 1; s < numOfStrongest + 1; s++)
                {
                    Debug.Log(playerActions[possibleWinners[s]].getPlayerName() + " is one of the winners and has won: " + pot.getSplitPotValue(numOfStrongest + 1) + " moneys");
                    payOutWinners(playerActions[possibleWinners[s]], numOfStrongest + 1, 0);
                }
                payOutWinners(playerActions[possibleWinners[0]], numOfStrongest + 1, pot.getRemainder());
                Debug.Log(playerActions[possibleWinners[1]].getPlayerName() + " is one of the winners and has won: " + pot.getSplitPotValue(numOfStrongest + 1) + pot.getRemainder() + " moneys");
            }
            else
            {
                Debug.Log(playerActions[possibleWinners[0]].getPlayerName() + " one is the winner and has won: " + pot.getPot() + " moneys");
                payOutWinners(playerActions[possibleWinners[0]], 1, 0);
            }
            /*
                switch (possibleWinners[0])
                {
                    case 0:
                        Debug.Log(playerOneActions.getPlayerName() + " one is the winner and has won: " + pot.getPot() + " moneys");
                        payOutWinners(playerOneActions, 1, 0);
                        break;
                    case 1:
                        Debug.Log(playerTwoActions.getPlayerName() + "Player two is the winner and has won: " + pot.getPot() + " moneys");
                        playerOneActions.addMoney(pot.getPot());
                        break;
                    case 2:
                        Debug.Log(playerThreeActions.getPlayerName() + "Player three is the winner and has won: " + pot.getPot() + " moneys");
                        playerOneActions.addMoney(pot.getPot());
                        break;
                    case 3:
                        Debug.Log(playerFourActions.getPlayerName() + "Player four is the winner and has won: " + pot.getPot() + " moneys");
                        playerOneActions.addMoney(pot.getPot());
                        break;
                    default:
                        break;

                 }*/

            Debug.Log(possibleWinners[0]);
            Debug.Log(possibleWinners[1]);
            Debug.Log(possibleWinners[2]);
            Debug.Log(possibleWinners[3]);


            newGame.interactable = true;
        }
    }

    public void payOutWinners(PlayerActions player, int numOfWinners, int remainder)
    {
        player.addMoney(pot.getSplitPotValue(numOfWinners) + remainder);
    }

    public Card[] getPlayerStrongestHand(PlayerActions player, Card[] cards)
    {
        Card[] newHands = new Card[105];
        int r = 5;
        int n = 7;
        Card[] hand;
        cards[5] = player.getCard(0);
        cards[6] = player.getCard(1);
        GW.generateAllCombinations(cards, newHands, 0, n - 1, 0, r);

        hand = GW.findStrongest().ToArray();

        return hand.ToArray();
    }

    public int getPlayerHandStrength(Card[] hand, bool folded)
    {
        if (folded)
            return 0;
        else
            return GW.getHandValue(hand);
    }

    public int getTieBreaker(Card[,] hands, int handStrength, int firstHand, int secondHand)
    {
        Card[] hand1 = new Card[5];
        Card[] hand2 = new Card[5];
        for (int i = 0; i < 5; i++)
        {
            hand1[i] = hands[firstHand, i];
            hand2[i] = hands[secondHand, i];
        }

        return GW.isTie(hand1, hand2, handStrength);
    }
}
