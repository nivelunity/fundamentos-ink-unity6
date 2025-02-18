using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public DialogueEvents dialogueEvents;
    public InputEvents inputEvents;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError(" Instancia duplicada de GameEventsManager "+ transform + " - " +Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        dialogueEvents = new DialogueEvents();
        inputEvents = new InputEvents();
    }

    public void InputEvents_SubmitPressed()
    {
        inputEvents.SubmitPressed();
    }
}
