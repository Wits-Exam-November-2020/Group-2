using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private bool wave2Started;
    private bool volumeChanged;
    public AudioSource mainMusic;


    private float mainVolume;
    private NewEnemySpawner waveManager;

    private void Awake()
    {
        mainVolume = mainMusic.volume;
        wave2Started = false;
        volumeChanged = false;
        DontDestroyOnLoad(this.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        
    }

    public void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMusic.volume = mainVolume;
    
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 && !volumeChanged)
        {
            waveManager = GameObject.Find("Manager").GetComponent<NewEnemySpawner>();
            if (waveManager.currentWave == 2 && !volumeChanged)
            {
                wave2Started = true;
            }
            if (wave2Started)
            {
                volumeChanged = true;
                wave2Started = false;
                mainMusic.volume += 0.04f;
            }

        }

    
        
    }

    
}
