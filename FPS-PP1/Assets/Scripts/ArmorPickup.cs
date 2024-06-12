using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject player;
    [Range(1,2)][SerializeField] int armorType;
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
            if(playerController.currentArmor < playerController.MaxArmor) 
            {
                if(armorType == 1)
                {

                    playerController.currentArmor = playerController.MaxArmor;
                    Destroy(gameObject);
                    playerController.updatePlayerUI();
                }
                if(armorType == 2)
                {
                    playerController.currentArmor += playerController.MaxArmor/2;
                    Destroy(gameObject);
                    playerController.updatePlayerUI();
                }
            }
            if(playerController.currentArmor > playerController.MaxArmor)
            {
                playerController.currentArmor = playerController.MaxArmor;
            }
        }
    }
}
