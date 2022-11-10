using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //Check to see if the player is inside the bounds on X-axis
        float deltaX = lookAt.position.x - transform.position.x; //Difference between player position and camera's position
        if (deltaX > boundX)
        {
            delta.x = deltaX - boundX;
        }
        else if (deltaX < -boundX)
        {
            delta.x = deltaX + boundX;
        }

        //Check to see if the player is inside the bounds on Y-axis
        float deltaY = lookAt.position.y - transform.position.y; //Difference between player position and camera's position
        if (deltaY > boundY)
        {
            delta.y = deltaY - boundY;
        }
        else if (deltaY < -boundY)
        {
            delta.y = deltaY + boundY;
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
