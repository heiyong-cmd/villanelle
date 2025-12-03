using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleCinematic : MonoBehaviour
{
    [Header("【第一步】请手动把对话系统拖进来")]
    // 因为对话系统和这个脚本在同一个场景，所以可以直接拖！
    // 即使它是灰色的（隐藏的），拖进来也能用。
    public GameObject dialogueUI;

    [Header("【第二步】黑边设置 (无需拖拽)")]
    public float targetHeight = 150f; // 黑边高度
    public float growSpeed = 3f;      // 生长速度

    // 这两个在另一个场景，我们用代码自动找，不让你手动拖
    private RectTransform topBar;
    private RectTransform bottomBar;

    // 逻辑开关
    private bool hasClickedOnce = false;   // 是否点击了第一次
    private bool barsAreReady = false;     // 黑边是否长好了
    private bool hasTriggeredDialogue = false; // 是否触发了对话

    void Start()
    {
        // --- 自动查找黑边 (解决跨场景问题) ---
        GameObject topObj = GameObject.Find("TopBar");
        GameObject bottomObj = GameObject.Find("BottomBar");

        if (topObj != null) topBar = topObj.GetComponent<RectTransform>();
        else Debug.LogError("❌ 找不到 TopBar！请检查 Persistent 场景里的物体名字是不是叫 TopBar");

        if (bottomObj != null) bottomBar = bottomObj.GetComponent<RectTransform>();
        else Debug.LogError("❌ 找不到 BottomBar！");

        // --- 检查对话系统 ---
        if (dialogueUI == null)
        {
            Debug.LogError("❌ 你忘记拖拽对话系统了！请在 Inspector 面板里赋值！");
        }
        else
        {
            // 确保一开始对话系统是关闭的
            dialogueUI.SetActive(false);
        }
    }

    void Update()
    {
        // 如果有东西没找到，就什么都不做，防止报错
        if (topBar == null || bottomBar == null || dialogueUI == null) return;

        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // === 第一次点击：长黑边 ===
            if (!hasClickedOnce)
            {
                hasClickedOnce = true; // 标记已经点过一次了
                StartCoroutine(GrowBars()); // 开始长黑边
            }
            // === 第二次点击：黑边长好了 && 还没显示对话 ===
            else if (barsAreReady && !hasTriggeredDialogue)
            {
                hasTriggeredDialogue = true; // 标记对话已触发
                dialogueUI.SetActive(true);  // 显示对话 UI
                Debug.Log("✅ 对话系统已启动！");
            }
        }
    }

    // 黑边生长的动画协程
    IEnumerator GrowBars()
    {
        float currentHeight = 0f;

        // 只要高度没达标，就一直循环变高
        while (Mathf.Abs(currentHeight - targetHeight) > 0.1f)
        {
            currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * growSpeed);

            // 同时设置上下黑边
            if (topBar != null) topBar.sizeDelta = new Vector2(topBar.sizeDelta.x, currentHeight);
            if (bottomBar != null) bottomBar.sizeDelta = new Vector2(bottomBar.sizeDelta.x, currentHeight);

            yield return null; // 等待下一帧
        }

        // 动画结束，强制设为目标高度（防止小数误差）
        if (topBar != null) topBar.sizeDelta = new Vector2(topBar.sizeDelta.x, targetHeight);
        if (bottomBar != null) bottomBar.sizeDelta = new Vector2(bottomBar.sizeDelta.x, targetHeight);

        // 标记：黑边长好了，可以进行第二次点击了
        barsAreReady = true;
    }
}