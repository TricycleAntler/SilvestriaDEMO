using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    private List<Item> itemList;
    public event Action OnInventoryListChanged;

    public Inventory() {
        itemList = new List<Item>();

        AddItem(new Item {itemType = Item.ItemType.Berry, amount = 2});
        Debug.Log("Inventory");

    }

    public void AddItem(Item item) {
        if(item.IsStackable()) {
            bool itemAlreadyExistInInventory = false;
            foreach(Item invItem in itemList) {
                if(invItem.itemType == item.itemType) {
                    invItem.amount += item.amount;
                    itemAlreadyExistInInventory = true;
                }
            }
            if(itemAlreadyExistInInventory == false) {
                itemList.Add(item);
            }
        }
        else {
            itemList.Add(item);
        }
        Debug.Log("item Added");
        OnInventoryListChanged?.Invoke();
    }

    public void DropItem(Item item) {
        if(item.IsStackable()) {
            Item inventoryItem = null;
            for(int i=0; i< itemList.Count; i++) {
                if(itemList[i].itemType == item.itemType) {
                    itemList[i].amount -= 1;
                    inventoryItem = itemList[i];
                }
                if(inventoryItem != null && inventoryItem.amount <= 0){
                    itemList.Remove(inventoryItem);
                }
            }
        }
        else {
            itemList.Remove(item);
        }
        Debug.Log("item Removed");
        OnInventoryListChanged?.Invoke();
    }

    public List<Item> GetItemList() {
        return itemList;
    }
}
