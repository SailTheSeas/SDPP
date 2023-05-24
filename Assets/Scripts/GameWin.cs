using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System.Linq;

public class GameWin : MonoBehaviour
{
    private Card[ , ] allCombs = new Card[21, 5];
    int row = 0;

    //Decide which hand is bigger between two player hands of same value
    //Returns a one if its the first player, 2 if its the second player
    //and returns a zero if it is a tie
    public int isTie(Card[] hand1, Card[] hand2, int handStrength)
    {
        bool handOneStronger = false, handTwoStronger = false;
        bool isEqual = true;
        hand1 = sortHand(hand1);
        hand2 = sortHand(hand2);
        Card[] strongestHand;
        
        strongestHand = findStrongerHand(hand1, hand2, handStrength);
        for (int i = 0; i < 5; i++)
        {
            if (!(strongestHand[i].getCardValue() == hand1[i].getCardValue() && strongestHand[i].getCardSuit() == hand1[i].getCardSuit()))
                isEqual = false;

        }

        if (isEqual)
            handOneStronger = true;

        isEqual = true;
        strongestHand = findStrongerHand(hand2, hand1, handStrength);
        for (int i = 0; i < 5; i++)
        {
            if (!(strongestHand[i].getCardValue() == hand2[i].getCardValue() && strongestHand[i].getCardSuit() == hand2[i].getCardSuit()))
                isEqual = false;

        }

        if (isEqual)
            handTwoStronger = true;

        if (handOneStronger && handTwoStronger)
            return 0;
        else
            if (handOneStronger)
                return 1;
            else
                return 2;
    }

    public void testHands(Text display)
    {
        Card[] theStrong = findStrongest();
        for (int i = 0; i < 5; i++)
        {
            display.text += theStrong[i].getCardValue() + " " + theStrong[i].getCardSuit().ToString() + "  ";
        }

    }

    public Card[] findStrongest()
    {
        row = 0;
        Card[] strongestHand = new Card[5];
        Card[] temp = new Card[5];
        int handStrength, strongestHandStrength = 0;
        for (int i = 0; i < 21; i++)
        {
            for (int l = 0; l < 5; l++)
            {
                temp[l] = allCombs[i, l];
            }
            handStrength = getHandValue(temp);
            if (handStrength == strongestHandStrength)
            {
                strongestHand = findStrongerHand(strongestHand, temp, handStrength);
            }
            else
                if (handStrength > strongestHandStrength)
                {
                    strongestHandStrength = handStrength;
                    strongestHand = temp.ToArray();
                }
        }
        return strongestHand.ToArray();
    }

    public void generateAllCombinations(Card[] hand, Card[] data, int start, int end, int index, int r)
    {
        if (index == r)
        {
            loadHands(data);
        }
            

        for (int i = start; (i <= end) && (end - i + 1 >= r - index); i++)
        {
            data[index] = hand[i];
            generateAllCombinations(hand, data, i + 1, end, index + 1, r);
        }


    }

    public void loadHands(Card[] hands)
    {
        for (int i = 0; i < 5; i++)
        {
            allCombs[row, i] = hands[i];
        }
        row++;
    }

    public void displayHands(Text display)
    {
        display.text = "";
        for (int i = 0; i < 21; i++)
        {
            for (int l = 0; l < 5; l++)
            {
                display.text += allCombs[i, l].getCardValue() + " " + allCombs[i, l].getCardSuit().ToString() + "  ";
            }
            display.text += "\n";
        }
    }

