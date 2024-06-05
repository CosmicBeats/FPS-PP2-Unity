using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static InventoryManager;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
  
    public List<GameObject> allKeys = new List<GameObject>();

    private void Awake()
    {

        instance = this;
    }



    public void AddKey(GameObject key)
    {
        if (!allKeys.Contains(key))
        {
            allKeys.Add(key);
        }
    }

    public void RemoveKey(GameObject key)
    {
        if (allKeys.Contains(key))
        {
            allKeys.Remove(key);
        }
    }

}
