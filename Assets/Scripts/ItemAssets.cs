using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance {get; private set;}
    public Sprite berrySprite;
    public Sprite greenGrassSprite;
    public Sprite dryGrassSprite;
    public Sprite oakSprite;

    public Transform itemTemplatePrefab;

    void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);

        }
        else {
            Instance = this;
        }
    }

}
