using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGrenade : MonoBehaviour
{//throwforce needs to be its own script for the player 
    [SerializeField] Rigidbody gb;
    [SerializeField] Renderer[] model;


    [SerializeField] ParticleSystem explosionEffect;
    //disabling the collision
    public bool disable = false;
    //set a delay
   /* public int delay = 2;*/
    //[SerializeField] int delay;
    [SerializeField] int Radius; 
    [SerializeField] float countdown;
   // bool hasExploded = false;
    [SerializeField] float force;

    [SerializeField] int damage;
    [SerializeField] int destroyTime;
 
    void Start()
    {
        //countdown = delay;
        /*if (countdown <= 0f && !hasExploded)
       {
           //Explode();
           Invoke("Explode", countdown);
           //OnTriggerEnter();
           hasExploded = true;
       }*/
        Invoke("Explode", countdown);

    }

    // Update is called once per frame
    void Update()
    {
       /* countdown = delay;*/
       
        /*countdown -= .5f;*/

    }
    //trigger for the grenade 
    //it should travel at 3 floats
   
/*    public void OnTriggerEnter(Collider other)
    {
        //the trigger needs to be disable and then turn on when used
        //taking dmg apon entering
        if (!disable)
        {
            IDamage dmg = other.gameObject.GetComponent<IDamage>();
            //needs to look for and take dmg
            if (dmg != null)
            {
                Explode();
                dmg.TakeDamage(damage);
            }
        }
        *//*if (other.CompareTag("Player") || other.CompareTag("EnemyAI"))
        {
            //disable = true;

          
        }*//*
    }*/
    //if the trigger works this should not be needed
    public void OnCollisionEnter(Collision other)
     {

        //timer needs to end and damage anything in its radius
        //damage 
        if (!disable)
        {
            IDamage dmg = other.gameObject.GetComponent<IDamage>();
            //needs to look for and take dmg
            if (dmg != null)
            {
                //Explode();
               // dmg.TakeDamage(damage);
            }
        }
    }
    public void Knockback()
    {
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

    }
    void Explode()
    {
        //Explostion has to Show Effects
        Instantiate(explosionEffect,transform.position, transform.rotation);
        //explosionEffect.Play();
        // get nearby object and destroy them
        Collider[] colliderToDestroy = Physics.OverlapSphere(transform.position, Radius);

        foreach (Collider nearbyObject in colliderToDestroy)
        {
            //Damage 
            IDamage dest = nearbyObject.GetComponent<IDamage>();
            if(dest != null)
            {
                dest.TakeDamage(damage);
                Knockback();
            }
        }

        /* Collider[] colliderToMove = Physics.OverlapSphere(transform.position, Radius);
         //add force to the object and move them 
         foreach(Collider nearbyObject in colliderToMove)
         {
             //Add Force 
             Rigidbody gb = nearbyObject.GetComponent<Rigidbody>();
             if (gb != null)
             {
                 gb.AddExplosionForce(force, transform.position, Radius);
             }

         }*/
        // get nearby object

       
        //Destroy(explosionEffect,destroyTime);
    }
}
