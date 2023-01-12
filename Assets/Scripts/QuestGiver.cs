using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class QuestGiver : MonoBehaviour
{
    [SerializeField] private List<QuestGoals> quests;

    private void Awake() {
        Assert.IsNotNull(quests);
    }
    //because the QuestMgr instance is not created when onEnable is being executed, hence subscribing is delayed.
    void Start() {
        QuestManager.Instance.dialogueVariables.OnQuestStarted += AssignQuestsToMgr;
        foreach(QuestGoals quest in quests) {
            quest.questState = QuestGoals.QuestState.Untouched;
        }
    }
    public void AssignQuestsToMgr(string questGroup) {
        foreach(QuestGoals quest in quests) {
            if(quest.questGroup == questGroup) {
                QuestManager.Instance.AddQuestsToQuestList(quest);
            }
        }
    }

    private void OnDisable() {
        QuestManager.Instance.dialogueVariables.OnQuestStarted -= AssignQuestsToMgr;
    }
}
