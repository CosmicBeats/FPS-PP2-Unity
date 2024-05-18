using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Vector3 doorClosedPosition;
    Vector3 doorOpenPosition;


    void Start()
    {
        doorClosedPosition = transform.position;
        doorOpenPosition = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    }
    //Modify this for multiple door with tags
    public void OpenDoor()
    {
        if (transform.position != doorOpenPosition)
        {
            transform.LeanMoveLocalY(3.5f, 1).setEaseInSine();
        }
    }
}
