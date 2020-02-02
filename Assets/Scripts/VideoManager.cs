using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VideoPlayer>().loopPointReached += MudaCena;   
    }

    public void MudaCena(VideoPlayer vp)
    {
        SceneManager.LoadScene(2);      
    }

}
