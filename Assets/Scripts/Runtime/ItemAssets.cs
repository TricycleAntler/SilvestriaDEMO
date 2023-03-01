using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{

    [Header("Spawnable Item Assets")]
    public Sprite berrySprite;
    public Sprite greenGrassSprite;
    public Sprite dryGrassSprite;
    public Sprite oakSprite;
    public Transform itemTemplatePrefab;
    public Transform plantTemplatePrefab;

    [Header("Plant States")]

    //For Berry Tree
    public Sprite dirtPileSprite;
    public Sprite seedSprite;
    public Sprite saplingSprite;
    public Sprite plantSprite;
    public Sprite treeSprite;
    //TODO : Add other plant assets here

    public static ItemAssets Instance {get; private set;}
    void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);

        }
        else {
            Instance = this;
        }
    }

}
