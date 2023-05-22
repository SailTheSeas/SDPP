using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableHand : MonoBehaviour
{
    [SerializeField]
    Text display;
    private Card[] hand = new Card[5];
    private Card[] cardsInPlay = new Card[13];
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

    public void newRound()
    {
        hand = new Card[5];
        cardsInPlay = new Card[13];
        numCardsInPlay = 0;
    }

    public void showFlop()
    {
        clearDisplay();
        hand[0].displayCard(display, hand[0]);
        hand[1].displayCard(display, hand[1]);
        hand[2].displayCard(display, hand[2]);
    }

    public void showTurn()
    {
        hand[3].displayCard(display, hand[3]);
    }

    public void showRiver()
    {
        hand[4].displayCard(display, hand[4]);
    }

    public void clearDisplay()
    {
        hand[0].clearDisplay(display);
    }
}
