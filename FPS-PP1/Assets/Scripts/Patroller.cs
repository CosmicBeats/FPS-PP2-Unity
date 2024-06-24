using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] int speed;

    [SerializeField] int waypointIndex;
    [SerializeField] float dist;

    
   /* void Start()
    {
        //starts on the first index
        waypointIndex = 0;
        //ai will look at the way point 
        //transform.LookAt(waypoints[waypointIndex].position);
    }

   
    void Update()
    {
       *//* dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f) { }
        {
            IncreaseIndex();
        }*//*
    }
    private void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
            transform.LookAt(waypoints[waypointIndex].position);
        }
    }*/
}
