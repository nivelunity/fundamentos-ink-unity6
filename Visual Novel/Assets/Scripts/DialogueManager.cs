using System;
using UnityEngine;
using Ink.Runtime;
public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")] 
    
    [SerializeField] private TextAsset inkJson;

    private Story story;
    private bool dialoguePlaying = false;

    private void Awake()
    {
        story = new Story(inkJson.text);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue += DialogueEvents_OnEnterDialogue;
    }

    public void SubmitPressed()
    {
        if(!dialoguePlaying) return;

        Debug.Log("NEXT LINE");
        
        ContinueOrExitStory();
    }
    
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= DialogueEvents_OnEnterDialogue;
    }

    private void DialogueEvents_OnEnterDialogue(string knotName)
    {
        if(dialoguePlaying) return;
        if(knotName.Equals("")) return;
        
        Debug.Log("Ingresando al dialogo [Nombre del Knot] " + knotName);
        
        dialoguePlaying = true;
        story.ChoosePathString(knotName);
        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if (story.canContinue)
        {
            string dialogueLine = story.Continue();
            Debug.Log(dialogueLine);
        }
        else
        {
            ExitDialogue();
        }
    }
    
    private void ExitDialogue()
    {
        Debug.Log("Exiting Dialogue");
        dialoguePlaying = false;
        
        story.ResetState();
    }
}
