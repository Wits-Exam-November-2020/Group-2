using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenu;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        player = GameObject.Find("Player(Clone)");
    }

    // Update is called once per frame
    void Update()
    {

       
        
        if (Input.GetKeyDown(KeyCode.Escape) && !player.GetComponent<Player>().dead)
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

    public void RestartGame()
    {
        Time.timeScale = 1;
        Look.cursorLocked = true;
        SceneManager.LoadScene(1);
    }

}
