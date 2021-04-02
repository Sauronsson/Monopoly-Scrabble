using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    private int amountOfPlayers = 1;
    private GameObject[] playerArray;
    private SteppingStones[] playerMovement;
    private PlayerData[] playerData;
    private int sameRollCounter = 0;

    //data for main to keep track of information.
    private int startingPosition;


    //To keep track of which part of the turn you're on
    int turnTracker = 0;
    //To determine who's turn it is
    int currentPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerArray = new GameObject[] { player1, player2, player3, player4 };

        //get movement scripts
        SteppingStones p1MoveScript = player1.GetComponent<SteppingStones>() as SteppingStones;
        SteppingStones p2MoveScript = player2.GetComponent<SteppingStones>() as SteppingStones;
        SteppingStones p3MoveScript = player3.GetComponent<SteppingStones>() as SteppingStones;
        SteppingStones p4MoveScript = player4.GetComponent<SteppingStones>() as SteppingStones;
        PlayerData p1DataScript = player1.GetComponent<PlayerData>() as PlayerData;
        PlayerData p2DataScript = player2.GetComponent<PlayerData>() as PlayerData;
        PlayerData p3DataScript = player3.GetComponent<PlayerData>() as PlayerData;
        PlayerData p4DataScript = player4.GetComponent<PlayerData>() as PlayerData;

        //Player Specific Scripts
        playerMovement = new SteppingStones[] { p1MoveScript, p2MoveScript, p3MoveScript, p4MoveScript };
        playerData = new PlayerData[] { p1DataScript, p2DataScript, p3DataScript, p4DataScript };
    }

    // Update is called once per frame
    void Update()
    {
        switch (turnTracker) {

            //beginning function, tbd on use
            case 0:
                turnTracker++;
                break;

            //Move player character to next location. Keep moving until no doubles. on 3 doubles send to jail
            case 1:
                if (Input.GetKeyDown(KeyCode.Space) && !playerMovement[currentPlayer].isMoving)
                {
                    //Starting Movement information
                    playerMovement[currentPlayer].roll();
                    if (sameRollCounter == 0)
                    {
                        startingPosition = playerMovement[currentPlayer].routePosition;
                    }

                    //Move Object based off roll
                    if (playerMovement[currentPlayer].permsteps1 != playerMovement[currentPlayer].permsteps2)
                    {
                        playerMovement[currentPlayer].goForwards();
                        turnTracker++;
                    } else {
                        //rolls snake eyes, go to jail
                        if (playerMovement[currentPlayer].permsteps1 == 1)
                        {
                            goToJail(currentPlayer);
                            turnTracker++;
                        }
                        //rolls doubles 3 times, go to jail
                        else if (sameRollCounter == 2)
                        {
                            goToJail(currentPlayer);
                            turnTracker++;
                        }
                        else
                        {
                            playerMovement[currentPlayer].goForwards();
                            sameRollCounter++;
                        }
                    }
                }
                break;

            //Do whatever board says
            case 2:
                if (!playerMovement[currentPlayer].isMoving)
                {
                    //update cash from previous turn
                    if (playerData[currentPlayer].passedGo)
                    {
                        updateCash(currentPlayer, 200);
                        playerData[currentPlayer].passedGo = false;
                    }
                    determineBoardEffect();
                    turnTracker++;
                }
                break;

            //move to next player, reset turn tracker
            default:
                if (currentPlayer+1 == amountOfPlayers) {
                    currentPlayer = 0;
                } else {
                    currentPlayer = currentPlayer + 1;
                }
                turnTracker = 0;
                sameRollCounter = 0;
                break;
        }
    }

    //helper method, to ensure that additional data files are changed to reflect jail.
    private bool goToJail(int currentPlayer)
    {
        playerMovement[currentPlayer].goToJail();
        return true;
    }

    //helper method, to take care of extra effects from changing cash, like bankruptcy
    private bool updateCash(int currentPlayer, int amount) {
        playerData[currentPlayer].updateCash(amount);
        return true;
    }

    private void determineBoardEffect(){
        //activate effects from landing on tile
        switch (playerMovement[currentPlayer].routePosition)
        {
            case 0: //GO
                break;

            case 4: //INCOME TAX
                updateCash(currentPlayer, -200);
                break;

            case 10: //VISITING JAIL
                break;

            case 21: //FREE PARKING
                break;

            case 31: //GO TO JAIL
                goToJail(currentPlayer);
                break;

            case 39: //LUXURY TAX
                updateCash(currentPlayer, -100);
                break;
        }
    }
}

