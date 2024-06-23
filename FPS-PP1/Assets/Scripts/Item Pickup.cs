using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject player;
    bool hasBeenCollected;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasBeenCollected==false)
        {
            playerController.itemPickups.Add(gameObject);
            gameObject.SetActive(false);
            hasBeenCollected = true;

        }
    }
}
