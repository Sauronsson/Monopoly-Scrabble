using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UIupdate : MonoBehaviour
{

      public Text p1;
      public Text p2;
      public Text p3;
      public Text p4;
      public PlayerData player;

   

    void Update()
    {
        //Press the space key to change the Text message  
            int cash = player.currentCash;
            string playerCash = cash.ToString();
            //Text sets your text to say this message
            p1.text = playerCash;
            //p1.text = "My text has now changed.";

    }





    /*
    public PlayerData player;



    void updateText()
    {


    }
    */

}
