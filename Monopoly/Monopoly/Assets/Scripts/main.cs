using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class main : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject UIMainText;
    public GameObject positionsObject;
    public GameObject ChanceCards;
    public GameObject CommunityChestCards;
    private UIupdate textUpdater;
    private Path positions;
    private int amountOfPlayers = 4;
    private GameObject[] playerArray;
    private SteppingStones[] playerMovement;
    private PlayerData[] playerData;
    private int sameRollCounter = 0;
    private CardDeck CommunityChestDeck;
    private CardDeck ChanceDeck;



    //data for main to keep track of information.
    private int startingPosition;


    //To keep track of which part of the turn you're on
    int turnTracker = 0;
    int prevTurnTracker;
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
        textUpdater = UIMainText.GetComponent<UIupdate>() as UIupdate;
        positions = positionsObject.GetComponent<Path>() as Path;
        CommunityChestDeck = CommunityChestCards.GetComponent<CardDeck>() as CardDeck;
        ChanceDeck = ChanceCards.GetComponent<CardDeck>() as CardDeck;

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
                if (!playerMovement[currentPlayer].isMoving)
                {
                    updateText("Player " + (currentPlayer + 1).ToString() + ": Press space to roll now!");
                }

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
                        updateText("Dice 1 Rolled: " + playerMovement[currentPlayer].permsteps1.ToString() + "\t\t\tDice 2 Rolled: " + playerMovement[currentPlayer].permsteps2.ToString());
                        playerMovement[currentPlayer].goForwards();
                        turnTracker++;
                    } else {
                        //rolls snake eyes, go to jail
                        if (playerMovement[currentPlayer].permsteps1 == 1)
                        {
                            updateText("SNAKE EYES!!!!!!! YOU'RE GOING TO JAIL!!!");
                            goToJail(currentPlayer);
                            turnTracker++;
                        }
                        //rolls doubles 3 times, go to jail
                        else if (sameRollCounter == 2)
                        {
                            updateText("Dice 1 Rolled: " + playerMovement[currentPlayer].permsteps1.ToString() + "\t\t\t You got 3 doubles..... Go to Jail" + "\t\t\tDice 2 Rolled: " + playerMovement[currentPlayer].permsteps2.ToString());
                            goToJail(currentPlayer);
                            turnTracker++;
                        }
                        else
                        {
                            updateText("Dice 1 Rolled: " + playerMovement[currentPlayer].permsteps1.ToString() + "\t\t\t Doubles, roll again. Don't get 3" + "\t\t\tDice 2 Rolled: " + playerMovement[currentPlayer].permsteps2.ToString());
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
                }
                break;

            //Used for if we need to wait for the player to press space (usually text dialog)
            case 50000:
                if (Input.GetKeyDown(KeyCode.Space)){
                    //move to next part of turn after space...
                    turnTracker = prevTurnTracker+1;
                }
                break;

            //Used to wait for space, continue from same point. Great for user errors.
            case 50001:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    turnTracker = prevTurnTracker;
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

    private void updateText(string newText)
    {
        textUpdater.setText(newText);
    }

    private void determineBoardEffect()
    {
        //activate effects from landing on tile
        //case int i when i >= 16 && i <= 100:
        switch (playerMovement[currentPlayer].routePosition)
        {
            case 0: //GO
                break;

            case 4: //INCOME TAX
                updateCash(currentPlayer, -200);
                updateText("Income Tax... Lose $200. (Space)");
                waitForSpace();
                break;

            case 10: //VISITING JAIL
                turnTracker++;
                break;

            case 11:
                break;

            case 21: //FREE PARKING
                turnTracker++;
                break;

            case 31: //GO TO JAIL
                updateText("You're going to jail, feller");
                goToJail(currentPlayer);
                waitForSpace();
                break;

            case 39: //LUXURY TAX
                updateCash(currentPlayer, -100);
                updateText("Luxury Tax... Lose $100. (Space)");
                waitForSpace();
                break; //7, 23, 37

            case int i when i == 2 || i == 18 || i == 34:
                updateText("Community Chest Spot");
                //pull community Chest card deck and shuffle 
                // switch-case/if statement for each card
               /* switch ()
                {
                    case 0: // Advance to Go Collect $200
                        break;
                    case 1: //BankError Collect $200 
                        break;
                    case 2: //FromSaleOfStock Collect $50
                        break;
                    case 3: //GetOutOfJailFree 
                        break;
                    case 4: //GoToJail
                        break;
                    case 5: //HolidayFund Collect $100 
                        break;
                    case 6: //IncomeTaxRefund Collect $20
                        break;
                    case 7: //Birthday collect $10 from each player
                        break;
                    case 8: //LifeInsurance Collect $100
                        break;
                    case 9: //HospitalBill pay $100
                        break;
                    case 10: //SchoolFees Pay $100 
                        break;
                    case 11: //ConsultancyFee Collect $25 
                        break;
                    case 12: //SecondPrize Collect $10
                        break;
                    case 13: //StreetRepairs Pay $40 per house, $115 per hotel
                        break;
                    case 14: //Inheritance Collect $100
                        break;
                }*/
                waitForSpace();
                break;

            case int i when i == 7 || i == 23 || i == 37:
                updateText("Chance Chest Spot");
                //pull chance card deck and shuffle
                //switch-case/if statement for each card
                
                /*switch ()
                {
                    case 0: //Advance to Boardwalk 
                        break;
                    case 1: //Advance to Go Collect $200 
                        break;
                    case 2: //Advance to Go Collect $200  
                        break;
                    case 3: //Advance to Illinois Ave 
                        //if you pass go collect $200
                        break;
                    case 4: //Advance to St. Charles Pl. 
                        //if you pass go collect $200 
                        break;
                    case 5: //Advance to Nearest Railroad
                        //if unowned player may buy it
                        //else pay owner twice the rental they are entitled
                        break;
                    case 6: //Advance to Nearest Railroad
                        //if unowned player may buy it
                        //else pay owner twice the rental they are entitled
                        break;
                    case 7: //BankPaysYou Collect $50 
                        break;
                    case 8: //GeneralRepairs Pay $25 per house, $100 per hotel
                        break;
                    case 9: //GetOutofJailFree 
                        //maybe kept until needed or traded
                        break;
                    case 10: //Go back 3 spaces
                        break;
                    case 11: //GoToJail 
                        break;
                    case 12: //Advance to nearest utility
                        //if unowned player may buy 
                        // else roll and pay ten times the amount thrown
                        break;
                    case 13: //ReadingRailRoadTrip
                        //if you pass go collect $200
                        break;
                    case 14: //SpeedingFine pay $15
                        break;
                    case 15: //BuildingLoanMatures Collect $150
                        break;
                }*/
                waitForSpace();
                break;

            case int i when i == 5 || i == 13 || i == 16 || i == 26 || i == 29 || i == 36:
                updateText("Other Property Spot");
                waitForSpace();
                break;

            default:
                ImprovableProperty property = positions.getPositionData(playerMovement[currentPlayer].routePosition).positionObject.GetComponent<ImprovableProperty>() as ImprovableProperty;
                updateText("Landed on: " + property.name + " (space)");
                waitForSpace();
                break;
        }
    }
    private void DetermineCardEffect()
    {
        //updateText(cardEffect);
        /*switch ()
        {
            case 1:

        }*/
    }

    void waitForSpace()
    {
        //Uses case 50000 to wait
        prevTurnTracker = turnTracker;
        turnTracker = 50000;
    }

    void waitForSpaceReturnHere()
    {
        //Uses case 50001
        prevTurnTracker = turnTracker;
        turnTracker = 50001;
    }

}

