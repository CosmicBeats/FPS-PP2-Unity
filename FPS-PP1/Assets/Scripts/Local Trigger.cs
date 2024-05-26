using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalTrigger : MonoBehaviour
{
    [SerializeField] TMP_Text localEnemyCountText;
    [SerializeField] GameObject localEnemyCountLabel;

    public int localEnemyCount;

    private void Start()
    {
        UpdateEnemyCountText();
    }

    public void UpdateEnemyCountText()
    {
        localEnemyCountText.text = localEnemyCount.ToString("F0");
        localEnemyCountLabel.SetActive(localEnemyCount > 0); // Hide label if count is zero
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {      
            localEnemyCountLabel.SetActive(true);
            localEnemyCount = GameManager.instance.localEnemies.Count - 1;
            localEnemyCountText.text = localEnemyCount.ToString("F0");
        }

        if(other.CompareTag("EnemyAI"))
        {
            GameObject enemy = other.gameObject;
            GameManager.instance.localEnemies.Add(enemy);
        }
    }

    // Called when a collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            localEnemyCountLabel.SetActive(false);
            localEnemyCount = 0;
            localEnemyCountText.text = localEnemyCount.ToString("F0");
        }

        if (other.CompareTag("EnemyAI"))
        {
            GameObject enemy = other.gameObject;
            GameManager.instance.localEnemies.Remove(enemy);
        }


    }

    
}
