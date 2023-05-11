using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private Card[] hand = new Card[2];
    [SerializeField]
    Text display;
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

        showCards();
    }

    public Card getCard(int num)
    {
        return hand[num];
    }

    public void assignTable(TableHand newTH)
    {
        TH = newTH;
    }

    public void showCards()
    {
        hand[0].clearDisplay(display);
        hand[0].displayCard(display, hand[0]);
        hand[1].displayCard(display, hand[1]);
    }

}
