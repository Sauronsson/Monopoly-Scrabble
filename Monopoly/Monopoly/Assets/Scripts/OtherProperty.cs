using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherProperty : Card
{
    public string name;
    public int type; //0 is railroad, 1 is utility
    public int position;
    public int cost;
    public int rent = 0;
    public int rent2 = 0;
    public int rent3 = 0;
    public int rent4 = 0;
    public int mortgageValue;
    public int[] rents;

    public PlayerData currentOwner = null;
    public int currentOwnerInt = -1;

    // Start is called before the first frame update
    void Start()
    {
        rents = new int[] { rent, rent2, rent3, rent4 };
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getRent(int i)
    {
        return rents[i];
    }
}
