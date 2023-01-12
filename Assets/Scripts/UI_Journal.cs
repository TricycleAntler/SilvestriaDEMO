using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Journal : MonoBehaviour
{
    private Transform questSlotContainer;
    private Transform questSlotTemplate;

    private void Awake()
    {
        questSlotContainer = transform.Find("QuestSlotContainer");
        questSlotTemplate = questSlotContainer.Find("QuestSlotTemplate");
    }

    public void DisplayQuestList()
    {
        foreach (Transform child in questSlotContainer)
        {
            if (child == questSlotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);

        }
        int x = 0;
        int y = 0;
        float questSlotCellSize = 70f;
        foreach (QuestGoals quest in QuestManager.Instance.questList)
        {
            RectTransform questSlotRectTransform = Instantiate(questSlotTemplate, questSlotContainer).GetComponent<RectTransform>();
            questSlotRectTransform.gameObject.SetActive(true);
            questSlotRectTransform.anchoredPosition = new Vector2(0 * questSlotCellSize, y * questSlotCellSize);
            TextMeshProUGUI questName = questSlotRectTransform.Find("QuestNameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI questDescription = questSlotRectTransform.Find("QuestDescriptionText").GetComponent<TextMeshProUGUI>();
            Image image = questSlotRectTransform.Find("QuestStatusImg").GetComponent<Image>();
            image.sprite = quest.GetQuestStateSprite();
            questName.SetText(quest.questName.ToString());
            questDescription.SetText(quest.questDescription.ToString());
            //code to move the items to the next page should be written.
            y--;
            x++;
            if (x > 1)
            {
                x = 0;
            }
        }
    }


}
