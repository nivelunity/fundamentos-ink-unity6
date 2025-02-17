using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private bool dialoguePlaying = false;

    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue += DialogueEvents_OnEnterDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= DialogueEvents_OnEnterDialogue;
    }

    private void DialogueEvents_OnEnterDialogue(string knotName)
    {
        if(dialoguePlaying) return;
        
        dialoguePlaying = true;

        Debug.Log("Ingresando al dialogo [Nombre del Knot] " + knotName);
    }
}
