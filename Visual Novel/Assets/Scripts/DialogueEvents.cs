using System;

public class DialogueEvents 
{
   public event Action<string> onEnterDialogue;
   public event Action onDialogueStarted;
   public event Action<string> onDisplayDialogue;
   public event Action onDialogueFinished;

   public void EnterDialogue(string knotName)
   {
      onEnterDialogue?.Invoke(knotName);
   }
   public void DialogueStarted()
   {
      onDialogueStarted?.Invoke();
   }
   public void DisplayDialogue(string dialogueLine)
   {
      onDisplayDialogue?.Invoke(dialogueLine);
   }
   public void DialogueFinished()
   {
      onDialogueFinished?.Invoke();
   }
}
