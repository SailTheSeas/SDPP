using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class TableHand : MonoBehaviour
{
    private Card[] hand = new Card[5];
    private Card[] cardsInPlay = new Card[13];
    [SerializeField]
    Image[] tableCards = new Image[5];
    Sprite[,] cardImages = new Sprite[4, 13];
    Sprite backImage;
    private int numCardsInPlay = 0;

    public void drawHand()
    {    
        for (int i = 0; i < 5; i++)
        {
            hand[i].drawCard(cardsInPlay,numCardsInPlay);
            addCard(hand[i]);
        }

    }

    public void reshuffle()
    {
        numCardsInPlay = 0;
    }

    public Card getCard(int num)
    {
        return hand[num];
    }

    public void addCard(Card newCard)
    {
        cardsInPlay[numCardsInPlay] = newCard;
        numCardsInPlay++; ;
    }

    public Card getCardInPlay(int num)
    {
        return cardsInPlay[num];
    }

    public int getNumCardsInPlay()
    {
        return numCardsInPlay;
    }

    /*[ClientRpc]*/public void setCardImages(Sprite[,] newCardImages, Sprite newBackImage)
    {
        cardImages = newCardImages;
        backImage = newBackImage;
    }

    public void newRound()
    {
        hand = new Card[5];
        cardsInPlay = new Card[13];
        numCardsInPlay = 0;
    }

    public void showFlop()
    {
        clearDisplay();
        hand[0].displayCard(tableCards[0], hand[0], cardImages);
        hand[1].displayCard(tableCards[1], hand[1], cardImages);
        hand[2].displayCard(tableCards[2], hand[2], cardImages);
    }

    public void showTurn()
    {
        hand[3].displayCard(tableCards[3], hand[3], cardImages);
    }

    public void showRiver()
    {
        hand[4].displayCard(tableCards[4], hand[4], cardImages);
    }

    public void clearDisplay()
    {
        hand[0].clearDisplay(tableCards[0], backImage);
        hand[0].clearDisplay(tableCards[1], backImage);
        hand[0].clearDisplay(tableCards[2], backImage);
        hand[0].clearDisplay(tableCards[3], backImage);
        hand[0].clearDisplay(tableCards[4], backImage);

    }
}
