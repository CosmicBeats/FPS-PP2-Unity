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
            GameManager.instance.ItemInfoDisplay.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            GameManager.instance.ItemInfoDisplay.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            GameManager.instance.ItemInfoDisplay.SetActive(false);
        }
    }

    public enum KeyType
    {
        firstBuildingKey,
        secondBuildingKey,
        thirdBuildingKey,
       
    }
}
