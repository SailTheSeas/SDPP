using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableHand : MonoBehaviour
{
    private Card[] hand = new Card[5];
    private Card[] cardsInPlay = new Card[13];
    private int numCardsInPlay = 0;

    void initialize()
    {
    }
    public void drawHand()
    {    
        for (int i = 0; i < 5; i++)
        {
            hand[i].drawCard(cardsInPlay,numCardsInPlay);
            addCard(hand[i]);
        }
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
}
