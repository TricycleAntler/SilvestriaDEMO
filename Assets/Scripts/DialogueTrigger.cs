using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Player Input System")]
    [SerializeField] private InputActionAsset inputProvider;

    [Header("Ink Story")]
    [SerializeField] private TextAsset inkStory;

    [Header("Character Identifier")]
    [SerializeField] private string characterName;
    public static event Action<TextAsset,string> OnDialogueActivated; //event that triggers character dialogues

    private bool playerInRange;
    private bool dialogueActivated;

    private void Awake()
    {
        visualCue.SetActive(false);
        playerInRange = false;
        dialogueActivated = false;
        inputProvider.FindActionMap("UIActions").FindAction("Interact").performed += ActivateDialogueMode;
    }

    private void OnEnable()
    {
        inputProvider.FindAction("Interact").Enable();
        DialogueManager.OnDialogueExit += DialogueModeExited;
    }

    private void Update()
    {
        if(playerInRange)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    public void DialogueModeExited(string charName)
    {
        if(this.characterName == charName)
        {
            dialogueActivated = false;
        }
        
    }
    private void ActivateDialogueMode(InputAction.CallbackContext context)
    {
        if(playerInRange && !dialogueActivated)
        {
            OnDialogueActivated?.Invoke(inkStory,characterName);
            dialogueActivated = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

    private void OnDisable()
    {
        inputProvider.FindAction("Interact").Disable();
        DialogueManager.OnDialogueExit -= DialogueModeExited;
    }
}
