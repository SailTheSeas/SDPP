using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCardDisplay : MonoBehaviour
{
    [SerializeField]
    GameWin GW;
    Card[] cards = new Card[7];
    // Start is called before the first frame update
    void Start()
    {/*
        Passed
        cards[0].setCardSuit(CardSuit.HEARTS);
        cards[0].setCardValue(1);
        cards[1].setCardSuit(CardSuit.HEARTS);
        cards[1].setCardValue(2);
        cards[2].setCardSuit(CardSuit.HEARTS);
        cards[2].setCardValue(3);
        cards[3].setCardSuit(CardSuit.HEARTS);
        cards[3].setCardValue(4);
        cards[4].setCardSuit(CardSuit.HEARTS);
        cards[4].setCardValue(5);
        cards[5].setCardSuit(CardSuit.HEARTS);
        cards[5].setCardValue(6);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(7);
        
        Passed
        cards[0].setCardSuit(CardSuit.HEARTS);
        cards[0].setCardValue(10);
        cards[1].setCardSuit(CardSuit.HEARTS);
        cards[1].setCardValue(9);
        cards[2].setCardSuit(CardSuit.HEARTS);
        cards[2].setCardValue(12);
        cards[3].setCardSuit(CardSuit.HEARTS);
        cards[3].setCardValue(10);
        cards[4].setCardSuit(CardSuit.HEARTS);
        cards[4].setCardValue(11);
        cards[5].setCardSuit(CardSuit.HEARTS);
        cards[5].setCardValue(13);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(2);
        
        Passed
        cards[0].setCardSuit(CardSuit.DIAMONDS);
        cards[0].setCardValue(12);
        cards[1].setCardSuit(CardSuit.HEARTS);
        cards[1].setCardValue(9);
        cards[2].setCardSuit(CardSuit.HEARTS);
        cards[2].setCardValue(12);
        cards[3].setCardSuit(CardSuit.HEARTS);
        cards[3].setCardValue(10);
        cards[4].setCardSuit(CardSuit.HEARTS);
        cards[4].setCardValue(11);
        cards[5].setCardSuit(CardSuit.SPADES);
        cards[5].setCardValue(13);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(2);
        

        Passed
        cards[0].setCardSuit(CardSuit.DIAMONDS);
        cards[0].setCardValue(12);
        cards[1].setCardSuit(CardSuit.HEARTS);
        cards[1].setCardValue(9);
        cards[2].setCardSuit(CardSuit.HEARTS);
        cards[2].setCardValue(12);
        cards[3].setCardSuit(CardSuit.CLUBS);
        cards[3].setCardValue(10);
        cards[4].setCardSuit(CardSuit.HEARTS);
        cards[4].setCardValue(11);
        cards[5].setCardSuit(CardSuit.SPADES);
        cards[5].setCardValue(13);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(2);
        
        Passed
        cards[0].setCardSuit(CardSuit.DIAMONDS);
        cards[0].setCardValue(12);
        cards[1].setCardSuit(CardSuit.HEARTS);
        cards[1].setCardValue(9);
        cards[2].setCardSuit(CardSuit.HEARTS);
        cards[2].setCardValue(12);
        cards[3].setCardSuit(CardSuit.HEARTS);
        cards[3].setCardValue(7);
        cards[4].setCardSuit(CardSuit.HEARTS);
        cards[4].setCardValue(11);
        cards[5].setCardSuit(CardSuit.SPADES);
        cards[5].setCardValue(13);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(2);
        

        Passed
        cards[0].setCardSuit(CardSuit.DIAMONDS);
        cards[0].setCardValue(12);
        cards[1].setCardSuit(CardSuit.SPADES);
        cards[1].setCardValue(9);
        cards[2].setCardSuit(CardSuit.CLUBS);
        cards[2].setCardValue(12);
        cards[3].setCardSuit(CardSuit.HEARTS);
        cards[3].setCardValue(7);
        cards[4].setCardSuit(CardSuit.HEARTS);
        cards[4].setCardValue(11);
        cards[5].setCardSuit(CardSuit.SPADES);
        cards[5].setCardValue(13);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(2);
        
        Passed
        cards[0].setCardSuit(CardSuit.DIAMONDS);
        cards[0].setCardValue(12);
        cards[1].setCardSuit(CardSuit.DIAMONDS);
        cards[1].setCardValue(4);
        cards[2].setCardSuit(CardSuit.CLUBS);
        cards[2].setCardValue(13);
        cards[3].setCardSuit(CardSuit.HEARTS);
        cards[3].setCardValue(7);
        cards[4].setCardSuit(CardSuit.CLUBS);
        cards[4].setCardValue(4);
        cards[5].setCardSuit(CardSuit.SPADES);
        cards[5].setCardValue(4);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(4);
       
        Passed
        cards[0].setCardSuit(CardSuit.DIAMONDS);
        cards[0].setCardValue(12);
        cards[1].setCardSuit(CardSuit.DIAMONDS);
        cards[1].setCardValue(11);
        cards[2].setCardSuit(CardSuit.CLUBS);
        cards[2].setCardValue(11);
        cards[3].setCardSuit(CardSuit.HEARTS);
        cards[3].setCardValue(12);
        cards[4].setCardSuit(CardSuit.CLUBS);
        cards[4].setCardValue(4);
        cards[5].setCardSuit(CardSuit.SPADES);
        cards[5].setCardValue(4);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(3);
        

        Passed
        cards[0].setCardSuit(CardSuit.DIAMONDS);
        cards[0].setCardValue(1);
        cards[1].setCardSuit(CardSuit.DIAMONDS);
        cards[1].setCardValue(2);
        cards[2].setCardSuit(CardSuit.DIAMONDS);
        cards[2].setCardValue(3);
        cards[3].setCardSuit(CardSuit.DIAMONDS);
        cards[3].setCardValue(4);
        cards[4].setCardSuit(CardSuit.DIAMONDS);
        cards[4].setCardValue(5);
        cards[5].setCardSuit(CardSuit.DIAMONDS);
        cards[5].setCardValue(6);
        cards[6].setCardSuit(CardSuit.HEARTS);
        cards[6].setCardValue(7);
        */
    }

    // Update is called once per frame
    public void displayCards(Text display)
    {
        //int r = 5;
        //int n = 7;
        //Card[] newHands = new Card[105];
        //GW.generateAllCombinations(cards, newHands, 0, n - 1, 0, r, display);
        //newHands = GW.generateAllCombinations(cards, newHands, 0, n - 1, 0, r);
    }

}
