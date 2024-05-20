using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] TMP_Text totalEnemyCountText;
    [SerializeField] TMP_Text enemyCountText;

    public GameObject playerFlashDamage;
    public Image playerHPBar;
    public PlayerController playerScript;
    public GameObject player;

    Doors doorScript;
   

    Animator winAnimation;
    public Animator loseAnimation;

    public int totalEnemyCount;
    public bool isPaused;
    int enemyCount; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        winAnimation = menuWin.GetComponent<Animator>();
        loseAnimation = menuLose.GetComponent<Animator>();
        
        doorScript = GameObject.Find("Hidden Door").GetComponent<Doors>();
        Time.timeScale = 1;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                menuActive = menuPause;
                StatePause();
                menuActive.SetActive(isPaused);
            }
            else if(menuActive == menuPause)
            {
                StateUnPause();
            }
        }
        // To be adjusted for other buildings

        //Adjust the number for enemies. 
        if (totalEnemyCount <= 1)
        {
            doorScript.OpenHiddenDoor(0);
        }
        //else if (totalEnemyCount <= 1)
        //{
        //    doorScript.OpenHiddenDoor(1);
        //}
        //else if (totalEnemyCount <= 1)
        //{
        //    doorScript.OpenHiddenDoor(2);
        //}
    }

    public void StatePause()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    public void StateUnPause()
    {
        isPaused = !isPaused;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(isPaused);
        menuActive = null;
    }

    public void UpdateGameGoalWin(int totalAmount)
    {
        totalEnemyCount += totalAmount;
        totalEnemyCountText.text = totalEnemyCount.ToString("F0");



        if (totalEnemyCount <= 0)
        {
            StatePause();
            menuActive = menuWin;


            winAnimation.SetTrigger("WinTrigger");
           
        }
        
    }
    
    public void StateLose()
    {
        StatePause();
        menuActive = menuLose;
    }
   
    
}