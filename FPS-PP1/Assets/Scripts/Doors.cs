using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Vector3 doorClosedPosition;
    Vector3 doorOpenPosition;
    [SerializeField] Animator doorAnimator; 

    //door audio
    //[SerializeField] AudioSource aud;
    //[SerializeField] AudioClip[] audOpen;
    //[Range(0, 1)][SerializeField] float audOpenVol;



    void Start()
    {

       

    }

   

    public void OpenDoor()
    {

        doorAnimator.SetBool("Opening", true);
        
    }
}

