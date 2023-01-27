using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public float transitionTime = 2f;
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
