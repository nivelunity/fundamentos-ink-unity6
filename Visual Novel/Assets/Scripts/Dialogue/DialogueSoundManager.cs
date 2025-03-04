using UnityEngine;

public class DialogueSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip dialogueTypingSoundClip;

    [SerializeField]
    [Range(1,5)]
    private int frequencyLevel = 2;
    
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

    public void PlayDialogueSoundClip(int currentDisplayedCharacterCount)
    {
        if(audioSource.isPlaying) return;

        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            audioSource.PlayOneShot(dialogueTypingSoundClip);
        }
    }
}
