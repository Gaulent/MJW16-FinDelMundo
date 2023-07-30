using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    void Start()
    {
        ChangeToMainSettings();
    }

    public void ChangeToMainSettings()
    {
        GameObject.Find("SoundMenu").SetActive(false);
        GameObject.Find("Start Menu").SetActive(true);
    }

    public void ChangeToAudioSettings()
    {        
        GameObject.Find("Start Menu").SetActive(false);
        GameObject.Find("SoundMenu").SetActive(true);
    }

}
