using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] InventoryManager.Keys keyType;

    bool isPlayerInRange;

    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Interact"))
        {
            InventoryManager.instance.AddKey(keyType);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    public enum KeyType
    {
        firstBuildingKey,
        secondBuildingKey,
        thirdBuildingKey,
       
    }
}
