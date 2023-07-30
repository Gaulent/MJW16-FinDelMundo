using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD;
using FMODUnity;
using UnityEngine.Events;

public class VolumeManager : MonoBehaviour
{
    
    private Slider s_Master;
    private Slider s_BGM;
    private Slider s_Sfx;
    private Slider s_Video;

    float tmpVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        s_Master = GameObject.Find("Slider_Master").GetComponent<Slider>();
        s_BGM = GameObject.Find("Slider_BGM").GetComponent<Slider>();
        s_Sfx = GameObject.Find("Slider_SFX").GetComponent<Slider>();
        s_Video = GameObject.Find("Slider_Video").GetComponent<Slider>();

        RefreshVolumesStatus();

        s_Master.onValueChanged.AddListener(delegate{SetVolume("Master");});
        s_BGM.onValueChanged.AddListener(delegate{SetVolume("BGM");});
        s_Sfx.onValueChanged.AddListener(delegate{SetVolume("SFX");});
        s_Video.onValueChanged.AddListener(delegate{SetVolume("Videos");});
    }

    void Testing()
    {
        //FMODUnity.
    }

    void SetVolume(string volumetype)
    {
        Slider sliderToSet = null;
        switch(volumetype)
        {
            case "Master": sliderToSet = s_Master; break;
            case "BGM": sliderToSet = s_BGM; break;
            case "SFX": sliderToSet = s_Sfx; break;
            case "Video": sliderToSet = s_Video; break;
        }
        
        if(volumetype == "Master")
        {
            FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(sliderToSet.normalizedValue);
        }else{
            FMODUnity.RuntimeManager.GetVCA("vca:/"+volumetype).setVolume(sliderToSet.normalizedValue);
        }

        RefreshVolumesStatus();
    }

    void RefreshVolumesStatus()
    {
        string prefix = "vca:/";
        FMODUnity.RuntimeManager.GetBus("bus:/").getVolume(out tmpVolume);

        s_Master.SetValueWithoutNotify(tmpVolume);

        FMODUnity.RuntimeManager.GetVCA(prefix + "SFX").getVolume(out tmpVolume);
        s_Sfx.SetValueWithoutNotify(tmpVolume);
        FMODUnity.RuntimeManager.GetVCA(prefix + "BGM").getVolume(out tmpVolume);
        s_BGM.SetValueWithoutNotify(tmpVolume);
        FMODUnity.RuntimeManager.GetVCA(prefix + "Videos").getVolume(out tmpVolume);
        s_Video.SetValueWithoutNotify(tmpVolume);
    }

}
