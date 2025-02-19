using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialoguePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;

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
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }
}
