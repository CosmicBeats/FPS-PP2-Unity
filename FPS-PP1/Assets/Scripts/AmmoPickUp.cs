using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject player;
    
    [Range(1, 2)][SerializeField] int ammoType;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerController.gunStats.totalAmmo < playerController.gunStats.totalAmmoStash)
            {
                if (ammoType == 1)
                {
                    playerController.gunStats.totalAmmo = playerController.gunStats.totalAmmoStash;

                    Destroy(gameObject);
                    
                }
                if (ammoType == 2)
                {
                    playerController.gunStats.totalAmmo += playerController.gunStats.totalAmmoStash/2;

                    Destroy(gameObject);
                    
                }
            }

            if(playerController.gunStats.totalAmmo > playerController.gunStats.totalAmmoStash)
            {
                playerController.gunStats.totalAmmo = playerController.gunStats.totalAmmoStash;
            }
        }
    }
}
