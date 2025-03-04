using UnityEngine;

public class DialogueSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] dialogueTypingSoundClips;

    [SerializeField]
    [Range(1,5)]
    private int frequencyLevel = 2;
    
    [SerializeField]
    [Range(-3,3)]
    private float minPitch = 0.5f;
    
    [SerializeField]
    [Range(-3,3)]
    private float maxPitch = 3f;
    
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
            //Random Sound Clip
            int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
            AudioClip soundClip = dialogueTypingSoundClips[randomIndex];
            //Random Pitch
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            //Play
            audioSource.PlayOneShot(soundClip);
        }
    }
}
