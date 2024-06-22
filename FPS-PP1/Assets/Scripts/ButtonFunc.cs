using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//added UnityEngine.SceneManagement

public class ButtonFunc : MonoBehaviour
{
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

    public void quit()
    {
        SceneManager.LoadScene(0);

/*#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();

#endif*/

    }

    //MAIN MENU

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Cursor.visible = false;
    }

    public void Credits()
    {
        MainMenuManager.instance.menuCredits.SetActive(true);
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
}
