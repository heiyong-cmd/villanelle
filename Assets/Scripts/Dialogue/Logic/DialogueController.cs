using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO dialogueEmpty;


    private Stack<string> dialogueEmptyStack;

    private bool isTalking;
    private void Awake()
    {
        FillDialogueStack();
    }
    private void FillDialogueStack()
    {
        dialogueEmptyStack = new Stack<string>();

        for (int i = dialogueEmpty.dialogueList.Count - 1; i > -1; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }

    }
    public void ShowDialogueEmpty()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogueEmptyStack));
        }
    }
    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;
        if (data.TryPop(out string result))
        {
            EventHandler.CallshowDialogueEvent(result);
            yield return null;
            isTalking = false;
        }
        else
        {
            EventHandler.CallshowDialogueEvent(string.Empty);
            FillDialogueStack();
            isTalking = false;

        }
    }

}