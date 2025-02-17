using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogueKnotName;

    private void Start()
    {
        EnterKnot();
    }

    public void EnterKnot()
    {
        if(dialogueKnotName.Equals("")) return;
        
        GameEventsManager.Instance.dialogueEvents.EnterDialogue(dialogueKnotName);
    }
}