    public Card[] sortHand(Card[] hand)
    {
        Card temp;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5 - i - 1; j++)
            {
                if (hand[j].getCardValue() > hand[j+1].getCardValue())
                {
                    temp = hand[j];
                    hand[j] = hand[j + 1];
                    hand[j+1] = temp;
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

    public int getHandValue(Card[] hand)
    {
        hand = sortHand(hand);
        int value;
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
        for (int x = 0; x < 13; x++)
            matchingFaces[x] = 0;
        int numOfTwoKind = 0;
        int numOfThreeKind = 0;

        bool fourOfKind = false;
        bool allSameSuit = false;
        bool consNums;
        bool fiveRoyals = false;

        int num;
        for (int i = 0; i < 5; i++)
        {
            num = hand[i].getCardValue();
            matchingFaces[num-1]++;

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
                            if (allSameSuit == true && fiveRoyals == true)
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

    //Card to decide between two hands of the same strength
    //In the case of a tie between the hands, the inital hand is kept
    //If they tie, then whichever one is kept is unimportant
    public Card[] findStrongerHand(Card[] strongHand, Card[] hand, int handStrength)
    {
        Card[] strongestHand;
        hand = sortHand(hand);
        strongHand = sortHand(strongHand);
        int strongHandPair, handPair;
        switch (handStrength)
        {
            //High Card, we find the compare each of the cards from highest to lowest
            //First hand with a higher card wins, otherwise tie and stronghand is kept
            //Ace counts as higher card here
            case 1:
                strongestHand = findBiggestHighCard(strongHand, hand);
                return strongestHand.ToArray();
            //One Pair, we find the pairs and compare them
            //Take highest pair of the two
            //If a tie, compar the rest of hand
            case 2:
                strongHandPair = findStrongestPair(strongHand);
                handPair = findStrongestPair(hand);
                if (strongHandPair == handPair)
                    strongestHand = findBiggestHighCard(strongHand, hand);
                else
                    if (strongHandPair > handPair)
                        strongestHand = strongHand.ToArray();
                    else
                        strongestHand = hand.ToArray();
                return strongestHand.ToArray();
            //Two Pair, same as one pair
            //Except we compare the highest pair, then the next pair if a tie
            //And if its another tie, we compare the last card
            case 3:
                strongHandPair = findStrongestPair(strongHand);
                handPair = findStrongestPair(hand);
                if (strongHandPair == handPair)
                {
                    strongHandPair = findSecondStrongestPair(strongHand);
                    handPair = findSecondStrongestPair(hand);
                    if (strongHandPair == handPair)
                        strongestHand = findBiggestHighCard(strongHand, hand);
                    else
                        if (strongHandPair > handPair)
                            strongestHand = strongHand.ToArray();
                        else
                            strongestHand = hand.ToArray();
                }
                else
                    if (strongHandPair > handPair)
                        strongestHand = strongHand.ToArray();
                    else
                        strongestHand = hand.ToArray();
                return strongestHand.ToArray();
            //Three of a kind same concept as one pair
            case 4:
                strongHandPair = findStrongestPair(strongHand);
                handPair = findStrongestPair(hand);
                if (strongHandPair == handPair)
                    strongestHand = findBiggestHighCard(strongHand, hand);
                else
                    if (strongHandPair > handPair)
                    strongestHand = strongHand.ToArray();
                else
                    strongestHand = hand.ToArray();
                return strongestHand.ToArray();
            //Straight, compare cards same as high card
            //However, ace counts as one in a straight
            //Save for a royal flush, but that is a different case
            case 5:
                strongestHand = findBiggestStraight(strongHand, hand);
                return strongestHand.ToArray();
            //Flush, same concept as high card
            case 6:
                strongestHand = findBiggestHighCard(strongHand, hand);
                return strongestHand.ToArray();
            //Full House, first we compare the three pair
            //If its a tie we compare the two pair, but this can be done same as high card
            case 7:
                strongHandPair = findThreePairFullHouse(strongHand);
                handPair = findThreePairFullHouse(hand);
                if (strongHandPair == handPair)
                    strongestHand = findBiggestHighCard(strongHand, hand);
                else
                    if (strongHandPair > handPair)
                    strongestHand = strongHand.ToArray();
                else
                    strongestHand = hand.ToArray();
                return strongestHand.ToArray();
            //Four of a Kind, same concept as three of a kind and one pair 
            case 8:
                strongHandPair = findStrongestPair(strongHand);
                handPair = findStrongestPair(hand);
                if (strongHandPair == handPair)
                    strongestHand = findBiggestHighCard(strongHand, hand);
                else
                    if (strongHandPair > handPair)
                    strongestHand = strongHand.ToArray();
                else
                    strongestHand = hand.ToArray();
                return strongestHand.ToArray();
            //Straight flush, same concept as a straight
            case 9:
                strongestHand = findBiggestStraight(strongHand, hand);
                return strongestHand.ToArray();
            //Royal Flush, highest hand, can only tie with itself
            case 10:
                return strongHand.ToArray();               
            default:
                return strongHand.ToArray();
        }
    }
    public int findStrongestPair(Card[] hand)
    {
        int num = 0;
        for (int i = 1; i < 4; i++)
        {
            if (hand[i].getCardValue() == hand[i + 1].getCardValue() || hand[i].getCardValue() == hand[i - 1].getCardValue())
                if (hand[i].getCardValue() > num && num != 1)
                    num = hand[i].getCardValue();
        }
        return num;
    }

    public int findSecondStrongestPair(Card[] hand)
    {
        int strongestPair = findStrongestPair(hand);
        int num = 0;
        for (int i = 1; i < 4; i++)
        {
            if (hand[i].getCardValue() == hand[i + 1].getCardValue() || hand[i].getCardValue() == hand[i - 1].getCardValue())
                if (hand[i].getCardValue() > num && (hand[i].getCardValue() < strongestPair || strongestPair == 1))
                    num = hand[i].getCardValue();
        }
        return num;
    }


    public Card[] findBiggestHighCard(Card[] hand1, Card[] hand2)
    {

        if (hand1[0].getCardValue() == 1 && hand1[0].getCardValue() < hand2[0].getCardValue())
            return hand1.ToArray();

        if (hand2[0].getCardValue() == 1 && hand1[0].getCardValue() > hand2[0].getCardValue())
            return hand2.ToArray();

        for (int i = 4; i >= 0; i--)
        {
            if (hand1[i].getCardValue() != hand2[i].getCardValue())
                if (hand1[i].getCardValue() > hand2[i].getCardValue())
                    return hand1.ToArray();
                else
                    return hand2.ToArray();
        }

        return hand1.ToArray();
    }

    public Card[] findBiggestStraight(Card[] hand1, Card[] hand2)
    {
        for (int i = 4; i >= 0; i--)
        {
            if (hand1[i].getCardValue() != hand2[i].getCardValue())
                if (hand1[i].getCardValue() > hand2[i].getCardValue())
                    return hand1.ToArray();
                else
                    return hand2.ToArray();
        }

        return hand1.ToArray();
    }

    public int findThreePairFullHouse(Card[] hand)
    {
        int num = 0;
        for (int i = 1; i < 4; i++)
        {
            if (hand[i].getCardValue() == hand[i + 1].getCardValue() && hand[i].getCardValue() == hand[i - 1].getCardValue())
                num = hand[i].getCardValue();
        }
        return num;
    }

}
