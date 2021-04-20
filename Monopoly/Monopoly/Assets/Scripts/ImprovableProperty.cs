using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovableProperty : Card
{
    public string name = null;
    public int position = 0;
    public int cost = 0;
    public int generalRent = 0;
    public int houseRent1 = 0;
    public int houseRent2 = 0;
    public int houseRent3 = 0;
    public int houseRent4 = 0;
    public int hotelRent = 0;
    public int mortgageValue = 0;
    public int houseCost = 0;
    public int hotelCost = 0;
    public int[] rents;



    public PlayerData currentOwner = null;
    public int currentOwnerInt = -1;
    public int houseCount = 0;



    // Start is called before the first frame update
    void Start()
    {
        rents = new int[] {generalRent, houseRent1, houseRent2, houseRent3, houseRent4, hotelRent};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getRent()
    {
        return rents[houseCount];
    }
}
