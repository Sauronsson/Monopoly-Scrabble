using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    

    private Vector3 MAX = new Vector3((float)2.5, 0, (float)10);
    private Vector3 MIN = new Vector3((float)-2.5, 0, (float)-10);
    float mainSpeed = 100.0f;
    private float totalRun = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Referenced: https://docs.unity3d.com/ScriptReference/Input.GetKey.html
    // Referenced: https://gist.github.com/gunderson/d7f096bd07874f31671306318019d996 Used this quite a bit to get this code to work properly
    void Update()
    {
        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        
        totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
        p = p * mainSpeed;
        
        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        transform.Translate(p);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && MIN.z < transform.position.z)
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && MAX.z > transform.position.z)
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && MAX.x > transform.position.x)
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && MIN.x < transform.position.x)
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}

