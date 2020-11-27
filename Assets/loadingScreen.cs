using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingScreen : MonoBehaviour
{
    public GameObject LoadingScreen;
    public void ActivateLoadScreen()
    {
        LoadingScreen.SetActive(true);
    }
}
