using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //This is just a temporary script to load scenes in game

    [SerializeField] private float transitionTime = 2f;
    public Animator transition;

    public void LoadPlayScene()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadOuttro()
    {
        StartCoroutine(LoadLevel(2));
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
}
