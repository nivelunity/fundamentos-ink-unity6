using System;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions 
{
    public static event Action onAcceptQuest;
    
    public void Bind(Story story)
    {
        story.BindExternalFunction("GiveNotes", GiveNotes);
        story.BindExternalFunction("AcceptQuest", AcceptQuest);
    }
    
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("GiveNotes");
        story.UnbindExternalFunction("AcceptQuest");
    }
    
    private void GiveNotes()
    {
        Debug.Log("COMPARTISTE TUS NOTAS"); 
        GameEventsManager.Instance.dialogueEvents.UpdateInkDialogueVariable("QuestState", 
            new StringValue(QuestState.FINISHED.ToString()));
    }
    
    private void AcceptQuest()
    {
        Debug.Log("ACEPTASTE LA QUEST DE NOTAS");
        GameEventsManager.Instance.dialogueEvents.UpdateInkDialogueVariable("QuestState", 
            new StringValue(QuestState.IN_PROGRESS.ToString()));
        onAcceptQuest?.Invoke();
    }
}
