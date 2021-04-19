using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovableProperty : Card
{
    public string name;
    public int position;
    public int cost;
    public int generalRent;
    public int houseRent1;
    public int houseRent2;
    public int houseRent3;
    public int houseRent4;
    public int hotelRent;
    public int mortgageValue;
    public int houseCost;
    public int hotelCost;
    public int[] rents;
    public PlayerData currentOwner = null;



    // Start is called before the first frame update
    void Start()
    {
        rents = new int[] {generalRent, houseRent1, houseRent2, houseRent3, houseRent4, hotelRent};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
