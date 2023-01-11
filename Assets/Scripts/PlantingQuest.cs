using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="PlantingQuest", menuName ="ScriptableObjects/PlantingQuest")]
public class PlantingQuest : QuestGoals
{
    [SerializeField] private int amountToPlant;
    private int currentPlantedAmount;
    public override void StartQuest()
    {
        Debug.Log("Quest Starting Planting Quest");
        questType = QuestType.Planting;
        Debug.Log("Quest Type : "+questType);
        currentPlantedAmount = 0;
        for(int i = 0; i< amountToPlant; i++) {
            //ItemTemplate.SpawnItem(new Vector3(428f,4.5f,577f), new Item{itemID = this.itemID, amount = 1});
        }
        questState = QuestState.Started;
        //throw new System.NotImplementedException();
    }

    public override void IncrementAmount() {
        currentPlantedAmount++;
        if(currentPlantedAmount >= amountToPlant) {
            //notice the quest mgr that the quest has successfully being completed
            QuestManager.Instance.MarkQuestComplete(this.questID,this.questGroup);
        }

    }
}
