using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;


public class main : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject UIMainText;
    public GameObject positionsObject;
    public GameObject ChanceCard;
    public GameObject CommunityCard;
    public GameObject playerInput;
    public InputField playerInputText;

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
    private int turnTracker = 0;
    private int prevTurnTracker;
    //To determine who's turn it is
    private int currentPlayer = 0;
    private int prevCurrentPlayer = 0;

    //Function Utilities
    private ImprovableProperty property = null;
    private OtherProperty oProperty = null;
    private int lastUpdatedPlayer = 0;
    private int cashTracker = 0;

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
        CommunityChestDeck = CommunityCard.GetComponent<CardDeck>() as CardDeck;
        ChanceDeck = ChanceCard.GetComponent<CardDeck>() as CardDeck;

        //Player Specific Scripts
        playerMovement = new SteppingStones[] { p1MoveScript, p2MoveScript, p3MoveScript, p4MoveScript };
        playerData = new PlayerData[] { p1DataScript, p2DataScript, p3DataScript, p4DataScript };
        playerInput.SetActive(false);
        ChanceDeck.shuffle();
        CommunityChestDeck.shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        switch (turnTracker) {

            //beginning function, tbd on use
            case -2:
                updateText("Player " + (currentPlayer + 1).ToString() + " has been in jail for " + (playerData[currentPlayer].jailCounter + 1) + " turns...... (1) Roll for doubles, (2) Pay $50 fine, (3) Use get out of jail card");
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    int roll1 = UnityEngine.Random.Range(1, 7);
                    int roll2 = UnityEngine.Random.Range(1, 7);
                    if (roll1 == roll2)
                    {
                        updateText("Dice 1 Rolled: " + roll1.ToString() + "\t\t\t You got a double! You may leave!" + "\t\t\tDice 2 Rolled: " + roll2.ToString());
                        playerData[currentPlayer].inJail = false;
                        turnTracker = 0;
                    }
                    else if (playerData[currentPlayer].jailCounter > 2)
                    {
                        updateText("You've been here long enough, get outta here!");
                        playerData[currentPlayer].inJail = false;
                        turnTracker = 0;
                    }
                    else
                    {
                        updateText("Dice 1 Rolled: " + roll1.ToString() + "\t\t\t Looks like you're staying here a bit longer buckaroo " + "\t\t\tDice 2 Rolled: " + roll2.ToString());
                        turnTracker = 2;
                        playerData[currentPlayer].jailCounter = playerData[currentPlayer].jailCounter + 1;
                    }
                    waitForSpace();
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    updateText("Thought we wouldn't take a bribe eh? ACAB");
                    playerData[currentPlayer].inJail = false;
                    turnTracker = 0;
                    waitForSpace();
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    updateText("Papers please, you can do better than that....");
                    waitForSpaceReturnHere();
                    break;
                }
                break;

            case 0:
                if (playerData[currentPlayer].inJail) //so we can have a front menu before jail pops up
                {
                    turnTracker = -2;
                }
                else
                {
                    turnTracker++;
                }
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
                if (Input.GetKeyDown(KeyCode.Space)) {
                    //move to next part of turn after space...
                    turnTracker = prevTurnTracker + 1;
                }
                break;

            //Used to wait for space, continue from same point. Great for user errors.
            case 50001:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    turnTracker = prevTurnTracker;
                }
                break;

            //Used to go over property if already owned by a player
            case 50002: //ASSUMED: property is defined
                updateCash(currentPlayer, -1 * property.getRent());
                updateCash(property.currentOwnerInt, property.getRent());
                updateText("Paying Player " + (property.currentOwnerInt + 1) + " $" + property.getRent());
                waitForSpaceNoTracking();
                break;

            //Used to buy property
            case 50003: //ASSUMED: property is defined
                updateText(property.name + " costs: " + property.cost + ". Would you like to buy it? yes: (y), no: (n)");
                if (Input.GetKeyDown(KeyCode.Y)) {
                    buyProperty(currentPlayer, property.cost, property);
                    updateText("Congrats on your purchase! (space)");
                    waitForSpaceNoTracking();
                    turnTracker = prevTurnTracker + 1;
                } else if (Input.GetKeyDown(KeyCode.N)) {
                    updateText("Moving to Auction. Current player will start with $1");
                    lastUpdatedPlayer = currentPlayer; //so we don't lose person's turn
                    cashTracker = 1;
                    prevCurrentPlayer = currentPlayer;
                    currentPlayer++;
                    waitForSpace(); //will advance turn tracker to auction state
                }
                break;

            case 50004://AUCTION acts as its own turn order until finished //TODO ADD LOGIC FOR VALUE THE SAME, OR BELOW
                playerInput.SetActive(true);
                updateText("Can you beat $" + cashTracker + " player " + (currentPlayer + 1) + "? Player " + (prevCurrentPlayer + 1) + " is in the lead. (space)");
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
                {
                    playerInputText = playerInput.GetComponent<InputField>() as InputField;
                    if (playerInputText.text != "")
                    {
                        try
                        {
                            int newCash = Int32.Parse(playerInputText.text);
                            if (newCash > cashTracker)
                            {
                                cashTracker = newCash;
                                playerInputText.text = "";
                                prevCurrentPlayer = currentPlayer;
                                updateText("Player " + (currentPlayer + 1) + " is in the lead with $" + cashTracker);
                                if (currentPlayer + 1 == amountOfPlayers)
                                {
                                    currentPlayer = 0;
                                }
                                else
                                {
                                    currentPlayer = currentPlayer + 1;
                                }
                                waitForSpaceReturnHere();
                                break;
                            }
                            else
                            {
                                updateText("Not a valid number, try again");
                                playerInputText.text = "";
                                waitForSpaceReturnHere();
                                break;
                            }

                        }
                        catch (Exception e)
                        {
                            updateText("Not a valid number, try again");
                            playerInputText.text = "";
                            waitForSpaceReturnHere();
                            break;
                        }

                    } else { //blank text box
                        if (currentPlayer + 1 == prevCurrentPlayer)
                        {
                            buyProperty(currentPlayer, cashTracker, property);
                            updateText("Congrats Player " + (prevCurrentPlayer + 1) + " on winning the auction! Enjoy your new property!");
                            currentPlayer = lastUpdatedPlayer;
                            turnTracker = 2; //This is bad practice. don't do this.
                            playerInput.SetActive(false);
                            waitForSpace();
                            break;
                        }
                        else
                        {
                            if (currentPlayer + 1 == amountOfPlayers)
                            {
                                currentPlayer = 0;
                            }
                            else
                            {
                                currentPlayer = currentPlayer + 1;
                            }
                            break;
                        }
                    }
                }
                break;

            //Used to go over other property property if already owned by a player
            case 50005: //ASSUMED: property is defined
                if (oProperty.type == 0)
                {
                    updateCash(currentPlayer, -1 * oProperty.getRent(playerData[oProperty.currentOwnerInt].railroadsOwned));
                    updateCash(property.currentOwnerInt, oProperty.getRent(playerData[oProperty.currentOwnerInt].railroadsOwned));
                    updateText("Paying Player " + (oProperty.currentOwnerInt + 1) + " $" + oProperty.getRent(playerData[oProperty.currentOwnerInt].railroadsOwned));
                }
                else if (oProperty.type == 1)
                {
                    updateCash(currentPlayer, -1 * property.getRent());
                    updateCash(property.currentOwnerInt, property.getRent());
                    updateText("Paying Player " + (oProperty.currentOwnerInt + 1) + " $" + oProperty.getRent(playerData[oProperty.currentOwnerInt].utilitiesOwned));
                }
                waitForSpaceNoTracking();
                break;

            //Used to buy property
            case 50006: //ASSUMED: property is defined
                updateText(oProperty.name + " costs: " + oProperty.cost + ". Would you like to buy it? yes: (y), no: (n)");
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    buyOProperty(currentPlayer, oProperty.cost, oProperty);
                    updateText("Congrats on your purchase! (space)");
                    waitForSpaceNoTracking();
                    turnTracker = prevTurnTracker + 1;
                }
                else if (Input.GetKeyDown(KeyCode.N))
                {
                    updateText("Moving to Auction. Current player will start with $1");
                    lastUpdatedPlayer = currentPlayer; //so we don't lose person's turn
                    cashTracker = 1;
                    prevCurrentPlayer = currentPlayer;
                    currentPlayer++;
                    waitForSpace(); //will advance turn tracker to auction state
                }
                break;

            case 50007://AUCTION acts as its own turn order until finished //TODO ADD LOGIC FOR VALUE THE SAME, OR BELOW
                playerInput.SetActive(true);
                updateText("Can you beat $" + cashTracker + " player " + (currentPlayer + 1) + "? Player " + (prevCurrentPlayer + 1) + " is in the lead. (space)");
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
                {
                    playerInputText = playerInput.GetComponent<InputField>() as InputField;
                    if (playerInputText.text != "")
                    {
                        try
                        {
                            int newCash = Int32.Parse(playerInputText.text);
                            if (newCash > cashTracker)
                            {
                                cashTracker = newCash;
                                playerInputText.text = "";
                                prevCurrentPlayer = currentPlayer;
                                updateText("Player " + (currentPlayer + 1) + " is in the lead with $" + cashTracker);
                                if (currentPlayer + 1 == amountOfPlayers)
                                {
                                    currentPlayer = 0;
                                }
                                else
                                {
                                    currentPlayer = currentPlayer + 1;
                                }
                                waitForSpaceReturnHere();
                                break;
                            }
                            else
                            {
                                updateText("Not a valid number, try again");
                                playerInputText.text = "";
                                waitForSpaceReturnHere();
                                break;
                            }

                        }
                        catch (Exception e)
                        {
                            updateText("Not a valid number, try again");
                            playerInputText.text = "";
                            waitForSpaceReturnHere();
                            break;
                        }

                    }
                    else
                    { //blank text box
                        if (currentPlayer + 1 == prevCurrentPlayer)
                        {
                            buyOProperty(currentPlayer, cashTracker, oProperty);
                            updateText("Congrats Player " + (prevCurrentPlayer + 1) + " on winning the auction! Enjoy your new property!");
                            currentPlayer = lastUpdatedPlayer;
                            turnTracker = 2; //This is bad practice. don't do this.
                            playerInput.SetActive(false);
                            waitForSpace();
                            break;
                        }
                        else
                        {
                            if (currentPlayer + 1 == amountOfPlayers)
                            {
                                currentPlayer = 0;
                            }
                            else
                            {
                                currentPlayer = currentPlayer + 1;
                            }
                            break;
                        }
                    }
                }
                break;


            //move to next player, reset turn tracker
            default:
                if (currentPlayer + 1 == amountOfPlayers) {
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
        playerData[currentPlayer].inJail = true;
        return true;
    }

    //helper method, to take care of extra effects from changing cash, like bankruptcy
    private bool updateCash(int currentPlayer, int amount) 
    {
        playerData[currentPlayer].updateCash(amount);
        return true;
    }

    private void determineBoardEffect()
    {
        //activate effects from landing on tile
        //case int i when i >= 16 && i <= 100:
        switch (playerMovement[currentPlayer].routePosition)
        {
            case 0: //GO
                turnTracker++;
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
                turnTracker++;
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
                //reference to cardEffect method
                DetermineCommunityCardEffect();
                waitForSpace();
                break;

            case int i when i == 7 || i == 23 || i == 37:
                updateText("Chance Chest Spot");
                // reference to cardEffect method
                DetermineChanceCardEffect();
                waitForSpace();
                break;

            case int i when i == 5 || i == 13 || i == 16 || i == 26 || i == 29 || i == 36:
                updateText("Other Property Spot");
                oProperty = positions.getPositionData(playerMovement[currentPlayer].routePosition).positionObject.GetComponent<OtherProperty>() as OtherProperty;
                if (oProperty.currentOwner != null)
                {
                    //property is currently Owned
                    prevTurnTracker = turnTracker;
                    turnTracker = 50005;
                } else
                {
                    //property isn't currently Owned
                    prevTurnTracker = turnTracker;
                    turnTracker = 50006;
                }
                break;

            default:
                property = positions.getPositionData(playerMovement[currentPlayer].routePosition).positionObject.GetComponent<ImprovableProperty>() as ImprovableProperty;
                if (property.currentOwner != null)
                {
                    //property is currently Owned
                    prevTurnTracker = turnTracker;
                    turnTracker = 50002;
                } else {
                    //property isn't currently Owned
                    prevTurnTracker = turnTracker;
                    turnTracker = 50003;
                }
                break;
        }
    }
    
    private void DetermineChanceCardEffect()
    {
        ChanceCard cardofChance = (ChanceCard)(ChanceDeck.draw());
        //updateText(ChanceCard.CardEffect);
        int cashEffect = -1; // = cardofChance.cash;
        int positionEffect = -1; // = cardofChance.position;
        updateText(cardofChance.cardEffect + "");
        switch (cardofChance.function)
        {
            case 1://move to position
                positionEffect = cardofChance.position;
                playerMovement[currentPlayer].goToPoint(positionEffect);
                break;
            case 2://add cash
                cashEffect = cardofChance.cash;
                updateCash(currentPlayer, cashEffect);
                break;
            case 3://both 1 and 2
                cashEffect = cardofChance.cash;
                positionEffect = cardofChance.position;
                playerMovement[currentPlayer].goToPoint(positionEffect);
                updateCash(currentPlayer, cashEffect);
                break;
            case 4://get out of jail 
               /* playerMovement[currentPlayer].goBackwards(positionEffect);//*/
                break;
            case 5://go to jail
                goToJail(currentPlayer);
                break;
            case 6://update cash per property the current player owns by specific amount\\not implemented//
                break;
           /* case 7://update cash by subtracting cash from every player except current and pay current player that cash
                int Bdaymoney = (amountOfPlayers-1)*10;
                for (int i = 0; i<=(amountOfPlayers); i++)
                {
                    updateCash(i, cashEffect);
                }
                updateCash(currentPlayer, Bdaymoney);
                break;*/
            default:
                //do nothing 
                break;
        }
    }

    private void DetermineCommunityCardEffect()
    { 
        CommunityCard cardofCommunity = (CommunityCard)(CommunityChestDeck.draw());
        //updateText(ChanceCard.CardEffect);
        int cashEffect = -1;//= cardofCommunity.cash;
        int positionEffect = -1;//= cardofCommunity.position;
        updateText(cardofCommunity.cardEffect + "");
        switch (cardofCommunity.function)
        {
            case 1://move to position
                positionEffect = cardofCommunity.position;
                playerMovement[currentPlayer].goToPoint(positionEffect);
                break;
            case 2://add cash
                cashEffect = cardofCommunity.cash;
                updateCash(currentPlayer, cashEffect);
                break;
            case 3://both 1 and 2
                positionEffect = cardofCommunity.position;
                cashEffect = cardofCommunity.cash;
                playerMovement[currentPlayer].goToPoint(positionEffect);
                updateCash(currentPlayer, cashEffect);
                break;
            case 4://get out of jail 
                /* playerMovement[currentPlayer].goBackwards(positionEffect);//*/
                break;
            case 5://go to jail
                goToJail(currentPlayer);
                break;
            case 6://update cash per property the current player owns by specific amount\\not implemented//
                break;
            case 7://update cash by subtracting cash from every player except current and pay current player that cash
                int Bdaymoney = (amountOfPlayers - 1) * 10;
                for (int i = 0; i <= (amountOfPlayers); i++)
                {
                    updateCash(i, cashEffect);
                }
                updateCash(currentPlayer, Bdaymoney);
                break;
            default:
                //do nothing 
                break;
        }
    }

    void buyOProperty(int player, int cost, OtherProperty oProperty)
    {
        updateCash(player, -1 * cost);
        if (oProperty.type == 0)
        {
            playerData[player].railroadsOwned = playerData[player].railroadsOwned + 1;
        }
        else
        {
            playerData[player].utilitiesOwned = playerData[player].utilitiesOwned + 1;
        }
        Debug.Log(player);
        oProperty.currentOwner = playerData[player];
        oProperty.currentOwnerInt = player;
    }

    void buyProperty(int player, int cost, ImprovableProperty property)
    {
        updateCash(player, -1 * cost);
        property.currentOwner = playerData[player];
        property.currentOwnerInt = player;
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

    void waitForSpaceNoTracking()
    {
        turnTracker = 50000;
    }
    
    private void updateText(string newText)
    {
        textUpdater.setText(newText);
    }
}