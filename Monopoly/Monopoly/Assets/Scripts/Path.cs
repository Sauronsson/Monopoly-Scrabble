using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    Transform[] childObject;
    public List<Transform> childNodeList = new List<Transform>();


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.color = Color.clear;

        FillNodes();
        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentPos = childNodeList[i].position;
            if(i>0)
            {
                Vector3 prevPos = childNodeList[i-1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }


    }

    void FillNodes()
    {

        childNodeList.Clear();
        childObject = GetComponentsInChildren<Transform>();
        foreach (Transform child in childObject)
        {
            if(child != this.transform)
            {
                childNodeList.Add(child);
            }
        }
    }
}
