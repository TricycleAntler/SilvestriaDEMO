using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapleState : PlantBaseState
{
    private float timer;
    public override void EnterState(PlantLifeCycle plant, Transform transform, float initialYVal) {
        plant.plantSpriteRenderer.sprite = ItemAssets.Instance.saplingSprite;
        transform.localScale = new Vector3(0.04f,0.04f,1f);
        float newYpos = initialYVal - 0.16f;
        transform.position = new Vector3(transform.position.x,newYpos,transform.position.z);
        timer  = plant.stateTransitionTime;
    }
    public override void UpdateState(PlantLifeCycle plant) {
        if(timer >= 0) {
            timer -= Time.deltaTime;
        }
        else {
            plant.SwitchState(plant.plantState);
        }

    }
}
