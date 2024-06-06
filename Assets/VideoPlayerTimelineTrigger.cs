using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerTimelineTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // public PlayableDirector timelineClip;

    private void Start()
    {
        Cursor.visible = false;
        videoPlayer.loopPointReached += EndVideoOpening;
    }

    private void EndVideoOpening(VideoPlayer source)
    {
        // UIManager.Instance.ShowCamera();
        // timelineClip.Play();
        // gameObject.SetActive(false);

        SceneManager.LoadScene(Loader.Scene.LaboratoryScene.ToString());
    }
}
