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
}
