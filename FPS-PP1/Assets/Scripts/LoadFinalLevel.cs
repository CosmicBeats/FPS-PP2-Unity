using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadFinalLevel : MonoBehaviour
{
    float levelLoaderCountDown;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] GameObject countDownTextLabel;
    


    void Start()
    {
        
        levelLoaderCountDown = 5;  
    }

    
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && levelLoaderCountDown > 0)
        {
            countDownTextLabel.SetActive(true);
            levelLoaderCountDown -= Time.deltaTime;
            countDownText.text = levelLoaderCountDown.ToString("F1");
        }
        else if(other.CompareTag("Player") && levelLoaderCountDown <= 0)
        {
            SceneManager.LoadScene("MainScene 2");
            
            countDownTextLabel.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            countDownTextLabel.SetActive(false);
            levelLoaderCountDown = 5;
        }
    }
}
