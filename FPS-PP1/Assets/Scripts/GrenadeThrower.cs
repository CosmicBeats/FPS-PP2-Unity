using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Forcethrow : MonoBehaviour
{
    //throw force is for travel
    [SerializeField] int throwForce;
    //this if for hieght
    [SerializeField] float ThrowHieght;
    //gravity
    [SerializeField] int throwGravity;

    /*[SerializeField] int MinHold;*/
    [SerializeField] int playerHold;
    [SerializeField] int MaxGrenade;

    //starting postion is needded 
    
    Vector3 GrenadeVelocity;
    //[SerializeField] int ThrowSpeed;
    [SerializeField] int delayTime;
    [SerializeField] GameObject grenadeP;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Grenade") && playerHold < MaxGrenade)
        {
            ThrowGrenade();
            GrenadeVelocity.y = transform.forward.y * ThrowHieght;
            GrenadeVelocity.x = transform.forward.x * throwForce;
            //GrenadeVelocity.y = transform.forward.y * ThrowHieght;
            GrenadeVelocity.y = throwGravity;
            playerHold--;
          // GrenadeVelocity.y = ThrowSpeed;
            GameManager.instance.GrenadeAmmoText.text = playerHold.ToString("F0");
        }
        GrenadeVelocity.y -= throwForce * Time.deltaTime;
        
        
    }
    //collistion for the grenade is not here

    void ReverseGround()
    {

    }
    void ThrowGrenade()
    {
        //if(grenadeP )
        GrenadeVelocity = Vector3.zero;
       
        GameObject grenade = Instantiate(grenadeP,transform.position, transform.rotation);
        Rigidbody gb = grenade.GetComponent<Rigidbody>();
        gb.velocity = transform.forward * throwForce;
        //throwForce * -1;
        GrenadeVelocity.y = throwForce;
        GrenadeVelocity.x = throwForce;
        //multi the throwspeed by -1
        GrenadeVelocity.y -= throwForce * Time.deltaTime;
        gb.AddForce(transform.forward *  throwForce, ForceMode.VelocityChange);
        Destroy(grenade,delayTime);
    }
}
