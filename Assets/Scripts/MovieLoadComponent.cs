using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class MovieLoad : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<string> list = new List<string>(){"example1.mp4","example2_reformat.mp4","example3.mp4"};;

    UnityEngine.Video.VideoPlayer videoPlayer;
    void Start()
    {  
            TriggerVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TriggerVideo()
    {
        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;

        // This will cause our Scene to be visible through the video being played.
        videoPlayer.targetCameraAlpha = 1.0f;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        videoPlayer.url = GetUrl();

        // Restart from beginning when done.
        videoPlayer.isLooping = true;

        // Each time we reach the end, we slow down the playback by a factor of 10.
        videoPlayer.loopPointReached += EndReached;

        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        videoPlayer.Play();
    }

    string GetUrl()
    {
        return Application.streamingAssetsPath + "/"+list[Random.Range(0,list.Count)];
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.url = GetUrl();
        vp.Play();
        // vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }
}
