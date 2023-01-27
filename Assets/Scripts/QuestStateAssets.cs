using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStateAssets : MonoBehaviour
{
    public static QuestStateAssets Instance { get; private set; }
    public Sprite questInProgressSprite;
    public Sprite questCompletedSprite;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            Instance = this;
        }
    }
}
