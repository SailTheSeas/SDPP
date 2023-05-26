using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]
    private Card[] hand = new Card[2];
    [SerializeField]
    Image cardOne, cardTwo;
    [SerializeField]
    Sprite[,] cardImages = new Sprite[4, 13];
    Sprite backImage;
    PlayerActions PA;
    [SerializeField]
    //TableHand TH;

    /*[Command (requiresAuthority=false)]*/public void drawCards()
    {

        int numCardsInPlay = PA.getNumOfCardsInPlay();
        Card[] cardsInPlay = new Card[numCardsInPlay];
        for (int i = 0; i < numCardsInPlay; i++)
        {
            cardsInPlay[i] = PA.getCardInPlay(i);
        }

        for (int i = 0; i < 2; i++)
        {
            hand[i].drawCard(cardsInPlay, numCardsInPlay);
            PA.addCard(hand[i]);
        }

        //showCards();
    }
    private void Awake()
    {

        cardOne = this.transform.parent.GetComponentInParent<Transform>().GetChild(1).GetChild(0).GetChild(4).GetComponent<Image>();
        cardTwo = this.transform.parent.GetComponentInParent<Transform>().GetChild(1).GetChild(0).GetChild(5).GetComponent<Image>();
    }

    public void assignPlayer(PlayerActions newPA)
    {
        PA = newPA;
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


   public void showCards()
    {
        clearDisplay();
        hand[0].displayCard(cardOne, hand[0], cardImages);
        hand[0].displayCard(cardTwo, hand[1], cardImages);
    }

    public void clearDisplay()
    {
        hand[0].clearDisplay(cardOne, backImage);
        hand[0].clearDisplay(cardTwo, backImage);
    }

}
