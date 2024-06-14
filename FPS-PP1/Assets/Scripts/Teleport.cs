using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    PlayerController playerCrontrol;
    // Start is called before the first frame update
    void Start()
    {
        playerCrontrol = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(Teleporter());
        } 
    }

    IEnumerator Teleporter()
    {
        //disables the player 
        playerCrontrol.disable = true;
        yield return new WaitForSeconds(0.5f);
        //teleports
        gameObject.transform.position = Vector3.zero;
        //reactivate player controls
        yield return new WaitForSeconds(0.5f);
        playerCrontrol.disable = false;
    }
}
