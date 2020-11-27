using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerOfScenes : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
       // Look.cursorLocked = false;
    }

    public void PlayGame()
    {
        //Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadControlsScene()
    {
        SceneManager.LoadScene(2);
    }

}
