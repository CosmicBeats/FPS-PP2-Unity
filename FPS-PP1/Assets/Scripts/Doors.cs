using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] MeshRenderer doorMesh;

    //door audio
    //[SerializeField] AudioSource aud;
    //[SerializeField] AudioClip[] audOpen;
    //[Range(0, 1)][SerializeField] float audOpenVol;
    

    void Start()
    {

       
    }

   

    public void OpenDoor()
    {
        doorMesh.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false ;  
    }
}

