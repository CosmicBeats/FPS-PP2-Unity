using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] GunStats gun;

    bool IsPlayerinRange;

    // Start is called before the first frame update
    void Start()
    {
        gun.currentAmmo = gun.maxAmmo;
        gun.totalAmmo = gun.totalAmmoStash;
    }

    private void Update()
    {
        if (IsPlayerinRange && Input.GetButtonDown("Interact"))
        {
            GameManager.instance.playerScript.GetGunStats(gun);
            Destroy(gameObject);
            GameManager.instance.ItemInfoDisplay.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerinRange = true;
            GameManager.instance.ItemInfoDisplay.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            IsPlayerinRange = false;
            GameManager.instance.ItemInfoDisplay.SetActive(false);

        }
    }
}
