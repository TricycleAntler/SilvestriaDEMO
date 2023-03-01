using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant {
    public enum PlantType {
    Berry,
    GreenGrass,
    DryGrass,
    Oak
    //Pine
  }

  public int plantID;
  //plant id for berries =1, oak =2,
  public PlantType plantType;
}
