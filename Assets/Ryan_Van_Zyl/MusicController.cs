using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private bool wave2Started;
    private bool volumeChanged;
    public AudioSource musicSource;
    private float mainVolume;

    private void Awake()
    {
        mainVolume = musicSource.volume;
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
            musicSource.volume = mainVolume;
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 && !volumeChanged)
        {
            NewEnemySpawner waveManager = GameObject.Find("Manager").GetComponent<NewEnemySpawner>();
            if (waveManager.currentWave == 2 && !volumeChanged)
            {
                wave2Started = true;
            }
            if (wave2Started)
            {
                volumeChanged = true;
                wave2Started = false;
                musicSource.volume += 0.04f;
            }

        }
        
    }

    
}
