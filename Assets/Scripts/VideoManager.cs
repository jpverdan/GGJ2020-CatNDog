using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    VideoPlayer _videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "CutscenePequena.mp4");
        _videoPlayer.loopPointReached += MudaCena;   
    }

    public void MudaCena(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);      
    }

}
