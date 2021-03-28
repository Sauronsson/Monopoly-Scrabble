using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    private GameObject[] playerArray;
    private SteppingStones[] playerMovement;
    private int sameRollCounter = 0;

    //To keep track of which part of the turn you're on
    int turnTracker = 0;
    //To determine who's turn it is
    int currentPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerArray = new GameObject[] {player1, player2, player3, player4};

        //get movement scripts
        SteppingStones p1MoveScript = player1.GetComponent<SteppingStones>() as SteppingStones;
        SteppingStones p2MoveScript = player2.GetComponent<SteppingStones>() as SteppingStones;
        SteppingStones p3MoveScript = player3.GetComponent<SteppingStones>() as SteppingStones;
        SteppingStones p4MoveScript = player4.GetComponent<SteppingStones>() as SteppingStones;
        playerMovement = new SteppingStones[] { p1MoveScript, p2MoveScript, p3MoveScript, p4MoveScript };
    }

    // Update is called once per frame
    void Update()
    {
        switch (turnTracker) {
            case 0:
                turnTracker++;
                break;
            
            //Move player character to next location. Keep moving until no doubles. on 3 doubles send to jail
            case 1:
                if (Input.GetKeyDown(KeyCode.Space) && !playerMovement[currentPlayer].isMoving )
                {
                    //SteppingStones p1MoveScript = player1.GetComponent<SteppingStones>() as SteppingStones;
                    playerMovement[currentPlayer].roll();
                    //go to next phase of turn once player doesn't roll a double
                    if (playerMovement[currentPlayer].permsteps1 != playerMovement[currentPlayer].permsteps2)
                    {
                        turnTracker++;
                    } else {
                        sameRollCounter++;
                    }
                }
                break;

            //move to next player, reset turn tracker
            default:
                if (currentPlayer == 3) {
                    currentPlayer = 0;
                } else {
                    currentPlayer = currentPlayer + 1;
                }
                turnTracker = 0;
                sameRollCounter = 0;
                break;
        }
    }
}
