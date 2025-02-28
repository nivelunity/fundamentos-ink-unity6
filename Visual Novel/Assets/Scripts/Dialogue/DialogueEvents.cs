using System;
using System.Collections.Generic;
using Ink.Runtime;

public class DialogueEvents 
{
   public event Action<string> onEnterDialogue;
   public event Action onDialogueStarted;
   public event Action<string, List<Choice>> onDisplayDialogue;
   public event Action onDialogueFinished; 
   public event Action<int> onUpdateChoiceIndex;
   public event Action<string, Ink.Runtime.Object> onUpdateInkDialogueVariable;
   
   public event Action<string> onUpdateSpeaker;
   public event Action<string> onUpdatePortrait;

   public void EnterDialogue(string knotName)
   {
      onEnterDialogue?.Invoke(knotName);
   }
   public void DialogueStarted()
   {
      onDialogueStarted?.Invoke();
   }
   public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
   {
      onDisplayDialogue?.Invoke(dialogueLine,dialogueChoices);
   }
   public void DialogueFinished()
   {
      onDialogueFinished?.Invoke();
   }
   public void UpdateChoiceIndex(int choiceIndex)
   {
      onUpdateChoiceIndex?.Invoke(choiceIndex);
   }
   public void UpdateInkDialogueVariable(string name, Ink.Runtime.Object value)
   {
      onUpdateInkDialogueVariable?.Invoke(name,value);
   }
   public void UpdateSpeaker(string speaker)
   {
      onUpdateSpeaker?.Invoke(speaker);
   }
   
   public void UpdatePortrait(string portrait)
   {
      onUpdatePortrait?.Invoke(portrait);
   }
}
