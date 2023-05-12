using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject playerOne, playerTwo, playerThree, playerFour;
    [SerializeField]
    PlayerHand playerOneHand, playerTwoHand, playerThreeHand, playerFourHand;
    [SerializeField]
    TableHand TH;
    [SerializeField]
    Pot pot;

    private PlayerType playerOneType, playerTwoType, playerThreeType, playerFourType;
    void Start()
    {
        playerOneType = PlayerType.Dealer;
        playerTwoType = PlayerType.Small_Bluff;
        playerThreeType = PlayerType.Big_Bluff;
        playerFourType = PlayerType.None;
        playerOneHand = playerOne.GetComponent<PlayerHand>();
        playerTwoHand = playerTwo.GetComponent<PlayerHand>();
        playerThreeHand = playerThree.GetComponent<PlayerHand>();
        playerFourHand = playerFour.GetComponent<PlayerHand>();
        TH = this.GetComponent<TableHand>();
        pot = this.GetComponent<Pot>();

        playerOneHand.assignTable(TH);
        playerTwoHand.assignTable(TH);
        playerThreeHand.assignTable(TH);
        playerFourHand.assignTable(TH);
    }

    public void handOutCards()
    {
        playerOneHand.drawCards();
        playerTwoHand.drawCards();
        playerThreeHand.drawCards();
        playerFourHand.drawCards();
        Debug.Log("Cards Handed Out");
    }

    public void handOutTable()
    {
        TH.drawHand();
        Debug.Log("Table Handed Out");
    }
    
}
