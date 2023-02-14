using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using FMOD.Studio;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputProvider;
    [SerializeField] private PostProcessingEffects postProcessingEffects;
    [SerializeField] private List<GameObject> speakers;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private List<CharacterUiBehaviour> characterUIs;
    [Header("Global Vars Ink File")]
    private GameObject speakerObj;
    private float textSpeed;
    private float textSpeedInMilSecs;
    private Story story;
    private TextMeshProUGUI nametag;
    private TextMeshProUGUI textBody;
    private List<string> tags; // = new List<string>();
    private bool dialogueIsPlaying;
    private bool isTyping; // = false;
    private string playerCurrentlySpeakingTo;
    private const string SPEAKER = "speaker";
    private const string PORTRAIT = "portrait";
    private const string LAYOUT = "layout";
    private const string SPEED = "speed";
    public static DialogueManager Instance;
    public static event Action<string> OnDialogueExit;
    //Dialogue Audio
    private EventInstance dialogueSFX;

    private void Awake() {
        if (Instance != null) {
            Debug.Log("More than One Dialogue Manager Instance Present");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        textBody = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        inputProvider.FindActionMap("UIActions").FindAction("Skip Dialogue").performed += UpdateDialogueSystem;
        tags = new List<string>();
        isTyping = false;

    }

    private void OnEnable() {
        inputProvider.FindAction("Skip Dialogue").Enable();
        DialogueTrigger.OnDialogueActivated += EnterDialogueMode;
    }
    void Start()
    {
        dialogueIsPlaying = false;
        dialogueBox.SetActive(false);
        //Audio, get FMOD event, start event and set dialogue status to "not talking"
        dialogueSFX = AudioManager.instance.CreateEventInstance(FMODEvents.instance.dialogueSFX);
        dialogueSFX.start();
        dialogueSFX.setParameterByName("DialogueStatus", 0f);
    }
    private void ManageTags() {
        tags = story.currentTags;
        foreach (var tag in tags) {
            if (tag.Split(":").Length != 2) {
                Debug.Log("Tag Incorrect :" + tag);
            }
            string tagKey = tag.Split(":")[0].Trim();
            string tagValue = tag.Split(":")[1].Trim();

            switch (tagKey) {
                case SPEAKER:
                    SetSpeakerName(tagValue);
                    break;
                case PORTRAIT:
                    SetPortrait(tagValue);
                    break;
                case LAYOUT:
                    AdjustPortrait(tagValue);
                    break;
                case SPEED:
                    SetTextSpeed(float.Parse(tagValue));
                    break;
                default:
                    break;
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, string characterName) {
        if (dialogueIsPlaying) {
            return;
        }
        else {
            story = new Story(inkJSON.text);
            playerCurrentlySpeakingTo = characterName;
            dialogueIsPlaying = true;
            postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceInteraction);
            QuestManager.Instance.dialogueVariables.StartListening(story);
            //speakers.SetActive(true);
            dialoguePanel.SetActive(true);
            dialogueBox.SetActive(true);
            ContinueStory();

        }
    }

    private void ExitDialogueMode() {
        OnDialogueExit?.Invoke(playerCurrentlySpeakingTo);
        dialogueIsPlaying = false;
        QuestManager.Instance.dialogueVariables.StopListening(story);
        postProcessingEffects.ChangeDepthOfField(postProcessingEffects.focusDistanceNormal);
        //speakers.SetActive(false);
        dialogueBox.SetActive(false);
        dialoguePanel.SetActive(false);
        textBody.text = "";
    }

    private void ContinueStory() {
        if (!story.canContinue && !isTyping) {
            ExitDialogueMode();
            return;
        }
        else if (isTyping) {
            StopAllCoroutines();
            textBody.text = story.currentText;
            isTyping = false;
        }
        else {
            story.Continue();
            ManageTags();
            StartCoroutine(DisplayText(story.currentText));
        }
    }

    private void SetSpeakerName(string tagValue) {
        speakerObj.transform.Find("CharacterNamePanel").gameObject.GetComponentInChildren<TextMeshProUGUI>().text = tagValue;
    }

    private void SetTextSpeed(float speedVal) {
        textSpeedInMilSecs = speedVal * 0.001f;
    }


    private void SetPortrait(string tagValue) {
        foreach (CharacterUiBehaviour characterUI in characterUIs) {
            if (characterUI.tagName == tagValue) {
                speakerObj.gameObject.GetComponentInChildren<Image>().sprite = characterUI.characterSprite;
                return;
            }
        }
    }

    private void AdjustPortrait(string tagValue) {
        if (tagValue == "Left") {
            foreach (GameObject speaker in speakers)
            {
                if (speaker.name == "CharacterPortrait")
                {
                    speakerObj = speaker;
                    speaker.SetActive(true);
                    speaker.transform.Find("CharacterImage").gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    speaker.SetActive(false);
                }
            }
        }
        else {
            foreach (GameObject speaker in speakers)
            {
                if (speaker.name == "CharacterPortraitNPC")
                {
                    speakerObj = speaker;
                    speaker.SetActive(true);
                    speaker.transform.Find("CharacterImage").gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    speaker.SetActive(false);
                }
            }

        }
    }

    private void UpdateDialogueSystem(InputAction.CallbackContext context) {
        if (!dialogueIsPlaying) {
            return;
        }
        ContinueStory();
    }

    private IEnumerator DisplayText(string currentText)
    {
        textBody.text = "";
        isTyping = true;
        
        char[] textArray = currentText.ToCharArray();
        foreach (char letter in textArray)

        {
            textBody.text += letter;
            yield return new WaitForSeconds(textSpeedInMilSecs);
            //Audio change dialogue state to "talking"
            dialogueSFX.setParameterByName("DialogueStatus", 1f);
        }
        isTyping = false;
        yield return null;
        //Audio change dialogue state to "not talking"
        dialogueSFX.setParameterByName("DialogueStatus", 0f);
    }

    private void OnDisable() {
        DialogueTrigger.OnDialogueActivated -= EnterDialogueMode;
        inputProvider.FindAction("Skip Dialogue").Disable();
    }

   

}
