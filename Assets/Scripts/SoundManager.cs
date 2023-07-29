using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : MonoBehaviour, ISoundManager
{
    // Start is called before the first frame update
    public bool PlaySFX(ESFXType sfxSound)
    {
        FMOD.Studio.EventInstance heal = FMODUnity.RuntimeManager.CreateInstance(GetID(sfxSound));
        //heal.setParameterByID(fullHealthParameterId, restoreAll ? 1.0f : 0.0f);
        heal.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        heal.start();
        return heal.release().HasFlag(FMOD.RESULT.OK);
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
