using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testHands(Card[] hand)
    {
        Card[] strongestHand = new Card[5];

        strongestHand = findStrongest(hand, 0, 7, 0, 5);
    }

    public Card[] findStrongest(Card[] hand, int begin, int end, int currentCycle, int handSize)
    {
        Card[] strongestHand = new Card[handSize];
        int handStrength = 0;
        if (currentCycle == handSize)
        {
            return strongestHand;
        }

        for (int i = begin; (i <= end) && (end - i + 1) >= (handSize - currentCycle); i++)
        {
            //handStrength = getHandValue()
        }
        return hand;
    }

    public Card[] sortHand(Card[] hand)
    {
        Card temp;
        for (int i = 1; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (hand[i].getCardValue() > hand[j+1].getCardValue())
                {
                    temp = hand[i];
                    hand[i] = hand[i + 1];
                    hand[i+1] = temp;
                }
            }
        }


        return hand;
    }

    public bool isConsecutive(Card[] hand)
    {
        for (int i = 1; i < 5; i++)
        {
            if (hand[i].getCardValue() != hand[i-1].getCardValue()+1)
            {
                return false;
            }
        }
        return true;
    }

    public bool isConsecutiveEXC(Card[] hand)
    {
        for (int i = 2; i < 5; i++)
        {
            if (hand[i].getCardValue() != hand[i - 1].getCardValue() + 1)
            {
                return false;
            }
        }
        return true;
    }

    public int getHandValue(Card[] hand)
    {
        hand = sortHand(hand);
        int value = 0;
        //Arrays to store how many times a condition is met, split by faces
        //0-hearts, 1-Clubs, 2-Spades, 3-Diamonds
        int[] sameSuit = new int[4];
        int royals = 0;
        for (int y = 0; y < 4; y++)
        {
            sameSuit[y] = 0;
        }

        //Array to increment each number found.
        int[] matchingFaces = new int[13];
        for (int x = 0; x < 14; x++)
            matchingFaces[x] = 0;
        int numOfTwoKind = 0;
        int numOfThreeKind = 0;

        bool fourOfKind = false;
        bool allSameSuit = false;
        bool consNums = false;
        bool fiveRoyals = false;

        int num;
        for (int i = 0; i < 5; i++)
        {
            num = hand[i].getCardValue();
            matchingFaces[num]++;

            if (num == 0 || num > 9)
            {
                royals++;
            }

            switch (hand[i].getCardSuit())
            {
                case CardSuit.HEARTS:
                    sameSuit[0]++;
                    break;
                case CardSuit.CLUBS:
                    sameSuit[1]++;
                    break;
                case CardSuit.SPADES:
                    sameSuit[2]++;
                    break;
                case CardSuit.DIAMONDS:
                    sameSuit[3]++;
                    break;
                default:
                    break;
            }



        }

        for (int z = 0; z < 13; z++)
        {
            if (matchingFaces[z] == 2)
                numOfTwoKind++;
            if (matchingFaces[z] == 3)
                numOfThreeKind++;
            if (matchingFaces[z] == 4)
                fourOfKind = true;
        }

        for (int h = 0; h < 4; h++)
        {
            if (sameSuit[h] == 5)
                allSameSuit = true;
        }

        if (royals == 5)
            fiveRoyals = true;

        if (hand[0].getCardValue() == 1)
            consNums = isConsecutiveEXC(hand);
        else
            consNums = isConsecutive(hand);

        if (numOfTwoKind == 1 && numOfThreeKind == 1)
            value = 7;
        else
            if (numOfTwoKind == 1)
                value = 2;
            else
                if (numOfTwoKind == 2)
                    value = 3;
                else
                    if (numOfThreeKind == 1)
                        value = 4;
                    else 
                        if (fourOfKind)
                            value = 8;
                        else
                            if (consNums == true && allSameSuit == true && fiveRoyals == true)
                                value = 10;
                            else
                                if (consNums == true && allSameSuit == true)
                                    value = 9;
                                else
                                    if (allSameSuit)
                                        value = 6;
                                    else 
                                        if (consNums)
                                            value = 5;
                                        else
                                            value = 1;
         
                

        return value;
    }
}
