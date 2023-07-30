using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private GameObject MainMenu;
    private GameObject AudioSettings;

    void Start()
    {
        MainMenu = GameObject.Find("Start Menu");
        AudioSettings = GameObject.Find("SoundMenu");
        ChangeToMainSettings();
    }

    public void ChangeToMainSettings()
    {
        AudioSettings.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ChangeToAudioSettings()
    {        
        AudioSettings.SetActive(true);
        MainMenu.SetActive(false);
    }

}
