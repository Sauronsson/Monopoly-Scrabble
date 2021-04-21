using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //Player Data
    public int currentCash;
    
    //Game Data
    public bool    inJail = false;
    public bool    passedGo = false;
    public int     jailCounter = 0;
    public int     currentOwedCash = 0;
    public int railroadsOwned = -1;
    public int utilitiesOwned = -1;
    public List<ImprovableProperty> ImprovableProperties = new List<ImprovableProperty>();
    public List<OtherProperty> OtherProperties = new List<OtherProperty>();
    // Start is called before the first frame update
    void Start()
    {
        currentCash = 1500;
    }

    //returns whether or not this succeeded. Main reason for failure is if Player goes to 0 or below.
    public bool updateCash(int cashToAdd) {
        currentCash += cashToAdd;
        Debug.Log(currentCash);
        if (cashToAdd > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Goal 1: Track player property in player data
    public void addImprovableProperty(ImprovableProperty improvablePropertyObject)
    {
        ImprovableProperties.Add(improvablePropertyObject);
        //Debug.Log();
    }
    public void removeImprovableProperty(ImprovableProperty improvablePropertyObject)
    {
        ImprovableProperties.Remove(improvablePropertyObject);
    }

    public void addOtherProperty(OtherProperty otherPropertyObject)
    {
        OtherProperties.Add(otherPropertyObject);
        //Debug.Log();
    }
    public void removeOtherProperty(OtherProperty otherPropertyObject)
    {
        OtherProperties.Remove(otherPropertyObject);
    }
    //Goal 2: updateCash(); if < 0, bankruptcy case 
    //Goal 3: Auction all properties, then skip this player



}
