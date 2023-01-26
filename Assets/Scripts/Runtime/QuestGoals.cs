using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestGoals : ScriptableObject
{
   //all quests which belongs to one larger quest has a group number, int.. Use that to send quests to quest
   public string questGroup; //an int value that can be used to group Bigger quests with multiple smaller quests
   public string questID;
   //questID should be unique
   public int itemID;
   public string questName;
   public string questDescription;

   public enum QuestType {Planting, Gathering };
   public QuestType questType;

   public enum QuestState {Untouched, Started, Completed};
   public QuestState questState = QuestState.Untouched;

   //method to change the state
   public abstract void StartQuest();
   public abstract void IncrementAmount();
   public virtual void QuestFinished() {
        questState = QuestState.Completed;
        //Debug.Log("Quest Completed :" + questID);
   }

    public virtual Sprite GetQuestStateSprite()
    {
        switch (questState)
        {
            default:
            case QuestState.Started:
                return QuestStateAssets.Instance.questInProgressSprite;
            case QuestState.Completed:
                return QuestStateAssets.Instance.questCompletedSprite;
        }
    }
}