using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public DialogueEvents dialogueEvents;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError(" Instancia duplicada de BubblesMatchManager "+ transform + " - " +Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        dialogueEvents = new DialogueEvents();
    }
}
