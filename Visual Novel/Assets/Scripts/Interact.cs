using UnityEngine;
using UnityEngine.Events;

public class Interact: MonoBehaviour
{
    public UnityEvent OnInteract;
    
    private void OnMouseUp()
    {
        Debug.Log("Interact with "+gameObject.name);
        OnInteract?.Invoke();
    }
}
