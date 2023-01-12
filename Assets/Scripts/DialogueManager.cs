using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputProvider;
    [SerializeField] private PostProcessingEffects postProcessingEffects;
    [SerializeField] private GameObject speaker;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Animator dialoguePanelAnim;
    [SerializeField] private GameObject dialogueBox;
    [Header("Global Vars Ink File")]

    private float textSpeed;
    private float textSpeedInMilSecs;
    private float focusDistanceNormal = 3f;
    private float focusDistanceDialogueMode = 1f;
    private Story story;
    private TextMeshProUGUI nametag;
    private TextMeshProUGUI textBody;
    private List<string> tags = new List<string>();
    private bool dialogueIsPlaying;
    private bool isTyping = false;
    private string playerCurrentlySpeakingTo;
    private const string SPEAKER = "speaker";
    private const string PORTRAIT = "portrait";
    private const string LAYOUT = "layout";
    private const string SPEED = "speed";

    public static DialogueManager Instance;
    public List<CharacterUiBehaviour> characterUIs = new List<CharacterUiBehaviour>();
    public static event Action<string> OnDialogueExit;

    private void Awake() {
        if(Instance != null) {
            Debug.Log("More than One Dialogue Manager Instance Present");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        textBody = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        inputProvider.FindActionMap("UIActions").FindAction("Skip Dialogue").performed += UpdateDialogueSystem;

    }

    private void OnEnable() {
        inputProvider.FindAction("Skip Dialogue").Enable();
        DialogueTrigger.OnDialogueActivated += EnterDialogueMode;
    }
    void Start()
    {
        dialogueIsPlaying = false;
        dialogueBox.SetActive(false);
    }
    private void ManageTags() {
        tags = story.currentTags;
        foreach(var tag in tags){
            if(tag.Split(":").Length !=2) {
                Debug.Log("Tag Incorrect :"+tag);
            }
            string tagKey = tag.Split(":")[0].Trim();
            string tagValue = tag.Split(":")[1].Trim();

            switch(tagKey) {
                case SPEAKER:
                    SetSpeakerName(tagValue);
                    break;
                case PORTRAIT:
                    SetPortrait(tagValue);
                    SetPortraitAnimations(dialoguePanelAnim,tagValue);
                    break;
                case LAYOUT:
                    AdjustSpriteScale(tagValue);
                    break;
                case SPEED:
                    SetTextSpeed(float.Parse(tagValue));
                    break;
                default:
                    break;
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON,string characterName) {
        if(dialogueIsPlaying) {
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
        if(!story.canContinue && !isTyping) {
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
        speaker.transform.Find("CharacterNamePanel").gameObject.GetComponentInChildren<TextMeshProUGUI>().text = tagValue;
    }

    private void SetTextSpeed(float speedVal) {
        textSpeedInMilSecs = speedVal * 0.001f;
    }

    private void SetPortrait(string tagValue) {
        foreach(CharacterUiBehaviour characterUI in characterUIs) {
            if(characterUI.tagName == tagValue){
                speaker.gameObject.GetComponentInChildren<Image>().sprite = characterUI.characterSprite;
                return;
            }
        }
    }

    private void AdjustSpriteScale(string tagValue) {
        if(tagValue == "Left") {
            speaker.transform.Find("CharacterImage").gameObject.transform.localScale = new Vector3(-1,1,1);
        }
        else {
            speaker.transform.Find("CharacterImage").gameObject.transform.localScale = new Vector3(1,1,1);
        }
    }

    private void UpdateDialogueSystem(InputAction.CallbackContext context) {
        if(!dialogueIsPlaying) {
            return;
        }
        ContinueStory();
    }

    private void SetPortraitAnimations(Animator animator,string tagValue) {
        foreach(CharacterUiBehaviour characterUI in characterUIs) {
            if(characterUI.tagName == tagValue) {
                string animName = characterUI.animClip.name;
                foreach(AnimationClip clip in animator.runtimeAnimatorController.animationClips) {
                    if(clip.name == animName){
                        animator.Play(clip.name);
                        return;
                    }
                }
            }
        }
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
        }
        isTyping = false;
        yield return null;
    }

    private void OnDisable() {
        DialogueTrigger.OnDialogueActivated -= EnterDialogueMode;
        inputProvider.FindAction("Skip Dialogue").Disable();
    }

}
