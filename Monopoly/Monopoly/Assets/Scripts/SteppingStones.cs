using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppingStones : MonoBehaviour
{
    public Path currentRoute;

    public int routePosition = 0;
    private int jailPosition = 11;
    private int maxPosition;


    private int steps;
    private int steps1;
    public int permsteps1 = 0;
    public int permsteps2 = 0;

    public bool isMoving;

    void Start()
    {
        maxPosition = currentRoute.childNodeList.Count-1;
    }

    //Commands to determine what position to go to
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
            StartCoroutine(MoveForwards());
            return true;
        }
        else
        {
            StartCoroutine(MoveForwards());
            return false;
        }

    }

    

    public bool goToJail()
    {
        steps = 1;
        StartCoroutine(MoveSpecificPoint(jailPosition));
        return true;
    }

    public bool goBackwards(int stepCount)
    {
        steps = stepCount;
        StartCoroutine(MoveBackwards());
        return true;
    }

    //Movers, only accessed by this class
    IEnumerator MoveSpecificPoint(int newRoutePosition)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {

            routePosition = newRoutePosition;

            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            while (MoveToNextNode(nextPos)) { yield return null; }

            //yield return new WaitForSeconds(0.1f);
            steps--;
            //routePosition++;
        }
        isMoving = false;
    }

    IEnumerator MoveForwards()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {

            routePosition++;
            routePosition %= currentRoute.childNodeList.Count;

            if (currentRoute.childNodeList[routePosition].name == "Jail")
            {
                routePosition++;
            }

            Debug.Log(currentRoute);
            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            while (MoveToNextNode(nextPos)) { yield return null; }

            //yield return new WaitForSeconds(0.1f);
            steps--;
            //routePosition++;
        }
        isMoving = false;
    }

    IEnumerator MoveBackwards()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {
            if (routePosition == 0)
            {
                routePosition = maxPosition;
            }
            else
            {
                routePosition--;
            }

            if (currentRoute.childNodeList[routePosition].name == "Jail")
            {
                routePosition--;
            }

            Debug.Log(currentRoute);
            Vector3 nextPos = currentRoute.childNodeList[routePosition].position;
            while (MoveToNextNode(nextPos)) { yield return null; }

            //yield return new WaitForSeconds(0.1f);
            steps--;
            //routePosition++;
        }
        isMoving = false;
    }

    //Movement Function
    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 4f * Time.deltaTime));
    }

}
