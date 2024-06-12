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
            


            if (playerController.gunList[playerController.selectedGun].totalAmmo < playerController.gunList[playerController.selectedGun].totalAmmoStash)
            {
                if (ammoType == 1)
                {
                    playerController.gunList[playerController.selectedGun].totalAmmo = playerController.gunList[playerController.selectedGun].totalAmmoStash;

                    Destroy(gameObject);
                    GameManager.instance.TotalAmmoText.text = playerController.gunList[playerController.selectedGun].totalAmmo.ToString("F0");

                }
                if (ammoType == 2)
                {
                    playerController.gunList[playerController.selectedGun].totalAmmo += playerController.gunList[playerController.selectedGun].totalAmmoStash /2;

                    Destroy(gameObject);
                    GameManager.instance.TotalAmmoText.text = playerController.gunList[playerController.selectedGun].totalAmmo.ToString("F0");

                }
            }

            if(playerController.gunList[playerController.selectedGun].totalAmmo > playerController.gunList[playerController.selectedGun].totalAmmoStash)
            {
                playerController.gunList[playerController.selectedGun].totalAmmo = playerController.gunList[playerController.selectedGun].totalAmmoStash;
                GameManager.instance.TotalAmmoText.text = playerController.gunList[playerController.selectedGun].totalAmmo.ToString("F0");

            }
        }
    }
}
