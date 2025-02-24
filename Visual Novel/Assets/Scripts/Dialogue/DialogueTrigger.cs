using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogueKnotName;

    public void InitDialogue()
    {
        if(dialogueKnotName.Equals("")) return;
        
        GameEventsManager.Instance.DialogueEvents_EnterDialogue(dialogueKnotName);
    }
}
