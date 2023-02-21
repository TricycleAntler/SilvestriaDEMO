using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    // FMOD Events
    // SFX Events

    [field: Header("Pickup Berry SFX")]
    [field: SerializeField] public EventReference pickupBerrySound { get; private set; }

    [field: Header("Player Footstep SFX")]
    [field: SerializeField] public EventReference playerFootstep { get; private set; }

    [field: Header("Player AutoWalk SFX")]
    [field: SerializeField] public EventReference playerAutowalk { get; private set; }

    [field: Header("Quest popup SFX")]
    [field: SerializeField] public EventReference questPopup { get; private set; }

    [field: Header("Journal open SFX")]
    [field: SerializeField] public EventReference journalOpen { get; private set; }

    [field: Header("Journal close SFX")]
    [field: SerializeField] public EventReference journalClose { get; private set; }

    [field: Header("Inventory open SFX")]
    [field: SerializeField] public EventReference inventoryOpen { get; private set; }

    [field: Header("Inventory close SFX")]
    [field: SerializeField] public EventReference inventoryClose { get; private set; }

    [field: Header("Dialogue SFX")]
    [field: SerializeField] public EventReference dialogueSFX { get; private set; }

    [field: Header("Ambiance SFX")]
    [field: SerializeField] public EventReference ambianceSFX { get; private set; }

    //Music Events
    [field: Header("Music Player")]
    [field: SerializeField] public EventReference musicPlayer { get; private set; }







    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene!");
        }
        instance = this;
    }
}
