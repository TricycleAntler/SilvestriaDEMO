using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    void Awake() {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }
    public void SetInventory(Inventory inventory){
        this.inventory = inventory;
        inventory.OnInventoryListChanged += UpdateInventory;
        RefreshInventoryItems();
    }

    public void UpdateInventory() {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems() {

        foreach(Transform child in itemSlotContainer) {
            if(child == itemSlotTemplate){
                continue;
            }
            Destroy(child.gameObject);
        }

        int x = 1;
        int y = 0;
        float itemSlotCellSize = 70f;
        foreach(Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI amountTxt = itemSlotRectTransform.Find("ItemAmountTxt").GetComponent<TextMeshProUGUI>();
            itemSlotRectTransform.GetComponent<ItemTemplate>().SetItemOnly(item);
            if(item.amount > 1) {
                amountTxt.SetText(item.amount.ToString());
            }
            else {
                amountTxt.SetText("");
            }

            x++;
            if(x > 3) {
                x = 1;
                y--;
            }
        }
    }

    void OnDisable() {
        inventory.OnInventoryListChanged -= UpdateInventory;
    }
}
