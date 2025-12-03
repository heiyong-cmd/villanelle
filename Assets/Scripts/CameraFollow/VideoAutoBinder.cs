using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoAutoBinder : MonoBehaviour
{
    void Start()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();

        // 自动找到场景里标记为 MainCamera 的相机，并赋值给播放器
        if (Camera.main != null)
        {
            vp.targetCamera = Camera.main;
        }
        else
        {
            Debug.LogError("找不到 MainCamera！请确保你的主相机 Tag 设置为 MainCamera");
        }
    }
}