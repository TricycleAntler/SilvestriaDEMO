using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;

public class LevelLoader : MonoBehaviour
{
    //This is just a temporary script to load scenes in game

    [SerializeField] private float transitionTime = 2f;
    public Animator transition;
    //Sound and music
    private EventInstance ambianceSFX;
    private EventInstance musicPlayer;


    public void LoadPlayScene()
    {
        StartCoroutine(LoadLevel(1));
        ambianceSFX.start();
        musicPlayer.start();
    }

    public void LoadOuttro()
    {
        StartCoroutine(LoadLevel(2));
        //musicPlayer.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //ambianceSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

    }

    public void LoadIntro()
    {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    private void Start()
    {
        //Audio and music
        ambianceSFX = AudioManager.instance.CreateEventInstance(FMODEvents.instance.ambianceSFX);
        ambianceSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        musicPlayer = AudioManager.instance.CreateEventInstance(FMODEvents.instance.musicPlayer);
        
    }
}
