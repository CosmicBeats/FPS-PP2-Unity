using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Range(0, 1)][SerializeField] float healRate; // Time in seconds between each heal tick
    [SerializeField] int healPoints;
    bool isPlayerInRange;
    PlayerController playerController;
    Coroutine healCoroutine;

    void Start()
    {
        playerController = GameManager.instance.playerScript;
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKey(KeyCode.E))
        {
            if (healCoroutine == null)
            {
                healCoroutine = StartCoroutine(HealOverTime());
            }
        }
        else
        {
            if (healCoroutine != null)
            {
                StopCoroutine(healCoroutine);
                healCoroutine = null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (healCoroutine != null)
            {
                StopCoroutine(healCoroutine);
                healCoroutine = null;
            }
        }
    }

    IEnumerator HealOverTime()
    {
        while (true)
        {
            playerController.Heal(healPoints);
            yield return new WaitForSeconds(healRate);
        }
    }
}