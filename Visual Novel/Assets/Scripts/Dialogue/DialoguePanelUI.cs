using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialoguePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;
    [SerializeField] private Image portraitImage;

    [SerializeField] private float typingSpeed = 0.04f;
    private void Awake()
    {
        ResetPanel();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onDialogueStarted  += DialogueStarted;
        GameEventsManager.Instance.dialogueEvents.onDisplayDialogue  += DisplayDialogue;
        GameEventsManager.Instance.dialogueEvents.onDialogueFinished += DialogueFinished;
        GameEventsManager.Instance.dialogueEvents.onUpdateSpeaker += UpdateSpeaker;
        GameEventsManager.Instance.dialogueEvents.onUpdatePortrait += UpdatePortrait;

    }

    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onDialogueStarted  -= DialogueStarted;
        GameEventsManager.Instance.dialogueEvents.onDisplayDialogue  -= DisplayDialogue;
        GameEventsManager.Instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
        GameEventsManager.Instance.dialogueEvents.onUpdateSpeaker -= UpdateSpeaker;
        GameEventsManager.Instance.dialogueEvents.onUpdatePortrait -= UpdatePortrait;
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
        StartCoroutine(DisplayLine(dialogueLine));

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

    private void UpdateSpeaker(string speaker)
    {
        Debug.Log("SET SPEAKER UI "+speaker);
        speakerText.text = speaker;
    }
    
    private void UpdatePortrait(string portrait)
    {
        Debug.Log("SET Portrait UI "+portrait);
        portraitImage.sprite = Resources.Load<Sprite>(portrait);
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
