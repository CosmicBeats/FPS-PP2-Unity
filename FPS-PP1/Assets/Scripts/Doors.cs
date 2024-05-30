using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Vector3 doorClosedPosition;
    Vector3 doorOpenPosition;
    List<GameObject> hiddenDoors = new List<GameObject>();

    //door audio
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] audOpen;
    [Range(0, 1)][SerializeField] float audOpenVol;



    void Start()
    {
        hiddenDoors.Add(GameObject.FindWithTag("Hidden Door"));
        doorClosedPosition = transform.position;
        doorOpenPosition = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    }

   

    public void OpenDoor()
    {
       

        if (transform.position != doorOpenPosition)
        {
            aud.PlayOneShot(audOpen[Random.Range(0, audOpen.Length)], audOpenVol);
            transform.LeanMoveLocalY(3.5f, 1).setEaseInSine();
        }
    }

    public void OpenHiddenDoor()
    {
        if(hiddenDoors.Count > 0)
        {
            Destroy(hiddenDoors[0]);
            hiddenDoors.RemoveAt(0);
        }
        
    }
}

