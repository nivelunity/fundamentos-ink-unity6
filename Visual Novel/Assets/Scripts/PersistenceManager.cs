using UnityEngine;
using Ink.Runtime;

public class PersistenceManager : MonoBehaviour
{
   private const string saveVariablesKey = "INK_VARIABLES";

   public void SaveStoryState(Story story)
   {
      PlayerPrefs.SetString(saveVariablesKey, story.state.ToJson());
   }
   
   public void TryLoadStoryState(Story story)
   {
      if (PlayerPrefs.HasKey(saveVariablesKey))
      {
         string jsonState = PlayerPrefs.GetString(saveVariablesKey);
         story.state.LoadJson(jsonState);
      }
   }
}
