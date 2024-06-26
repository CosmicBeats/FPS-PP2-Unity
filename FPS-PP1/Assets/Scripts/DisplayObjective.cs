using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObjective : MonoBehaviour
{
    bool isPlayerInRange;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

        }
    }
}
