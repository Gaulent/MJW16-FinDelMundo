using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class SoundManager : MonoBehaviour, ISoundManager
{
    private EventInstance audioVideo;
    private PLAYBACK_STATE status;

    // Start is called before the first frame update
    public bool PlaySFX(ESFXType sfxSound, GameObject originSound)
    {
        EventInstance heal = RuntimeManager.CreateInstance("event:/"+GetID(sfxSound));
        //heal.setParameterByID(fullHealthParameterId, restoreAll ? 1.0f : 0.0f);
        heal.set3DAttributes(RuntimeUtils.To3DAttributes(originSound));
        heal.start();
        return heal.release().HasFlag(FMOD.RESULT.OK);
    }

    public bool PlayAudioFromVideo(string filename, GameObject originSound)
    {
        audioVideo.getPlaybackState(out status);
        if(status == PLAYBACK_STATE.PLAYING)
        {
            audioVideo.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        
        audioVideo = RuntimeManager.CreateInstance("event:/"+filename.Split(".")[0]);
        //heal.setParameterByID(fullHealthParameterId, restoreAll ? 1.0f : 0.0f);
        audioVideo.set3DAttributes(RuntimeUtils.To3DAttributes(originSound));
        audioVideo.start();
        return audioVideo.release().HasFlag(FMOD.RESULT.OK);

    }

    public bool PlaySFX(ESFXType sfxSound)
    {
        return PlaySFX(sfxSound, this.gameObject);
    }

    private string GetID(ESFXType sfxSound)
    {
        switch(sfxSound)
        {
            case ESFXType.BrokenGlass: return "BrokenGlass";
            case ESFXType.Carry: return "Carry";
            case ESFXType.Jump: return "Jump";
            case ESFXType.Like: return "Like";
        }
        return "";
    }

}
