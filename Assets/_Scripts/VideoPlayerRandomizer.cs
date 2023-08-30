using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class VideoPlayerRandomizer : MonoBehaviour
{
    [SerializeField] private VideoClip[] myVideos;
    private VideoPlayer myPlayer;
    private List<VideoClip> videoQueue;

    
    // Start is called before the first frame update
    void Start()
    {
        if (myVideos.Length == 0)
            Debug.LogError("VideoPlayer don't have any input video");
        myPlayer = GetComponent<VideoPlayer>();
        myPlayer.loopPointReached += EndReached;
        PlayRandomVideo(myPlayer);
    }   

    VideoClip GetRandomVideo()
    {
        VideoClip vc;

        if (videoQueue == null || videoQueue.Count == 0)
            videoQueue = myVideos.OrderBy(a => Random.value).ToList();

        vc = videoQueue[0];
        videoQueue.RemoveAt(0);
        return vc;
    }

    void PlayRandomVideo(VideoPlayer vp)
    {
        VideoClip vc = GetRandomVideo();

        // Discard current video if is actually the same
        if (vp.clip.name == vc.name)
            vc = GetRandomVideo();

        vp.clip = vc;
        vp.Play();
    }


    void EndReached(VideoPlayer vp)
    {
        PlayRandomVideo(vp);
    }

    public void StopVideo()
    {
        myPlayer.Pause();
    }
}
