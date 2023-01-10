using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;
public class DialogueVariables
{
    private Dictionary<string, Ink.Runtime.Object> dialogueVars;
    private Story gloablVarsStory;

     public event Action<string> OnQuestStarted;
    public DialogueVariables(TextAsset loadGlobalsJSON) {
        //create an ink story for the global vars asset file provided.
         gloablVarsStory = new Story(loadGlobalsJSON.text);
        //initialize dictionary
        dialogueVars = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in gloablVarsStory.variablesState) {
            Ink.Runtime.Object value = gloablVarsStory.variablesState.GetVariableWithName(name);
            dialogueVars.Add(name,value);
            Debug.Log("Initializing global dialogue variables :" + name + " = " +value);
        }
    }

    public void StartListening(Story story) {
        //always assign GetVariablesToStory method before setting the event listener to avoid errors...
        GetVariablesToStory(story);
        story.variablesState.variableChangedEvent += DialogueVarsChanged;
    }
    public void StopListening(Story story) {
        story.variablesState.variableChangedEvent -= DialogueVarsChanged;
    }
    //test method
    public void ModifyDictionary(string globalVarName) {
        //modify ink variable dictionary of the value corresponding to the given string
        Ink.Runtime.Object value = gloablVarsStory.variablesState.GetVariableWithName(globalVarName);
        if(dialogueVars.ContainsKey(globalVarName)) {
            Debug.Log("Key Present.. Key : "+globalVarName);
            //Debug.Log("global vars value : "+ value);
            dialogueVars.Remove(globalVarName);
            dialogueVars.Add(globalVarName,value);
            Debug.Log("INK Variables Have Been Modified!!");
        }
        foreach(var nam in dialogueVars) {
            Debug.Log("Key : "+nam.Key + "Value : "+nam.Value);
        }
    }

    public string CheckQuestIDExists(string qid) {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in dialogueVars) {
            //extra caution
            string keyValue = variable.Key;
            string key = keyValue.ToLower();
            int val = ((Ink.Runtime.IntValue) this.GetVariableState(variable.Key)).value;
            string quest_id = key+val;
            if(quest_id == qid) {
                return keyValue;
            }
        }
        return null;
    }
    public void MarkQuestCompletedBool(string questKey) {
        string keyTag = questKey+"_quest_completed";
        string globalVarName = "";
        foreach(string name in gloablVarsStory.variablesState) {
            if(name == keyTag) {
                //gloablVarsStory.variablesState[name] = true;
                globalVarName = name;
            }
        }
        gloablVarsStory.variablesState[globalVarName] = true;
        ModifyDictionary(globalVarName);
    }
    public Ink.Runtime.Object GetVariableState(string variableName) {
        Ink.Runtime.Object variableValue = null;
        this.dialogueVars.TryGetValue(variableName, out variableValue);
        if(variableValue == null) {
            Debug.Log("Variable value is empty");
        }
        return variableValue;
    }
    private void DialogueVarsChanged(string name, Ink.Runtime.Object val) {
        //updates variables on the ink globals.ink file
        if(dialogueVars.ContainsKey(name)) {
            dialogueVars.Remove(name);
            dialogueVars.Add(name, val);
            CheckQuestStartedState(name);
        }
    }

    private void CheckQuestStartedState(string name) {
        //check for the status of the bool
        if(gloablVarsStory.variablesState[name].GetType() == typeof(bool)){
            bool questStartedState = ((Ink.Runtime.BoolValue) this.GetVariableState(name)).value;
            if(questStartedState) {
                string questKey = name.Split("_")[0];
                foreach(KeyValuePair<string, Ink.Runtime.Object> variable in dialogueVars) {
                    if(variable.Key == questKey) {
                        int val = ((Ink.Runtime.IntValue) this.GetVariableState(variable.Key)).value;
                        string keyVal = questKey+val;
                        OnQuestStarted?.Invoke(keyVal);
                        return;
                    }
                }
            }
        }
        //get the [0]th element on the string to get the questID 
        //make an event trigger to pass quests relating to the ID to the quest manager
        //start quests in quest mgr
    }

    private void GetVariablesToStory(Story story) {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in dialogueVars) {
            story.variablesState.SetGlobal(variable.Key,variable.Value);
            Debug.Log("Dictionary Key : "+variable.Key + " Dictionary Value : " +variable.Value);
        }
    }
}
