using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] TMP_Text totalEnemyCountText;

    [SerializeField] int level;
    
    public GameObject menuCredits;
    public GameObject playerFlashDamage;
    public Image playerHPBar;
    public Image playerArmorBar;
    public PlayerController playerScript;
    public GameObject playerSpawnPos;
    public GameObject player;
    public TMP_Text CurrentAmmoText;
    public TMP_Text MaxAmmoText;
    public TMP_Text TotalAmmoText;
    public TMP_Text ItemInfoText;
    public GameObject ItemInfoDisplay;
    
    public GameObject checkpointPopup;

    Doors doorScript;
    GameObject hiddenDoor;

    Animator winAnimation;
    public Animator loseAnimation;

    public int totalEnemyCount;
    public bool isPaused;
    public List<GameObject> totalEnemiesInScene;
    
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

       
        totalEnemiesInScene = new List<GameObject>();
        totalEnemiesInScene.Add(GameObject.FindWithTag("EnemyAI"));

        Time.timeScale = 1;

        hiddenDoor = GameObject.FindWithTag("Hidden Door");
        doorScript = hiddenDoor.GetComponent<Doors>();
        if(doorScript == null)
        {
            return;
        }
        

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
    }

    public void StatePause()
    {
        isPaused = !isPaused;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
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

    public void UpdateGameGoalWin(int amount)
    {
        totalEnemyCount += amount;
        totalEnemyCountText.text = totalEnemyCount.ToString("F0");
        
        if (totalEnemyCount <= 0 && level == 2)
        {
            StatePause();
            menuActive = menuWin;
            winAnimation.SetTrigger("WinTrigger");
        }
        if(totalEnemyCount <= 20)
        {
            if (doorScript != null)
            {
                doorScript.OpenHiddenDoor();
            }
        }
        if (totalEnemyCount <= 0)
        {
            if (doorScript != null)
            {
                doorScript.OpenHiddenDoor();
            }
        }
    }

    public void StateLose()
    {
        StatePause();
        menuActive = menuLose;
    }


   
    
}