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
        GameEventsManager.Instance.inputEvents.onSubmitPressed += InputEvents_OnSubmitPressed;
    }
    
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= DialogueEvents_OnEnterDialogue;
        GameEventsManager.Instance.inputEvents.onSubmitPressed += InputEvents_OnSubmitPressed;
    }
    
    private void DialogueEvents_OnEnterDialogue(string knotName)
    {
        if(dialoguePlaying) return;
        if(knotName.Equals("")) return;
        
        Debug.Log("Ingresando al dialogo [Nombre del Knot] " + knotName);
        GameEventsManager.Instance.inputEvents.ChangeInputEventContext(InputEventContext.DIALOGUE);
        
        dialoguePlaying = true;
        story.ChoosePathString(knotName);
        ContinueOrExitStory();
    }
    
    public void InputEvents_OnSubmitPressed(InputEventContext inputEventContext)
    {
        if(!inputEventContext.Equals(InputEventContext.DIALOGUE)) return;
        
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
        GameEventsManager.Instance.inputEvents.ChangeInputEventContext(InputEventContext.RESET);
    }
}
