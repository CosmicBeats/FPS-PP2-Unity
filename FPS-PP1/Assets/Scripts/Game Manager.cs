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
    
    

    public GameObject playerFlashDamage;
    public Image playerHPBar;
    public Image playerArmorBar;
    public PlayerController playerScript;
    public GameObject playerSpawnPos;
    public GameObject player;
    
    public GameObject checkpointPopup;
    

    private List<GameObject> totalEnemies;
    public List<GameObject> localEnemies;
    [SerializeField] LocalTrigger[] localTriggers;

    

    Doors doorScript;
    GameObject hiddenDoor;

    public LocalTrigger triggerScript;
    [SerializeField] GameObject trigger;

    


    Animator winAnimation;
    public Animator loseAnimation;

    public int totalEnemyCount;
    public bool isPaused;
   

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

        localEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyAI"));

        totalEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyAI"));

        //trigger = GameObject.FindWithTag("Local Trigger");
        //triggerScript = trigger.GetComponent<LocalTrigger>();

        Time.timeScale = 1;

        //hiddenDoor = GameObject.FindWithTag("Hidden Door");
        //doorScript = hiddenDoor.GetComponent<Doors>();

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

    public void UpdateGameGoalWin()
    {
        totalEnemyCount = totalEnemies.Count;
        totalEnemyCountText.text = totalEnemyCount.ToString("F0");

        if (totalEnemyCount <= 0)
        {
            StatePause();
            menuActive = menuWin;
            winAnimation.SetTrigger("WinTrigger");
        }
        else if(totalEnemyCount <= 4)
        {
            doorScript.OpenHiddenDoor();
        }
        
    }


    public void AddEnemy(GameObject enemy)
    {
        if(!totalEnemies.Contains(enemy))
        {
            totalEnemies.Add(enemy);
            UpdateGameGoalWin();
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (totalEnemies.Contains(enemy))
        {
            totalEnemies.Remove(enemy);
            UpdateGameGoalWin();
        }
    }

    public void EnemyDefeated(GameObject enemy)
    {
        RemoveEnemy(enemy);
    }

    public void StateLose()
    {
        StatePause();
        menuActive = menuLose;
    }
   
    
}