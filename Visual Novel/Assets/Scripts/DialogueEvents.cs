using System;

public class DialogueEvents 
{
   public event Action<string> onEnterDialogue;

   public void EnterDialogue(string knotName)
   {
      onEnterDialogue?.Invoke(knotName);
   }
}
