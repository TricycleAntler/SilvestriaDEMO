using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GatheringQuest", menuName ="ScriptableObjects/GatheringQuest")]
public class GatheringQuests : QuestGoals
{
    [SerializeField] private int amountToCollect;
    private int currentCollectedAmount;
    public override void StartQuest()
    {
        questType = QuestType.Gathering;
        currentCollectedAmount = 0;
        for(int i = 0; i< amountToCollect; i++) {
            ItemTemplate.SpawnItem(new Vector3(428f,4.5f,577f), new Item{itemID = this.itemID, amount = 1});
            Debug.Log("Instantiated");
        }
        questState = QuestState.Started;
        //throw new System.NotImplementedException();
    }
    public override void IncrementAmount() {
        currentCollectedAmount++;
        if(currentCollectedAmount >= amountToCollect) {
            //notice the quest mgr that the quest has successfully being completed
            QuestManager.Instance.MarkQuestComplete(this.questID,this.questGroup);
        }

    }
}
