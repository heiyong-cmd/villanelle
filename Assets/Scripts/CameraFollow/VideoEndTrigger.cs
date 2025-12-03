using UnityEngine;
using UnityEngine.Video;

public class VideoEndTrigger : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    [SceneName] public string nextSceneName; // 视频播完去哪？

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        // 绑定事件：当视频播放结束（loopPointReached）时，调用 OnVideoEnd 方法
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 调用你之前写好的 TransitionManager 进行转场
        TransitionManager.Instance.Transition("H1", nextSceneName);
    }
}