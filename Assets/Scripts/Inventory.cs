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

    public List<Item> GetItemList() {
        return itemList;
    }
}
