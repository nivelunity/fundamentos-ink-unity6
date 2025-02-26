using System;
using UnityEngine;
using Ink.Runtime;

public class Notes : MonoBehaviour
{
    public void GetNotes()
    {
        Debug.Log("TENES LAS NOTAS");
        GameEventsManager.Instance.dialogueEvents.UpdateInkDialogueVariable("QuestState", 
            new StringValue(QuestState.CAN_FINISH.ToString()));
        
        Destroy(gameObject , 1f);
    }
}
