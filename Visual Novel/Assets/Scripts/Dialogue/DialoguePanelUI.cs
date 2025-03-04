using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialoguePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private GameObject nextLineButton;
    [SerializeField] private GameObject choicesContainer;
    
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;
    [SerializeField] private Image portraitImage;

    [SerializeField] private float typingSpeed = 0.04f;

    private Coroutine displayLineCoroutine;
    private string currentLine = "";
    private void Awake()
    {
        ResetDialogueText();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentLine != "")
            {
                TryHandleCoroutineDuplicate();
                dialogueText.maxVisibleCharacters = currentLine.Length;
                SetActiveInteractUI(true);
                currentLine = "";
            }
        }
    }

    private void DialogueStarted()
    {
        contentParent.SetActive(true);
    }
    
    private void DialogueFinished()
    {
        contentParent.SetActive(false);
        ResetDialogueText();
    }
    
    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        currentLine = dialogueLine;
        
        TryHandleCoroutineDuplicate();
        
        displayLineCoroutine = StartCoroutine(DisplayLine(dialogueLine));

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

    private void ResetDialogueText()
    {
        dialogueText.text = "";
    }

    private IEnumerator DisplayLine(string line)
    {
        ResetDialogueText();
        SetActiveInteractUI(false);

        dialogueText.text = currentLine;
        dialogueText.maxVisibleCharacters = 0;

        bool isAddingRichTextTag = false;
        
        foreach (char letter in line.ToCharArray())
        {
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                DialogueSoundManager.Instance.PlayDialogueSoundClip(dialogueText.maxVisibleCharacters);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        SetActiveInteractUI(true);
        currentLine = "";
    }

    private void SetActiveInteractUI(bool status)
    {
        nextLineButton.SetActive(status);
        choicesContainer.SetActive(status);
    }

    private void TryHandleCoroutineDuplicate()
    {
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }
    }
}
