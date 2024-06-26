using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text displayShipPartsText;
    [SerializeField] GameObject displayShipParts;
   
    bool hasBeenCollected;
    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        

    }
    void Update()
    {
        if(playerInRange && Input.GetButtonDown("Interact"))
        {
            playerController.itemPickups.Add(gameObject);
            gameObject.SetActive(false);
            displayShipParts.SetActive(false);
            hasBeenCollected = true;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasBeenCollected==false)
        {
            playerInRange = true;
            displayShipParts.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange=false;
            displayShipParts.SetActive(false);
        }
    }
}
