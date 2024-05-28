using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    [SerializeField]  PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(playerController.currentArmor < playerController.MaxArmor) 
            {
                playerController.currentArmor = playerController.MaxArmor;
                Destroy(gameObject);
            }
        }
    }
}
