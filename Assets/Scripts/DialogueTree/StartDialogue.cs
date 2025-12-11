using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;
public class StartDialogue : MonoBehaviour
{
    DialogueTreeController dialogueTree;
    private void Start()
    {
        dialogueTree = GetComponent<DialogueTreeController>();
    }
    public void Talk()
    {
        dialogueTree.StartDialogue();
    }
}
