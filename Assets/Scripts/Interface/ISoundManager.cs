using UnityEngine;

public interface ISoundManager
{
        bool PlaySFX(ESFXType sfxSound);

        bool PlayAudioFromVideo(string video, GameObject gameObject);
        bool PlayEvent(string eventName, GameObject originSound);

}