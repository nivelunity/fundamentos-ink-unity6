using System;
using UnityEngine;
using Ink.Runtime;
public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")] 
    
    [SerializeField] private TextAsset inkJson;

    private Story story;
    private int currentChoiceIndex = -1;
    private bool dialoguePlaying = false;

    private InkExternalFunctions inkExternalFunctions;
        
    private void Awake()
    {
        story = new Story(inkJson.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);
    }

    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue += DialogueEvents_OnEnterDialogue;
        GameEventsManager.Instance.inputEvents.onSubmitPressed += InputEvents_OnSubmitPressed;
        GameEventsManager.Instance.dialogueEvents.onUpdateChoiceIndex += DialogueEvents_OnUpdateChoiceIndex;
    }
    
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= DialogueEvents_OnEnterDialogue;
        GameEventsManager.Instance.inputEvents.onSubmitPressed += InputEvents_OnSubmitPressed;
        GameEventsManager.Instance.dialogueEvents.onUpdateChoiceIndex += DialogueEvents_OnUpdateChoiceIndex;
    }
    
    private void DialogueEvents_OnEnterDialogue(string knotName)
    {
        if(dialoguePlaying) return;
        if(knotName.Equals("")) return;
        
        GameEventsManager.Instance.dialogueEvents.DialogueStarted();
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

    private void DialogueEvents_OnUpdateChoiceIndex(int choiceIndex)
    {
        currentChoiceIndex = choiceIndex;
    }
  
    private void ContinueOrExitStory()
    {
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            story.ChooseChoiceIndex(currentChoiceIndex);
            currentChoiceIndex = -1;
        }
        
        if (story.canContinue)
        {
            string dialogueLine = story.Continue();

            while (IsLineBlank(dialogueLine) && story.canContinue)
            {
                dialogueLine = story.Continue();
            }

            if (IsLineBlank(dialogueLine) && !story.canContinue)
            {
                ExitDialogue();
            }
            else
            { 
                GameEventsManager.Instance.dialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices);      
            }
        }
        else if(story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }
    
    private void ExitDialogue()
    {
        dialoguePlaying = false;
        
        GameEventsManager.Instance.dialogueEvents.DialogueFinished();
        GameEventsManager.Instance.inputEvents.ChangeInputEventContext(InputEventContext.RESET);
        
        story.ResetState();
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }
}