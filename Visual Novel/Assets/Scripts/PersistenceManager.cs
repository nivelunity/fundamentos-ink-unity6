using UnityEngine;
using Ink.Runtime;

public class PersistenceManager : MonoBehaviour
{
   public static PersistenceManager Instance { get; private set; }
   
   private const string saveVariablesKey = "INK_VARIABLES";

   private void Awake()
   {
      if (Instance != null)
      {
         Debug.LogError(" Instancia duplicada de PersistenceManager "+ transform + " - " +Instance);
         Destroy(gameObject);
         return;
      }

      Instance = this;
   }
   
   public void SaveStoryState(Story story)
   {
      Debug.Log("Save Story State");
      PlayerPrefs.SetString(saveVariablesKey, story.state.ToJson());
   }
   
   public void TryLoadStoryState(Story story)
   {
      if (PlayerPrefs.HasKey(saveVariablesKey))
      {
         Debug.Log("Load Story State");
         string jsonState = PlayerPrefs.GetString(saveVariablesKey);
         story.state.LoadJson(jsonState);
      }
   }
}
