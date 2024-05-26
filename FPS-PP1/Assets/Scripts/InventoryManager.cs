using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static InventoryManager;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
  
    public List<Keys> allKeys = new List<Keys>();

    private void Awake()
    {

        instance = this;
    }



    public void AddKey(Keys key)
    {
        if (!allKeys.Contains(key))
        {
            allKeys.Add(key);
        }
    }

    public void RemoveKey(Keys key)
    {
        if (allKeys.Contains(key))
        {
            allKeys.Remove(key);
        }
    }

    public enum Keys
    {
        //Add more keys later
        firstBuildingKey,
        secondBuildingKey,
        thirdBuildingKey,
    }

    
}
