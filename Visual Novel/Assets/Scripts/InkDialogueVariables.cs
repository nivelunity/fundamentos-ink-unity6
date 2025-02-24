using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

public class InkDialogueVariables
{
    private Dictionary<string, Ink.Runtime.Object> variables;

    public InkDialogueVariables(Story story)
    {
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in story.variablesState)
        {
            Ink.Runtime.Object value = story.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable "+name + " = "+ value );
        }
    }
}
