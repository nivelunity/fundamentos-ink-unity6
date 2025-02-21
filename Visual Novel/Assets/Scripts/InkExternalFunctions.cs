using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions 
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("GiveNotes", GiveNotes);
    }
    
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("GiveNotes");
    }
    
    private void GiveNotes()
    {
        Debug.Log("COMPARTISTE TUS NOTAS");        
    }
}
