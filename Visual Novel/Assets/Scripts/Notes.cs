using System;
using UnityEngine;
using Ink.Runtime;

public class Notes : MonoBehaviour
{
    private void OnEnable()
    {
        InkExternalFunctions.onAcceptQuest += ShowNotes;
    }

    private void OnDisable()
    {
        InkExternalFunctions.onAcceptQuest -= ShowNotes;
    }

    public void GetNotes()
    {
        Debug.Log("TENES LAS NOTAS");
        GameEventsManager.Instance.dialogueEvents.UpdateInkDialogueVariable("QuestState", 
            new StringValue(QuestState.CAN_FINISH.ToString()));
        
        Destroy(gameObject , 1f);
    }

    private void ShowNotes()
    {
        Debug.Log("LAS NOTAS APARECEN EN PANTALLA");
        transform.position = Vector3.zero;
    }
}
