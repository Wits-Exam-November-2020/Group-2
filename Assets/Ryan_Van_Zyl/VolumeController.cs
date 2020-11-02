using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider globalVolume;
    public Slider musicVolume;
    private GameObject musicController;
    private AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        musicController = GameObject.Find("MusicController");
        music = musicController.GetComponent<AudioSource>();
        globalVolume.value = AudioListener.volume;
        musicVolume.value = music.volume;

    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = globalVolume.value;
        music.volume = musicVolume.value;

    }

}
