using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    PlayerController playerCrontrol;
    [SerializeField] GameObject playerT;

    AudioSource DashaudioSource;
    ParticleSystem teleParticle;

    [SerializeField] AudioSource audport;
    [SerializeField] AudioClip[] audDash;
    [Range(0, 1)][SerializeField] float audDashVol;

    public float distance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        playerCrontrol = playerT.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Teleport"))
        {
            StartCoroutine(Teleporter());
        } 
    }

    public void blinkForward()
    {
        RaycastHit solid;
        Vector3 destination = transform.position + transform.forward * distance;
        if(Physics.Linecast(transform.position, destination, out solid)) 
        {
            audport.PlayOneShot(audDash[Random.Range(0, audDash.Length)], audDashVol);
            teleParticle.Play();
            destination = transform.position + transform .forward * (solid.distance -1f);
        }
        if(Physics.Raycast(destination, -Vector3.up, out solid))
        {
            destination.y = -0.5f;
            destination = solid.point;
            transform.position = destination;
        }
    }

    IEnumerator Teleporter()
    {
        //disables the player 
        playerCrontrol.disable = true;
        yield return new WaitForSeconds(0.5f);
        //teleports
        blinkForward();
        //playerT.transform.position = Vector3.zero;
        //reactivate player controls
        yield return new WaitForSeconds(0.5f);
        playerCrontrol.disable = false;
    }
}
