using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeState : PlantBaseState
{
    private float timer;
    public override void EnterState(PlantLifeCycle plant,Transform transform, float initialYVal) {
        //TODO : Change the sprite of the plant to dirtpile using ItemAsset sprites
        setTreeSprite(plant, plant.plantTemplate);
        transform.localScale = new Vector3(0.25f,0.25f,1f);
        float newYpos = initialYVal + 1.94f;
        transform.position = new Vector3(transform.position.x,newYpos,transform.position.z);
        timer  = plant.stateTransitionTime;
        //Once the timer is over switch to the next state
    }
    public override void UpdateState(PlantLifeCycle plant) {

    }

    private void setTreeSprite(PlantLifeCycle plant,PlantTemplate template) {
        switch(template.plantType) {
            case PlantTemplate.PlantType.Berry :
                plant.plantSpriteRenderer.sprite = ItemAssets.Instance.treeSprite;
                break;
            case PlantTemplate.PlantType.Oak :
                plant.plantSpriteRenderer.sprite = ItemAssets.Instance.treeSprite;
                break;
            default:
                break;
        }
    }
}
