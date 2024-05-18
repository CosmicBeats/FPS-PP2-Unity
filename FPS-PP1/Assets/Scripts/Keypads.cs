using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadBehaviour : MonoBehaviour
{
    [SerializeField] Doors door;

    [SerializeField] InventoryManager.Keys requiredKey;

    bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetButton("Interact"))
        {
            if (HasRequiredKey(requiredKey))
            {
                door.OpenDoor();
            }
            else
            {
                //Add UI Later
                Debug.Log("You do not have the key for this door!!!");
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    public bool HasRequiredKey(InventoryManager.Keys keyRequired)
    {
        if (InventoryManager.instance.allKeys.Contains(keyRequired))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
