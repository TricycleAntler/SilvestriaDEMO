using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    void Awake() {
        inventory = new Inventory();

        //testing
        ItemTemplate.SpawnItem(new Vector3(428f,4.5f,577f), new Item{itemType = Item.ItemType.Oak, amount = 1});
    }

    public void DisplayInventory() {
        if(uiInventory.gameObject.activeInHierarchy){
            uiInventory.gameObject.SetActive(false);
        }
        else {
            uiInventory.gameObject.SetActive(true);
            uiInventory.SetInventory(inventory);
        }
    }

    public Inventory GetPlayerInventory() {
        return inventory;
    }
}
