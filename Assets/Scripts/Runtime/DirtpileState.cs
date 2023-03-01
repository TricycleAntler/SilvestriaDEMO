using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtpileState : PlantBaseState
{
    private float timer;
    public override void EnterState(PlantLifeCycle plant, Transform transform, float initialYVal) {
        plant.plantSpriteRenderer.sprite = ItemAssets.Instance.dirtPileSprite;
        transform.localScale = new Vector3(0.06f,0.06f,1f);
        timer  = plant.stateTransitionTime;
    }
    public override void UpdateState(PlantLifeCycle plant) {
        if(timer >= 0) {
            timer -= Time.deltaTime;
        }
        else {
            plant.SwitchState(plant.seedState);
        }
    }
}
