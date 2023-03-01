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
            HideInventory();
        }
        else {
            ShowInventory();
        }
    }

    public void ShowInventory() {
        uiInventory.gameObject.SetActive(true);
        uiInventory.SetInventory(inventory);
        postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceInteraction);

        //Inventory open audio
        AudioManager.instance.PlayOneShot(FMODEvents.instance.inventoryClose, this.transform.position);
    }

    public void HideInventory() {
         postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceNormal);
        uiInventory.gameObject.SetActive(false);

        //Inventory close audio
        AudioManager.instance.PlayOneShot(FMODEvents.instance.inventoryClose, this.transform.position);
    }

    public void DisplayJournal()
    {
        if (uiJournal.gameObject.activeInHierarchy)
        {
            postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceNormal);
            uiJournal.gameObject.SetActive(false);
            //Journal close audio
            AudioManager.instance.PlayOneShot(FMODEvents.instance.journalClose, this.transform.position);
        }
        else
        {
            uiJournal.gameObject.SetActive(true);
            uiJournal.DisplayQuestList();
            postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceInteraction);
            //Journal open Audio
            AudioManager.instance.PlayOneShot(FMODEvents.instance.journalOpen, this.transform.position);
        }
    }

    public Inventory GetPlayerInventory() {
        return inventory;
    }
}
