using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private PostProcessingEffects postProcessingEffects;
    [SerializeField] private UI_Journal uiJournal;
    private Inventory inventory;
    void Awake() {
        inventory = new Inventory();

        //testing
        //ItemTemplate.SpawnItem(new Vector3(428f,4.5f,577f), new Item{itemID = 1, amount = 1});

    }

    public void DisplayInventory() {
        if(uiInventory.gameObject.activeInHierarchy){
            postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceNormal);
            uiInventory.gameObject.SetActive(false);
        }
        else {
            uiInventory.gameObject.SetActive(true);
            uiInventory.SetInventory(inventory);
            postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceInteraction);
        }
    }

    public void DisplayJournal()
    {
        if (uiJournal.gameObject.activeInHierarchy)
        {
            postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceNormal);
            uiJournal.gameObject.SetActive(false);
        }
        else
        {
            uiJournal.gameObject.SetActive(true);
            uiJournal.DisplayQuestList();
            postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceInteraction);
        }
    }

    public Inventory GetPlayerInventory() {
        return inventory;
    }
}
