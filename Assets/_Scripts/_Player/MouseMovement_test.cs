using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement_test : MonoBehaviour
{
    private float screenWidth = Screen.width;
    private float screenHeight = Screen.height;
    public Vector3 normalizedMousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        normalizedMousePos = new Vector3(UpdateVectorX(), UpdateVectorY(), 0.0f);
        if (Input.GetButtonDown("Fire1"))
        {
            {
                //Debug.Log("mouse x="+normalizedMousePos.x);
                //Debug.Log("mouse y=" + normalizedMousePos.y);
            }
        }
    }
    float UpdateVectorX()
    {
         if (Input.mousePosition.x > screenWidth)
            return 1.0f;
        else if (Input.mousePosition.x < 0)
            return -1.0f;
        else
            return (Input.mousePosition.x * 2 - screenWidth)/screenWidth;
    }
    float UpdateVectorY()
    {
        if (Input.mousePosition.y > screenHeight)
            return 1.0f;
        else if (Input.mousePosition.y < 0)
            return -1.0f;
        else
            return (Input.mousePosition.y * 2 - screenHeight) / screenHeight;
    }
}

