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
            steps = Random.Range(1,8);
            steps1 = Random.Range(1,8);
            Debug.Log("Dice Rolled " + steps);
            Debug.Log("Dice Rolled " + steps1);
            steps += steps1;
            if(routePosition+steps < currentRoute.childNodeList.Count){
                StartCoroutine(Move());

            }else{
                Debug.Log("Rolled Number is to high: " + steps);
            }


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
            Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;
            while(MoveToNextNode(nextPos)){yield return null;}

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePosition++;
        }
        isMoving = false;

    }
    bool MoveToNextNode(Vector3 goal){
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }
}
