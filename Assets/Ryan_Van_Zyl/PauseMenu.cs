using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {    

        pauseMenu.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {

       
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

       
    }


    private void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Resume()
    {
        Look.cursorLocked = true;
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

}
