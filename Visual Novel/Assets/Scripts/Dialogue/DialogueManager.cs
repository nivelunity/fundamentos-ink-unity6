using System.Collections.Generic;
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
    private InkDialogueVariables inkDialogueVariables;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
        
    private void Awake()
    {
        story = new Story(inkJson.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);
        inkDialogueVariables = new InkDialogueVariables(story);
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
        GameEventsManager.Instance.dialogueEvents.onUpdateInkDialogueVariable += DialogueEvents_OnUpdateInkDialogueVariable;
    }
    
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= DialogueEvents_OnEnterDialogue;
        GameEventsManager.Instance.inputEvents.onSubmitPressed -= InputEvents_OnSubmitPressed;
        GameEventsManager.Instance.dialogueEvents.onUpdateChoiceIndex -= DialogueEvents_OnUpdateChoiceIndex;
        GameEventsManager.Instance.dialogueEvents.onUpdateInkDialogueVariable -= DialogueEvents_OnUpdateInkDialogueVariable;
    }
    
    private void DialogueEvents_OnEnterDialogue(string knotName)
    {
        if(dialoguePlaying) return;
        if(knotName.Equals("")) return;
        
        GameEventsManager.Instance.dialogueEvents.DialogueStarted();
        GameEventsManager.Instance.inputEvents.ChangeInputEventContext(InputEventContext.DIALOGUE);
        
        dialoguePlaying = true;
        story.ChoosePathString(knotName);
        
        inkDialogueVariables.SyncVariablesAndStartListening(story);
        
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

    private void DialogueEvents_OnUpdateInkDialogueVariable(string name, Ink.Runtime.Object value)
    {
        Debug.Log("ACTUALIZANDO DialogueEvents_OnUpdateInkDialogueVariable");
        inkDialogueVariables.UpdateVariableState(name,value);
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

            HandleTags(story.currentTags);
            
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

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag cold not be appropriately parsed: "+ tag);
            }

            string tagKey   = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    Debug.Log("speaker="+tagValue);
                    GameEventsManager.Instance.dialogueEvents.UpdateSpeaker(tagValue);
                    break;
                case PORTRAIT_TAG:
                    Debug.Log("portrait="+tagValue);
                    GameEventsManager.Instance.dialogueEvents.UpdatePortrait(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came but is not currently being handled: "+ tag);
                    break;
            }
        }
    }
    
    private void ExitDialogue()
    {
        dialoguePlaying = false;
        
        GameEventsManager.Instance.dialogueEvents.DialogueFinished();
        GameEventsManager.Instance.inputEvents.ChangeInputEventContext(InputEventContext.DEFAULT);
        
        inkDialogueVariables.StopListening(story);
        
        story.ResetState();
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }
}