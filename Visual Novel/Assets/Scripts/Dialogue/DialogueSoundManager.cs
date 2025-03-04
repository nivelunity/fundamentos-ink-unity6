using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueSoundManager : MonoBehaviour
{
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;

    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    
    [SerializeField]
    private bool makePredictable;
    
    public static DialogueSoundManager Instance { get; private set; }

    private DialogueAudioInfoSO currentAudioInfo;
    private AudioSource audioSource;

    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    
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
        currentAudioInfo = defaultAudioInfo;
    }

    private void Start()
    {
        InitializeAudioInfoDictionary();
    }

    private void InitializeAudioInfoDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
        audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);

        foreach (DialogueAudioInfoSO audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }
    }

    public void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO audioInfo = null;
        audioInfoDictionary.TryGetValue(id, out audioInfo);
        if (audioInfo != null)
        {
            
        }

    }
    
    public void PlayDialogueSoundClip(int currentDisplayedCharacterCount, char currentCharacter)
    {
        if(audioSource.isPlaying) return;

        int frequencyLevel = currentAudioInfo.frequencyLevel;
        
        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
            float minPitch = currentAudioInfo.minPitch;
            float maxPitch = currentAudioInfo.maxPitch;
            
            AudioClip soundClip = null;
            
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
            
            audioSource.PlayOneShot(soundClip);
        }
    }
}
