using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextAsset loadGlobalsJSON;
    [SerializeField] public List<QuestGoals> questList;
    public DialogueVariables dialogueVariables;
    public static QuestManager Instance;

    private void Awake() {
        if(Instance != null) {
            Debug.LogWarning("More than One Quest Manager Instance Present");
            Destroy(this.gameObject);
            return;
        }
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        Instance = this;
    }

    private void StartQuest() {
        foreach(QuestGoals questGoal in questList) {
            if(questGoal.questState == QuestGoals.QuestState.Untouched) {
                questGoal.StartQuest();
            }
        }
    }

    private bool CheckOverallQuestStatus(string questGroup) {
        bool allCompleted = true;
        foreach(QuestGoals quest in questList) {
            if(quest.questGroup == questGroup) {
                if(quest.questState != QuestGoals.QuestState.Completed) {
                    allCompleted = false;
                }
            }
        }
        return allCompleted;
    }

    //Keep track of planting quests
    public void CheckPlantingQuestStatus(int itemID) {
        if(questList == null) {
            return;
        }

        foreach(QuestGoals questGoal in questList) {
            if((questGoal.questState == QuestGoals.QuestState.Started) && (questGoal.questType == QuestGoals.QuestType.Planting)) {
                if(questGoal.itemID == itemID) {
                    //increment quest goal in a scriptable obj method and check if the quest is completed
                    questGoal.IncrementAmount();
                }
            }
        }
    }

    public void CheckCollectingQuestStatus(int itemID) {
        if(questList == null) {
            return;
        }

        foreach(QuestGoals questGoal in questList) {
            if((questGoal.questState == QuestGoals.QuestState.Started) && (questGoal.questType == QuestGoals.QuestType.Gathering)) {
                if(questGoal.itemID == itemID) {
                    questGoal.IncrementAmount();
                }
            }
        }
    }

    public void MarkQuestComplete(string questID, string questGroup) {
        //marks off individual quests as completed
        foreach(QuestGoals quest in questList) {
            if(quest.questID == questID) {
                if(quest.questState != QuestGoals.QuestState.Completed)
                    quest.QuestFinished();
            }
        }
        //check if all the tasks for the quest are done
        if(CheckOverallQuestStatus(questGroup)) {
            string questKey = dialogueVariables.CheckQuestIDExists(questGroup);
            if(questKey != null)
                    dialogueVariables.MarkQuestCompletedBool(questKey);
            else
                Debug.LogWarning("quest key is null.. something is wrong");
        }
    }

   public void AddQuestsToQuestList(QuestGoals goals) {
        questList.Add(goals);
        StartQuest();
   }
}
