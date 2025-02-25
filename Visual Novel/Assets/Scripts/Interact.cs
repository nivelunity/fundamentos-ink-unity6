using UnityEngine;
using UnityEngine.Events;

public class Interact: MonoBehaviour
{
    public UnityEvent OnInteract;
    
    private void OnMouseUp()
    {
        OnInteract?.Invoke();
    }
}
