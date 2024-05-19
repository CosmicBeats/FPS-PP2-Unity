using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.GraphicsBuffer;

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
    

    int totalEnemyCount;
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
            }
            else
            {
                StateUnPause();
            }
        }
    }

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

    public void UpdateGameGoalWin(int totalAmount)
    {
        totalEnemyCount += totalAmount;
        totalEnemyCountText.text = totalEnemyCount.ToString("F0");

        if (totalEnemyCount <= 0)
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