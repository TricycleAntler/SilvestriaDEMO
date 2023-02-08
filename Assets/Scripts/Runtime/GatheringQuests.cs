using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GatheringQuest", menuName ="ScriptableObjects/GatheringQuest")]
public class GatheringQuests : QuestGoals
{
    [SerializeField] private int amountToCollect;
    private int currentCollectedAmount;
    public LayerMask groundLayer;
    public override void StartQuest()
    {
        //Debug.Log("Quest Starting Gathering Quest");
        questType = QuestType.Gathering;
        //Debug.Log("Quest Type : "+questType);
        currentCollectedAmount = 0;
        for(int i = 0; i< amountToCollect; i++) {
            Vector3 spawnPosition = ItemTemplate.CalculateSpawnPosition();
            RaycastHit hit;
            if(Physics.Raycast(spawnPosition, Vector3.down,out hit,Mathf.Infinity,groundLayer)) {
                float yPosition = hit.point.y + 0.5f;
                spawnPosition.y = yPosition;
                ItemTemplate.SpawnItem(spawnPosition, new Item{itemID = this.itemID, amount = 1});
            }
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
