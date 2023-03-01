using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLifeCycle : MonoBehaviour
{
    PlantBaseState currentState;
    public DirtpileState dirtpileState = new DirtpileState();
    public SeedState seedState = new SeedState();
    public SapleState sapleState = new SapleState();
    public PlantState plantState = new PlantState();
    public TreeState treeState = new TreeState();

    //min and max times for transitions for now would be 5 and 10 mins
    private float minTime = 5f;
    private float maxTime = 10f;
    private float initYVal;
    private float totalTimeForPlantToGrow;
    public float stateTransitionTime;
    public SpriteRenderer plantSpriteRenderer;
    public PlantTemplate plantTemplate;
    void Awake() {
        initYVal = this.transform.position.y;
        totalTimeForPlantToGrow = Random.Range(minTime,maxTime);
        stateTransitionTime = (float)(totalTimeForPlantToGrow /4) * 60;
        plantSpriteRenderer = GetComponent<SpriteRenderer>();
        plantTemplate = GetComponent<PlantTemplate>();
    }
    void Start()
    {
        currentState = dirtpileState;
        currentState.EnterState(this,this.transform,initYVal);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlantBaseState state) {
        currentState = state;
        currentState.EnterState(this,this.transform,initYVal);
    }
}
