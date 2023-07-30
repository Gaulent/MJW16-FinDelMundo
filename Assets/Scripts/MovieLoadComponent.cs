using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class MovieLoad : MonoBehaviour
{
    // Start is called before the first frame update
    bool isStillShown = false;
    ISoundManager soundManager;
    string currentFileLoaded = "";
    string previousFileLoaded = "";
    [SerializeField] List<string> list;
    UnityEngine.Video.VideoPlayer videoPlayer;
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<ISoundManager>();
        videoPlayer = this.gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
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

        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;

        videoPlayer.url = GetUrl();

        // Restart from beginning when done.
        videoPlayer.isLooping = true;

        // Each time we reach the end, we slow down the playback by a factor of 10.
        videoPlayer.loopPointReached += EndReached;


        TriggerVideo(); // TEMPORARY TODO

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopVideo()
    {
        isStillShown = false;
        videoPlayer.Stop();
        // Maybe?? 
        Destroy(videoPlayer);
    }

    public void TriggerVideo()
    {
        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        isStillShown = true;
        this.Play(videoPlayer);
    }

    private void Play(VideoPlayer vp)
    {
        soundManager.PlayAudioFromVideo(currentFileLoaded,this.gameObject);
        vp.Play();
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        if (!isStillShown){vp.Stop();}
        vp.url = GetUrl();
        this.Play(vp);
        // vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }

    string GetUrl()
    {
        previousFileLoaded = currentFileLoaded;
        do{
            currentFileLoaded = list[Random.Range(0, list.Count)];
        }while(previousFileLoaded == currentFileLoaded);    
        return Application.streamingAssetsPath + "/" + currentFileLoaded;
    }
}
