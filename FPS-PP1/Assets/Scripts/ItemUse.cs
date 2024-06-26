using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject player;
    [SerializeField] GameObject item;
    [SerializeField] MeshRenderer pls;
    [SerializeField] ParticleSystem sparks;
    bool isInRange;
    bool hasBeenPlaced = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        pls.enabled = false;
    }

    private void Update()
    {
        if (isInRange && Input.GetButtonDown("Interact"))
        {
            pls.enabled = true; 
            hasBeenPlaced = true;
            playerController.itemsPlaced++;
            sparks.Stop();
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (playerController.itemPickups.Contains(item))
        {
            
            if ((!hasBeenPlaced))
            {
            
                if (other.CompareTag("Player"))
                {
                     isInRange = true;
                }

                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            
        }
    }
}
