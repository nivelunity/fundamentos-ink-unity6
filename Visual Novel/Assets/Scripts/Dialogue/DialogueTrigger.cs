using System;
using Ink.Runtime;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogueKnotName;

    private void Start()
    {
        
    }

    public void InitDialogue()
    {
        if(dialogueKnotName.Equals("")) return;
        GameEventsManager.Instance.DialogueEvents_EnterDialogue(dialogueKnotName);
    }
}
