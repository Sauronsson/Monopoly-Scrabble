using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    public Transform player1;
    public Transform player2;
    public Transform player3;
    public Transform player4;
    private Transform[] playerArray;

    //To keep track of which part of the turn you're on
    int turnTracker = 0;
    //To determine who's turn it is
    int currentPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerArray = new Transform[] {player1, player2, player3, player4};
    }

    // Update is called once per frame
    void Update()
    {
        switch (turnTracker) {
            case 0:
                turnTracker++;
                break;
            
            //Move player character to next location. Keep moving until no doubles.
            case 1:
                break;
                

            //move to next player, reset turn tracker
            default:
                if (currentPlayer == 3) {
                    currentPlayer = 0;
                } else {
                    currentPlayer = currentPlayer + 1;
                }
                turnTracker = 0;
                break;
        }
    }
}
