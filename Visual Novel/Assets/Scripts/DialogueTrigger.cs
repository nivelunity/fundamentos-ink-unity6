using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogueKnotName;

    private void Start()
    {
        InitKnot();
    }

    public void InitKnot()
    {
        if(dialogueKnotName.Equals("")) return;
        if(!GameEventsManager.Instance.inputEvents.inputEventContext.Equals(InputEventContext.DEFAULT)) return;
        
        GameEventsManager.Instance.dialogueEvents.EnterDialogue(dialogueKnotName);
    }
}
