using UnityEngine;

public class DialogueSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] dialogueTypingSoundClips;

    [SerializeField]
    private bool makePredictable;
    
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

    public void PlayDialogueSoundClip(int currentDisplayedCharacterCount, char currentCharacter)
    {
        if(audioSource.isPlaying) return;

        AudioClip soundClip = null;
        
        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (makePredictable)
            {
                int hashCode = currentCharacter.GetHashCode();
                
                // Predictable Sound Clip
                int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
                soundClip = dialogueTypingSoundClips[predictableIndex];
                
                // Predictable Pitch
                int minPitchInt = (int)(minPitch * 100);
                int maxPitchInt = (int)(maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;
                // Prevent 0 division
                if (pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = predictablePitch;
                }
                else
                {
                    audioSource.pitch = minPitch;
                }

            }
            else
            {
                //Random Sound Clip
                int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
                soundClip = dialogueTypingSoundClips[randomIndex];
                //Random Pitch
                audioSource.pitch = Random.Range(minPitch, maxPitch);

            }
        }
        
        audioSource.PlayOneShot(soundClip);
    }
}
