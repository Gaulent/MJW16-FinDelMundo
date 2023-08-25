using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerRandomizer : MonoBehaviour
{
    [SerializeField] private VideoClip[] myVideos;
    private VideoPlayer myPlayer;

    
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponent<VideoPlayer>();
        myPlayer.loopPointReached += EndReached;
        PlayRandomVideo(myPlayer);
    }   

    void PlayRandomVideo(VideoPlayer vp)
    {
        int videoIndex;

        do // TODO: Peligroso
        {
            videoIndex = Random.Range(0, myVideos.Length - 1);
        } while (vp.clip.name == myVideos[videoIndex].name);

        vp.clip = myVideos[videoIndex];

        vp.Play();
    }
    

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        PlayRandomVideo(vp);
    }

    public void StopVideo()
    {
        myPlayer.Pause();
    }
}
