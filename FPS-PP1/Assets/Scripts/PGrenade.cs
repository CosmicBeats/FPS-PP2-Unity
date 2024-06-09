using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGrenade : MonoBehaviour
{
    [SerializeField] Rigidbody gb;

    [SerializeField] GameObject explosionEffect;
    //set a delay
    public float delay = 3f;
    [SerializeField] float Radius; 
    [SerializeField] float countdown;
    [SerializeField] bool hasExploded = false;
    [SerializeField] float force;

    [SerializeField] int damage;
    [SerializeField] int destroyTime;
   
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= delay;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        //Explostion has to Show Effects
        Instantiate(explosionEffect,transform.position, transform.rotation);
        // get nearby object and destroy them
        Collider[] colliderToDestroy = Physics.OverlapSphere(transform.position, Radius);
       
        foreach (Collider nearbyObject in colliderToDestroy)
        {
            //Damage 
            Shatter dest = nearbyObject.GetComponent<Shatter>();
            if(dest != null)
            {
                dest.Destroy();
            }
        }
        Collider[] colliderToMove = Physics.OverlapSphere(transform.position, Radius);
        //add force to the object and move them 
        foreach(Collider nearbyObject in colliderToMove)
        {
            //Add Force 
            Rigidbody gb = nearbyObject.GetComponent<Rigidbody>();
            if (gb != null)
            {
                gb.AddExplosionForce(force, transform.position, Radius);
            }

        }
        // get nearby object


        Destroy(gameObject);
    }
}
