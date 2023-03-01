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
    }

    public void AddItem(Item item) {
        if(item.IsStackable()) {
            bool itemAlreadyExistInInventory = false;
            Debug.Log(itemAlreadyExistInInventory); //inv check TODO : REMOVE COMMENT
            foreach(Item invItem in itemList) {
                if(invItem.itemType == item.itemType) {
                    invItem.amount += 1;
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
        OnInventoryListChanged?.Invoke();
    }

    public List<Item> GetItemList() {
        return itemList;
    }
}
