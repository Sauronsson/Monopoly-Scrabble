using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppingStones : MonoBehaviour
{
    public Path currentRoute;

    int routePosition;

    private int steps;
    private int steps1;
    public int permsteps1 = 0;
    public int permsteps2 = 0;

    public bool isMoving;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {

        }



        /*if(routePosition+steps < currentRoute.childNodeList.Count){
            StartCoroutine(Move());

        }else{
            Debug.Log("Rolled Number is to high: " + steps);
            //Insert something here to repeat the route

        }*/



    }

    public bool roll()
    {
        steps = Random.Range(1, 7);
        steps1 = Random.Range(1, 7);
        Debug.Log("Dice Rolled " + steps);
        Debug.Log("Dice Rolled " + steps1);

        //stored for use in turn manager
        permsteps1 = steps;
        permsteps2 = steps1;
        steps += steps1;
        if (steps == steps1)
        {
            Debug.Log("SNAKE EYES GO TO JAIL" + steps);
            StartCoroutine(Move());
            return true;
        }
        else
        {
            StartCoroutine(Move());
            return false;
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


            Debug.Log(currentRoute);
            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            while(MoveToNextNode(nextPos)){yield return null;}

            //yield return new WaitForSeconds(0.1f);
            steps--;
            //routePosition++;
        }
        isMoving = false;

    }
    bool MoveToNextNode(Vector3 goal){
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 4f * Time.deltaTime));
    }
}
