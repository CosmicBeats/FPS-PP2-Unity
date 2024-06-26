using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    bool playerInRange;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetButton("Interact") && playerController.itemsPlaced == 2)
        {
            GameManager.instance.StatePause();
            GameManager.instance.menuActive = GameManager.instance.menuWin;
            GameManager.instance.winAnimation.SetTrigger("WinTrigger");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.displayWin.SetActive(true);
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.displayWin.SetActive(false);
            playerInRange = false;
        }
    }
}
