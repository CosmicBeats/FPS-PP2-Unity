using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//added UnityEngine.SceneManagement

public class ButtonFunc : MonoBehaviour
{
    [SerializeField] Animator transistion;
    public float TransitionTime;


    private void Start()
    {
        transistion.SetTrigger("End");
        transistion.speed = 1;
    }
    //PAUSE MENU
    public void resume()
    {
        GameManager.instance.StateUnPause();

    }

    public void Respawn()
    {
        GameManager.instance.playerScript.SpawnPlayer();
        GameManager.instance.StateUnPause();
    }

    public void restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.StateUnPause();

    }

    public void quit() //THIS QUITS TO MAIN MENU
    {
        StartCoroutine(Transtioner(0));
    }

    //MAIN MENU

    public void PlayGame()
    {
        StartCoroutine(Transtioner(1));
        
    }

    public void Credits()
    {
        StartCoroutine(PlayCredits());
    }

    public void CloseCredits()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();

#endif
    }

    //TRANSITIONERS (They transition and under varying circumstances)

    IEnumerator Transtioner()
    {
        Time.timeScale = 0.1f;
        transistion.SetTrigger("Same Scene");
        transistion.speed = 10;

        yield return new WaitForSeconds(TransitionTime * Time.timeScale);

    }

    IEnumerator Transtioner(int SceneIndex)
    {
        Time.timeScale = 0.1f;
        transistion.SetTrigger("Start");
        transistion.speed = 10;

        yield return new WaitForSeconds(TransitionTime * Time.timeScale);

        SceneManager.LoadScene(SceneIndex);
    }

    IEnumerator Transtioner(string SceneName)
    {
        Time.timeScale = 0.1f;
        transistion.SetTrigger("Start");
        transistion.speed = 10;

        yield return new WaitForSeconds(TransitionTime * Time.timeScale);

        SceneManager.LoadScene(SceneName);
    }

    IEnumerator PlayCredits()
    {
        StartCoroutine(Transtioner());

        yield return new WaitForSeconds(TransitionTime * Time.timeScale);

        MainMenuManager.instance.menuCredits.SetActive(true);

    }
}
