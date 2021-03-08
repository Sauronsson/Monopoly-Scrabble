using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    Vector3[] boardPositions = {new Vector3(-15.5, 0.25, 16.5), new Vector3(-11.8, 0.25, 16.5), new Vector3(-9,0.25,16.5), //GO, MEDITERRANEAN, COMMUNITY
                                new Vector3(-6, 0.25, 16.5), new Vector3(-3, 0.25, 16.5), new Vector3(0, 0.25, 16.5),  //BALTIC, INCOME, READING RAILROAD
                                new Vector3(3, 0.25, 16.5), new Vector3(6, 0.25, 16.5), new Vector3(9, 0.25, 16.5), //ORIENTAL, CHANCE, VERMONT
                                new Vector3(12, 0.25, 16.5), new Vector3(17.5, 0.25, 17.7), new Vector3(16.5, 0.25, 12), // CONNECTICUT, JAIL, ST CHARLES
                                new Vector3(16.5, 0.25, 9), new Vector3(16.5, 0.25, 6), new Vector3(16.5, 0.25, 3), // ELECTRIC, STATES, VIRGINIA
                                new Vector3(16.5, 0.25, 0), new Vector3(16.5, 0.25, -3), new Vector3(16.5, 0.25, -6), //PENNSYLVANIA, ST JAMES, COMMUNITY
                                new Vector3(16.5, 0.25, -9), new Vector3(16.5, 0.25, -12), new Vector3(16.5, 0.25, -16.5), //TENESSEE, NEW YORK, FREE PARKING
                                new Vector3(12, 0.25, -16.5), new Vector3(9, 0.25, -16.5), new Vector3(6, 0.25, -16.5), // KENTUCKY, CHANCE, INDIANA
                                new Vector3(3, 0.25, -16.5), new Vector3(0, 0.25, -16.5), new Vector3(-3, 0.25, 16.5), //CHANCE, B&O RAILROAD, ATLANTIC
                                new Vector3(-6, 0.25, -16.5), new Vector3(-9, 0.25, -16.5), new Vector3(-12, 0.25, -16.5), //VENTNOR, WATER WORKS, MARVIN GARDENS
                                new Vector3(-16.5, 0.25, -16.5), new Vector3(-16.5, 0.25, -12), new Vector3(-16.5, 0.25, -9), //GOTO JAIL, PACIFIC, NORTH CAROLINA
                                new Vector3(-16.5, 0.25, -6), new Vector3(-16.5, 0.25, -3), new Vector3(-16.5, 0.25, 0), //COMMUNITY, PENNSYLVANIA, SHORT LINE,
                                new Vector3(-16.5, 0.25, 3), new Vector3(-16.5, 0.25, 6), new Vector3(-16.5, 0.25, 9), //CHANCE, PARK PLACE, LUXURY TAX
                                new Vector3(-16.5, 0.25, 12), new Vector3(-16.5, 0.25, 16.5)}; //BOARDWALK, GO

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
