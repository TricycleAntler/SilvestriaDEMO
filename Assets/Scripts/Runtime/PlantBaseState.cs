using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantBaseState
{
    public abstract void EnterState(PlantLifeCycle plant, Transform transform,float initialYVal);
    public abstract void UpdateState(PlantLifeCycle plant);

}
