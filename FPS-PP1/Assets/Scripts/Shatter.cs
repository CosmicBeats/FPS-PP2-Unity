using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    public GameObject destroyedVersion; //calling the destroyed object

    public void Destroy()
    {
        //spawns the object 
        Instantiate(destroyedVersion,transform.position,transform.rotation);
        // removes the current 
        Destroy(gameObject);
    }
}
