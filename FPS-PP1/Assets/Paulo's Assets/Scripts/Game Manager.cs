using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{

    //Be sure to link the public and serialize fields and uncomment the lines of code when
    //necessary and create an empty object in unity named "Game Manager" to assign the script to. - Paulo
    public static GameManager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] TMP_Text enemyCountText;

    public GameObject playerFlashDamage;
    public Image playerHPBar;
    // Uncomment the line below to add the player controller - Paulo
    //public PlayerController playerScript;
    public GameObject player;
    int enemyCount;
    bool isPaused;

   
    void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
        //Uncomment the line below for player controller script - Paulo
        //playerScript = player.GetComponent<PlayerController>();
    }

    
    void Update()
    {
        // Toggles pause menu on and off with escape key
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                menuActive = menuPause;
                StatePause();
            }
            else
            {

                StateUnPause();
            }

        }
    }

    //
    public void StatePause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        menuPause.SetActive(true);
    }
    public void StateUnPause()
    {

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        menuPause.SetActive(false);
        menuActive = null;
    }

    public void UpdateGameGoalWin(int amount)
    {
        enemyCount += amount;
        enemyCountText.text = enemyCount.ToString("F0");

        if (enemyCount <= 0)
        {
            menuActive = menuWin;
            StartCoroutine(AnimateMenus());

        }
    }
    public void StateLose()
    {
        menuActive = menuLose;
        StartCoroutine(AnimateMenus());

    }

    IEnumerator AnimateMenus()
    {
        if (menuActive == menuWin)
        {
            menuWin.transform.LeanMoveLocalY(0, 1).setEaseOutQuart();
            yield return new WaitForSeconds(1);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        else if (menuActive == menuLose)
        {
            menuLose.transform.LeanMoveLocalY(0, 1).setEaseOutQuart();
            yield return new WaitForSeconds(1);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}