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
    [SerializeField] string sceneName;
    


    void Start()
    {
        
        levelLoaderCountDown = 3;  
    }

    
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && levelLoaderCountDown > 0)
        {
            countDownTextLabel.SetActive(true);
            levelLoaderCountDown -= Time.deltaTime;
            countDownText.text = levelLoaderCountDown.ToString("F1");
        }
        else if(other.CompareTag("Player") && levelLoaderCountDown <= 0 && GameManager.instance.totalEnemyCount <= 0)
        {
            SceneManager.LoadScene(sceneName);
            
            countDownTextLabel.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            countDownTextLabel.SetActive(false);
            levelLoaderCountDown = 3;
        }
    }
}
