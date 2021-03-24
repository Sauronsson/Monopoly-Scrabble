using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppingStones : MonoBehaviour
{
    public Path currentRoute;

    int routePosition;

    public int steps;
    public int steps1;

    bool isMoving;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            steps = Random.Range(1,7);
            steps1 = Random.Range(1,7);
            Debug.Log("Dice Rolled " + steps);
            Debug.Log("Dice Rolled " + steps1);
            
            steps += steps1;
            if(steps == 2)
            {
                Debug.Log("SNAKE EYES GO TO JAIL" + steps);
            }

            StartCoroutine(Move());



            /*if(routePosition+steps < currentRoute.childNodeList.Count){
                StartCoroutine(Move());

            }else{
                Debug.Log("Rolled Number is to high: " + steps);
                //Insert something here to repeat the route
                
            }*/


        }

    }


    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;

        while(steps>0)
        {

            routePosition++;
            routePosition %= currentRoute.childNodeList.Count;



            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            while(MoveToNextNode(nextPos)){yield return null;}

            yield return new WaitForSeconds(0.1f);
            steps--;
            //routePosition++;
        }
        isMoving = false;

    }
    bool MoveToNextNode(Vector3 goal){
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 4f * Time.deltaTime));
    }
}
