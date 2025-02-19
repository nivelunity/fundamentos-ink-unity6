using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogueKnotName;

    private void Start()
    {
        InitDialogue();
    }

    public void InitDialogue()
    {
        if(dialogueKnotName.Equals("")) return;
        
        GameEventsManager.Instance.DialogueEvents_EnterDialogue(dialogueKnotName);
    }
}
