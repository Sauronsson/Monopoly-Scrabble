using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UIupdate : MonoBehaviour
{

    public Text textObject;
    public PlayerData player = null;
    public ImprovableProperty property = null;
    public OtherProperty oProperty = null;



    void Update()
    {
        //ONLY UPDATE IF CASH
        if (player != null)
        {
            //Press the space key to change the Text message  
            int cash = player.currentCash;
            string playerCash = cash.ToString();
            //Text sets your text to say this message
            textObject.text = playerCash;
            //p1.text = "My text has now changed.";
        }

        if (property != null)
        {
            textObject.text = (property.name + ": " + property.currentOwnerInt.ToString() + "   H: " + property.houseCount.ToString());
        }

        if (oProperty != null)
        {
            textObject.text = (oProperty.name + ": " + oProperty.currentOwnerInt.ToString());
        }
    }

    public void setText(string newText)
    {
        textObject.text = newText;
    }





    /*
    public PlayerData player;



    void updateText()
    {


    }
    */

}
