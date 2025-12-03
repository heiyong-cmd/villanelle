using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 对话UI层
/// </summary>
public class DialogueView : Singleton<DialogueView>
{
    //对话UI层
    private Image dialogBox;
    private Image headBox;
    private TMP_Text dialogueText;
    private TMP_Text name;
    
    //选项框的对象池
    public OptionsPool optionsPool;
    private GameObject[] option = new GameObject[10];//创建数量与对象池的最大数量保持一致

    protected override void Awake()
    {
        dialogBox = transform.Find("Dialog Box").GetComponent<Image>();
        headBox = dialogBox.transform.Find("Head Box").GetComponent<Image>();
        dialogueText = dialogBox.transform.Find("Dialogue Text").GetComponent<TMP_Text>();
        name = dialogBox.transform.Find("Name").GetComponent<TMP_Text>();
        optionsPool = GetComponent<OptionsPool>();
        base.Awake();
    }

    /// <summary>
    /// 逐字显示文本
    /// </summary>
    /// <param name="text">当前语句文本</param>
    /// <param name="action">显示语句后执行的委托</param>
    /// <param name="dialogueModule">对话组件</param>
    /// <param name="isSkip">是否可以跳过逐字显示</param>
    /// <returns></returns>
    public IEnumerator DialogueCoroutine(string text, Action action,DialogueModule dialogueModule,bool isSkip)
    {
        dialogueText.text = "";
        
        foreach (char c in text)
        {
            if (isSkip)
            {
                dialogueText.text = "";
                dialogueText.text = text;
                
                dialogueModule.SkipWordOfWord(action);
                yield break;
            }
            
            dialogueText.text += c;
            yield return new WaitForSeconds(dialogueModule.dialogueSpeed);
        }
        
        action?.Invoke();
    }
    
    /// <summary>
    /// 直接显示文本
    /// </summary>
    /// <param name="text">当前语句文本</param>
    /// <param name="action">语句显示之后执行的委托</param>
    /// <returns></returns>
    public IEnumerator DialogueDirectlyContinue(string text, Action action)
    {
        dialogueText.text = "";
        dialogueText.text = text;
         
        yield return new WaitForSeconds(0.1f);
        
        action?.Invoke();
    }
    
    /// <summary>
    /// 显示选项
    /// </summary>
    /// <param name="currentOptionNode">当前的选项节点</param>
    /// <param name="dialogueModule">对话组件</param>
    public void OptionDisplay(OptionSO currentOptionNode,DialogueModule dialogueModule)
    {
        for (int i = 0; i < currentOptionNode.options.Length; i++)
        {
            option[i] = optionsPool.GetObjectFromPool();
            option[i].GetComponentInChildren<TMP_Text>().text = currentOptionNode.options[i].option;
            
            int currentIndex = i;
            Button button = option[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => dialogueModule.OptionAction(currentIndex));
        }
    }

    /// <summary>
    /// 回收选项
    /// </summary>
    /// <param name="currentOptionNode">当前选项节点</param>
    public void OptionDisappear(OptionSO currentOptionNode)
    {
        for (int i = 0; i < currentOptionNode.options.Length; i++)
        {
            optionsPool.ReturnObjectToPool(option[i]);
        }
    }
    
    /// <summary>
    /// 多人检查模式
    /// </summary>
    /// <param name="dialogueNode">对话节点（头像与名字）</param>
    public void MultiplayerModeCheck(DialogueSO dialogueNode)
    {
        if (dialogueNode.sprite != null)
        {
            headBox.enabled = true;
            headBox.sprite = dialogueNode.sprite;
        }
        else
        {
            headBox.enabled = false;
        }


        if (dialogueNode.name != null)
        {
            
            name.text = dialogueNode.name + ":";
        }
        else
        {
            name.text = null;
        }
    }
    
    /// <summary>
    /// 双人检查模式
    /// </summary>
    /// <param name="dialogueSprite">对话组件上的头像</param>
    /// <param name="dialogueName">对话组件上的名字</param>
    public void DuoModeCheck(Sprite dialogueSprite,string dialogueName)
    {
        if (dialogueSprite != null)
        {
            headBox.enabled = true;
            headBox.sprite = dialogueSprite;
        }
        else
        {
            headBox.enabled = false;
        }
        
        
        if (dialogueName != null)
        {
            name.text = dialogueName + ":";
        }
        else
        {
            name.text = null;
        }
    }

}
