using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class StartTitleMenu : MonoBehaviour
{
    EventInstance titleMenuScreenMusic;
    // Start is called before the first frame update
    void Start()
    {
        titleMenuScreenMusic = RuntimeManager.CreateInstance("event:/"+"pantallainicio");
        //heal.setParameterByID(fullHealthParameterId, restoreAll ? 1.0f : 0.0f);
        titleMenuScreenMusic.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        titleMenuScreenMusic.start();
        titleMenuScreenMusic.release();
    }

    void OnDestroy()
    {
        titleMenuScreenMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
