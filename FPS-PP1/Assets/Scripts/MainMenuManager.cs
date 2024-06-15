using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuMain;
   

    public GameObject menuCredits;


    // Start is called before the first frame update
    void Awake()
    {
        menuActive = menuMain;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
