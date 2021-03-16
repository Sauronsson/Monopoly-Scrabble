using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    //int POSITION_AMOUNT = 41;
    Vector3[] boardPositions = {new Vector3((float)-16.5,(float) 0.25,(float) 16.5), new Vector3((float)-11.8,(float) 0.25,(float) 16.5), new Vector3((float)-9,(float)0.25,(float)16.5), //GO, MEDITERRANEAN, COMMUNITY
                                new Vector3((float)-6,(float) 0.25, (float)16.5), new Vector3((float)-3,(float) 0.25,(float) 16.5), new Vector3((float)0,(float) 0.25,(float) 16.5),  //BALTIC, INCOME, READING RAILROAD
                                new Vector3((float)3, (float)0.25,(float) 16.5), new Vector3((float)6,(float) 0.25,(float) 16.5), new Vector3((float)9,(float) 0.25,(float) 16.5), //ORIENTAL, CHANCE, VERMONT
                                new Vector3((float)12,(float) 0.25,(float) 16.5), new Vector3((float)17.5,(float) 0.25, (float)17.7), new Vector3((float)16.5, (float)0.25, (float)12), // CONNECTICUT, JAIL, ST CHARLES
                                new Vector3((float)16.5, (float)0.25, (float)9), new Vector3((float)16.5, (float)0.25, (float)6), new Vector3((float)16.5, (float)0.25, (float)3), // ELECTRIC, STATES, VIRGINIA
                                new Vector3((float)16.5, (float)0.25, (float)0), new Vector3((float)16.5, (float)0.25, (float)-3), new Vector3((float)16.5, (float)0.25, (float)-6), //PENNSYLVANIA, ST JAMES, COMMUNITY
                                new Vector3((float)16.5, (float)0.25, (float)-9), new Vector3((float)16.5, (float)0.25, (float)-12), new Vector3((float)16.5, (float)0.25, (float)-16.5), //TENESSEE, NEW YORK, FREE PARKING
                                new Vector3((float)12, (float)0.25, (float)-16.5), new Vector3((float)9, (float)0.25, (float)-16.5), new Vector3((float)6, (float)0.25, (float)-16.5), // KENTUCKY, CHANCE, INDIANA
                                new Vector3((float)3, (float)0.25, (float)-16.5), new Vector3((float)0, (float)0.25, (float)-16.5), new Vector3((float)-3, (float)0.25, (float)16.5), //CHANCE, B&O RAILROAD, ATLANTIC
                                new Vector3((float)-6, (float)0.25, (float)-16.5), new Vector3((float)-9, (float)0.25, (float)-16.5), new Vector3((float)-12, (float)0.25, (float)-16.5), //VENTNOR, WATER WORKS, MARVIN GARDENS
                                new Vector3((float)-16.5, (float)0.25, (float)-16.5), new Vector3((float)-16.5, (float)0.25, (float)-12), new Vector3((float)-16.5, (float)0.25, (float)-9), //GOTO JAIL, PACIFIC, NORTH CAROLINA
                                new Vector3((float)-16.5,(float) 0.25, (float)-6), new Vector3((float)-16.5, (float)0.25, (float)-3), new Vector3((float)-16.5, (float)0.25,(float) 0), //COMMUNITY, PENNSYLVANIA, SHORT LINE,
                                new Vector3((float)-16.5, (float)0.25, (float)3), new Vector3((float)-16.5, (float)0.25, (float)6), new Vector3((float)-16.5, (float)0.25, (float)9), //CHANCE, PARK PLACE, LUXURY TAX
                                new Vector3((float)-16.5, (float)0.25, (float)12), new Vector3((float)-16.5, (float)0.25, (float)16.5)}; //BOARDWALK, GO
    int currPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int newPosition(int movementAmt)
    {
        int newPos = currPos + movementAmt;
        if (newPos > boardPositions.Length) { //passes go
            newPos = newPos - boardPositions.Length;
            return newPos;
        } else {
            return newPos;
        }
    }

    Vector3 getPositionVector(int position) {
        if (position < boardPositions.Length){
            return boardPositions[position];
        } else {
            return new Vector3(-1,-1,-1);
        }
    }
}
