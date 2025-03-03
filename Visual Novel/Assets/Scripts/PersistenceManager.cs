using UnityEngine;
using Ink.Runtime;

public class PersistenceManager : MonoBehaviour
{
   private const string saveVariablesKey = "INK_VARIABLES";

   public void SaveVariables(Story story)
   {
      PlayerPrefs.SetString(saveVariablesKey, story.state.ToJson());
   }
}
