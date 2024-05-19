using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Vector3 doorClosedPosition;
    Vector3 doorOpenPosition;
    List<GameObject> hiddenDoors = new List<GameObject>();


    void Start()
    {
        if (hiddenDoors.Count == 0)
        {
            hiddenDoors.Add(GameObject.FindWithTag("Hidden Door"));
        }


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

    public void OpenHiddenDoor(int index)
    {
        if(hiddenDoors.Count > 0)
        {
            
            Destroy(hiddenDoors[index]);
            //hiddenDoors.RemoveAt(index);
        }
        
    }

}
