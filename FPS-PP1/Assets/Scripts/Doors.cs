using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    //door audio
    //[SerializeField] AudioSource aud;
    //[SerializeField] AudioClip[] audOpen;
    //[Range(0, 1)][SerializeField] float audOpenVol;

    [SerializeField] Animator doorAnimator;
    string currentState;
    //const string doorIdle = "Door_Idle";
    //const string doorOpen = "Door_Open";



    void Start()
    {
       
    }

   

    public void OpenDoor()
    {
       
        doorAnimator.SetBool("Opening", true);
        
    }

    public void OpenHiddenDoor()
    {
        
        //Destroy(hiddenDoors[0]);
        //hiddenDoors.RemoveAt(0);
        
        
    }
}

