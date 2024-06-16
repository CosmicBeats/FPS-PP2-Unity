using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcethrow : MonoBehaviour
{
    [SerializeField] float throwForce;

    [SerializeField] int MinHold;
    [SerializeField] int MaxGrenade;

    [SerializeField] GameObject grenadeP;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadeP,transform.position, transform.rotation);
        Rigidbody gb = grenade.GetComponent<Rigidbody>();
        gb.AddForce(transform.forward *  throwForce, ForceMode.VelocityChange);
    }
}
