using UnityEngine;

public class LetterTrigger : MonoBehaviour
{
    [Header("请在编辑器里拖入对应的图片物体")]
    public GameObject promptUI;       // 那个“按E”的图片物体
    public GameObject letterContentUI;// 那个“信件内容”的图片物体
    public GameObject letter;

    // 这是一个开关，用来记录玩家是不是在范围内
    private bool isPlayerInRange = false;

    // 1. 初始化：先把所有图都藏起来
    void Awake()
    {
        if (promptUI != null) promptUI.SetActive(false);
        if (letterContentUI != null) letterContentUI.SetActive(false);
        if (letter != null) letter.SetActive(true);
    }

    // 2. 每一帧都在监听：是否按下了E键
    void Update()
    {
        // 只有当“玩家在范围内” 并且 “按下了E键” 时，才去执行你的自定义逻辑
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            RunMyCustomScript();
        }
    }

    // 3. 你的自定义逻辑方法（你要在这里实现切换图片）
    // 3. 你的自定义逻辑方法（你要在这里实现切换图片）
    void RunMyCustomScript()
    {
        // 安全检查：防止你忘了拖图片报错
        if (letterContentUI == null) return;

        // --- 核心判断逻辑 ---

        // 检查：信件当前是显示状态吗？
        if (letterContentUI.activeSelf == true)
        {
            // 【情况A：信已经开了】 -> 现在要关闭它
            Debug.Log("关闭信件");
            letterContentUI.SetActive(false); // 关掉信

            if (promptUI != null)
                promptUI.SetActive(true);     // 把“按E”提示重新显示出来
        }
        else
        {
            // 【情况B：信还没开】 -> 现在要打开它
            Debug.Log("打开信件");
            letterContentUI.SetActive(true);  // 打开信

            if (promptUI != null)
                promptUI.SetActive(false);    // 把“按E”提示藏起来
        }
    }
    // 4. 物理检测：玩家走进来了
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. 先不管Tag，只要有东西碰到了就打印
        Debug.Log("检测到有物体进入！碰到的东西叫：" + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("是 Player 标签，逻辑正确。");
            isPlayerInRange = true;
            if (promptUI != null) promptUI.SetActive(true);
        }
        else
        {
            // 2. 如果打印了上面那句话，但没打印这句话，说明 Tag 设置错了
            Debug.Log("碰到了物体，但它的 Tag 不是 Player，而是：" + other.tag);
        }
    }

    // 5. 物理检测：玩家走出去了
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 标记状态：离开范围
            isPlayerInRange = false;

            // 离开后，不管信看没看完，把所有UI都关掉
            if (promptUI != null) promptUI.SetActive(false);
            if (letterContentUI != null) letterContentUI.SetActive(false);

        }
    }
}