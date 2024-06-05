using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadBehaviour : MonoBehaviour
{
    [SerializeField] Doors door;
    [SerializeField] GameObject requiredKey;
    [SerializeField] bool isStartingDoor;
    [SerializeField] GameObject keypadCanvas;
    [SerializeField] TMP_Text keypadText;
    [SerializeField] AudioSource accessGranted, accessDenied, audioClick;

    Image imageColor;
    bool playerInRange;

    private void Start()
    {
        
        imageColor = keypadCanvas.GetComponent<Image>();
        
        
    }


    private void Update()
    {
        if (playerInRange && Input.GetButton("Interact"))
        {
            if (HasRequiredKey(requiredKey))
            {
                door.OpenDoor();
                
                imageColor.color = Color.green;
                audioClick.Play();
                accessGranted.PlayDelayed(0.5f);
                keypadText.text = "Granted";
            }
            else if(isStartingDoor)
            {
                door.OpenDoor();

                imageColor.color = Color.green;
                audioClick.Play();
                accessGranted.PlayDelayed(0.5f);
                keypadText.text = "Granted";
            }
            else
            {
                imageColor.color = Color.red;
                audioClick.Play();
                accessDenied.PlayDelayed(0.5f);
                keypadText.text = "Denied";
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
    public bool HasRequiredKey(GameObject keyRequired)
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
