using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum effectType
{
    typeWriter = 0,
}

public class TypeWriter : MonoBehaviour
{
    public TMP_Text m_text;

    [Range(0, 5)] public float speed = 1;

    // 拖拽下一句对话的物体，如果是最后一句（需要点击传送），这里留空
    public GameObject nextObject;

    // 新增：用来存储碰撞体组件
    private BoxCollider2D myCollider;

    private void Awake()
    {
        gameObject.TryGetComponent<TMP_Text>(out m_text);

        // 新增：尝试获取自身的 BoxCollider2D
        gameObject.TryGetComponent<BoxCollider2D>(out myCollider);
    }

    private void Start()
    {
        // 新增：一开始先把碰撞体关掉，防止玩家提前点击
        if (myCollider != null)
        {
            myCollider.enabled = false;
        }

        StartCoroutine(TypeWriterState());
    }

    private IEnumerator TypeWriterState()
    {
        m_text.ForceMeshUpdate();
        TMP_TextInfo textInfo = m_text.textInfo;
        int total = textInfo.characterCount;
        bool complete = false;
        int current = 0;

        while (!complete)
        {
            if (current > total)
            {
                current = total;
                yield return new WaitForSecondsRealtime(1);
                complete = true;
            }
            else
            {
                m_text.maxVisibleCharacters = current;
                current++;
                yield return new WaitForSecondsRealtime(speed);
            }
        }

        // --- 打字结束后的逻辑 ---

        // 情况1：如果有下一句对话（链条还没断）
        if (nextObject != null)
        {
            nextObject.SetActive(true);  // 激活下一句
            gameObject.SetActive(false); // 隐藏自己
        }
        // 情况2：没有下一句了（这是最后一句，等待玩家点击传送）
        else
        {
            // 如果身上有碰撞体，现在把它打开
            if (myCollider != null)
            {
                myCollider.enabled = true;
            }

            // 重要：这里删掉了 gameObject.SetActive(false);
            // 因为如果不保持开启，碰撞体就无法响应点击，你的 Teleport 脚本也就不会触发
        }
    }
}