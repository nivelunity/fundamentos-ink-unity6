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

    public void SyncVariablesAndStartListening(Story story)
    {
        SyncVariablesStory(story);
        story.variablesState.variableChangedEvent += UpdateVariableState;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= UpdateVariableState;
    }

    public void UpdateVariableState(string name, Ink.Runtime.Object value)
    {
        if(!variables.ContainsKey(name)){ return; }

        variables[name] = value;
        Debug.Log("Updated Dialogue variable "+ name + " = " + value);
    }

    private void SyncVariablesStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
