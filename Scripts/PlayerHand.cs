using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private Card[] hand = new Card[2];
    TableHand TH;

    public void drawCards()
    {

        int numCardsInPlay = TH.getNumCardsInPlay();
        Card[] cardsInPlay = new Card[numCardsInPlay];
        for (int i = 0; i < numCardsInPlay; i++)
        {
            cardsInPlay[i] = TH.getCardInPlay(i);
        }

        for (int i = 0; i < 2; i++)
        {
            hand[i].drawCard(cardsInPlay, numCardsInPlay);
            TH.addCard(hand[i]);
        }
    }

    public Card getCard(int num)
    {
        return hand[num];
    }

    public void assignTable(TableHand newTH)
    {
        TH = newTH;
    }

}
