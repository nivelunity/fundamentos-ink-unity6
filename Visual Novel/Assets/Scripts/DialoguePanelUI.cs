using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialoguePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;

    private void Awake()
    {
        ResetPanel();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onDialogueStarted  += DialogueStarted;
        GameEventsManager.Instance.dialogueEvents.onDisplayDialogue  += DisplayDialogue;
        GameEventsManager.Instance.dialogueEvents.onDialogueFinished += DialogueFinished;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onDialogueStarted  -= DialogueStarted;
        GameEventsManager.Instance.dialogueEvents.onDisplayDialogue  -= DisplayDialogue;
        GameEventsManager.Instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
    }

    private void DialogueStarted()
    {
        contentParent.SetActive(true);
    }
    
    private void DialogueFinished()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }
    
    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        dialogueText.text = dialogueLine;

        if (dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("More dialogue choices ("+dialogueChoices.Count+") came through than are supported("+ choiceButtons.Length+").");
        }
        
        // Hidden Choice Buttons
        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }
        
        // Set Ink choice Information
        int choiceButtonIndex = dialogueChoices.Count - 1;
        
        for (int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];
            
            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceText(dialogueChoice.text);
            choiceButton.SetChoiceIndex(inkChoiceIndex);

            if (inkChoiceIndex == 0)
            {
                choiceButton.SelectButton();
                GameEventsManager.Instance.dialogueEvents.UpdateChoiceIndex(0);
            }
            
            choiceButtonIndex--;
        }

    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }
}
