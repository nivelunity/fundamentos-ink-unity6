using UnityEngine;

public class DialogueSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip dialogueTypingSoundClip;
    
    public static DialogueSoundManager Instance { get; private set; }

    private AudioSource audioSource;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError(" Instancia duplicada de DialogueSoundManager "+ transform + " - " +Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDialogueSoundClip()
    {
        audioSource.PlayOneShot(dialogueTypingSoundClip);
    }
}
