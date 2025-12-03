using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DialogueController))]
public class H2Touch : Interactive
{
    private DialogueController dialogueController;
    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    public override void OnClickedAction()
    {
        dialogueController.ShowDialogueEmpty();
    }
}
