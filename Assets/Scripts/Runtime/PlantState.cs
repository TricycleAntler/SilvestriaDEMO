using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantState : PlantBaseState
{
    private float timer;
    public override void EnterState(PlantLifeCycle plant, Transform transform, float initialYVal) {
        setPlantSprite(plant,plant.plantTemplate);
        transform.localScale = new Vector3(0.06f,0.06f,1f);
        float newYpos = initialYVal + 0.22f;
        transform.position = new Vector3(transform.position.x,newYpos,transform.position.z);
        timer  = plant.stateTransitionTime;
    }
    public override void UpdateState(PlantLifeCycle plant) {
        if(timer >= 0) {
            timer -= Time.deltaTime;
        }
        else {
            plant.SwitchState(plant.treeState);
        }
    }

    private void setPlantSprite(PlantLifeCycle plant,PlantTemplate template) {
        switch(template.plantType) {
            case PlantTemplate.PlantType.Berry :
                plant.plantSpriteRenderer.sprite = ItemAssets.Instance.plantSprite;
                break;
            case PlantTemplate.PlantType.Oak :
                plant.plantSpriteRenderer.sprite = ItemAssets.Instance.plantSprite;
                break;
            default:
                break;
        }
    }
}
