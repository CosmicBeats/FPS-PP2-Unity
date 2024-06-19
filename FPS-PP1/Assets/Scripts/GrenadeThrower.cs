using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcethrow : MonoBehaviour
{
    //throw force is for travel
    [SerializeField] int throwForce;
    //this if for hieght
    [SerializeField] int ThrowHieght;
    //gravity
    [SerializeField] int throwGravity;

    /*[SerializeField] int MinHold;*/
    [SerializeField] int playerHold;
    [SerializeField] int MaxGrenade;

    Vector3 GrenadeVelocity;
    //[SerializeField] int ThrowSpeed;

    [SerializeField] GameObject grenadeP;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Grenade") && playerHold < MaxGrenade)
        {
            ThrowGrenade();
            GrenadeVelocity.y = throwForce;
            GrenadeVelocity.y -= throwGravity * Time.deltaTime;
            playerHold--;
          // GrenadeVelocity.y = ThrowSpeed;
            GameManager.instance.GrenadeAmmoText.text = playerHold.ToString("F0");
        }

    }

    void ThrowGrenade()
    {
        GrenadeVelocity = Vector3.zero;
       
        GameObject grenade = Instantiate(grenadeP,transform.position, transform.rotation);
        Rigidbody gb = grenade.GetComponent<Rigidbody>();
        gb.velocity = transform.forward * throwForce;
        /*GrenadeVelocity.y = throwForce;
        GrenadeVelocity.y -= throwGravity * Time.deltaTime;*/
        gb.AddForce(transform.forward *  throwForce, ForceMode.VelocityChange);
    }
}
