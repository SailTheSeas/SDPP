//Struct for the card. Includeds the face and value of the card
//Includes a function to generate a card.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Card
{

    private CardSuit suit;
    private int value;
    
    CardSuit getCardSuit()
    {
        return suit;
    }

    int getCardValue()
    {
        return value;
    }

    //Generates a card and compares it against the cards that are already in play.
    //X holds how many cards are in play
    //This includes the two cards of each player
    //As well as the other cards already drawn for the table
    public void drawCard(Card[] drawnCards, int x)
    {
        int randomFace;
        randomFace = Random.Range(0, 5);
        value = Random.Range(0, 14);
        switch (randomFace)
        {
            case 1:  
                suit = CardSuit.HEARTS;
                break;
            case 2:
                suit = CardSuit.CLUBS;
                break;
            case 3:
                suit = CardSuit.SPADES;
                break;
            case 4:
                suit = CardSuit.DIAMONDS;
                break;
            default:
                suit = CardSuit.DIAMONDS;
                break;
        }

        bool unique = true;
        for (int i = 0; i < x; i++)
        {
            if (suit == drawnCards[i].suit)
            {
                if (value == drawnCards[i].value)
                    unique = false;
            }
        }

        if (unique)
        {
        }
        else
            drawCard(drawnCards, x);
    }
}
