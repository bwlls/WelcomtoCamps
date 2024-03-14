using UnityEngine;
using UnityEngine.Video;
public class videoscript : MonoBehaviour
{
    [SerializeField] string VideoFileName;
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer)
        {
            string VideoPath = System.IO.Path.Combine(Application.streamingAssetsPath, VideoFileName);
            videoPlayer.url = VideoPath;
            Debug.Log(VideoPath);
            videoPlayer.Play();
        }
    }
}
