using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedState : PlantBaseState
{
    private float timer;
    public override void EnterState(PlantLifeCycle plant, Transform transform, float initialYVal) {
        plant.plantSpriteRenderer.sprite = ItemAssets.Instance.seedSprite;
        transform.localScale = new Vector3(0.03f,0.03f,1f);
        float newYpos = initialYVal - 0.15f;
        transform.position = new Vector3(transform.position.x,newYpos,transform.position.z);
        timer  = plant.stateTransitionTime;
    }
    public override void UpdateState(PlantLifeCycle plant) {
        if(timer >= 0) {
            timer -= Time.deltaTime;
        }
        else {
            plant.SwitchState(plant.sapleState);
        }
    }
}
