using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private Card[] hand = new Card[2];
    [SerializeField]
    Text display;
    [SerializeField]
    Image cardOne, cardTwo;
    Sprite[,] cardImages = new Sprite[4, 13];
    Sprite backImage;
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

    public void setCardImages(Sprite[,] newCardImages, Sprite newBackImage)
    {
        cardImages = newCardImages;
        backImage = newBackImage;
    }


    public void assignTable(TableHand newTH)
    {
        TH = newTH;
    }

    public void showCards()
    {
        clearDisplay();
        hand[0].displayCard(display, cardOne, hand[0], cardImages);
        hand[1].displayCard(display, cardTwo, hand[1], cardImages);
    }

    public void clearDisplay()
    {
        hand[0].clearDisplay(display, cardOne, backImage);
        hand[0].clearDisplay(display, cardTwo, backImage);
    }

}
