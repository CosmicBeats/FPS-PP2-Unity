using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//added UnityEngine.SceneManagement

public class ButtonFunc : MonoBehaviour
{
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();

#endif

    }

    public void PlayGame()
    {
        GameManager.instance.StateUnPause();
        
    }
}
