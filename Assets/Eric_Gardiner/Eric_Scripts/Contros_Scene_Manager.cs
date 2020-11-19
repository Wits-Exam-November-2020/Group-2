using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contros_Scene_Manager : MonoBehaviour
{
    public GameObject ActiveMenu;
    public GameObject[] Pages;
    // 0 controls
    // 1 story
    // 2 wiki
    // 3 credits

    void LoadPage(int page)
    {
        Pages[page].SetActive(true);
        ActiveMenu.SetActive(false);
        ActiveMenu = Pages[page];
    }
    public void Open_Story()
    {
        LoadPage(1);
    }
    public void Open_Wiki()
    {
        LoadPage(2);
    }
    public void Open_Credits()
    {
        LoadPage(3);
    }

    public void Return_to_Controls()
    {
        LoadPage(0);
    }
}
