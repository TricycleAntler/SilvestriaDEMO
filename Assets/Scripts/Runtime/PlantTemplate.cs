using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTemplate : MonoBehaviour
{
    public enum PlantType {
    Berry,
    GreenGrass,
    DryGrass,
    Oak
    //Pine
  }
   private Plant plant;
   public PlantType plantType;

   public static void SpawnPlant(Vector3 position, Plant plant) {
        Transform transform = Instantiate(ItemAssets.Instance.plantTemplatePrefab,position, Quaternion.identity);
        PlantTemplate plantTemplate = transform.GetComponent<PlantTemplate>();
        plantTemplate.SetPlantType(plant);
        plantTemplate.SetPlant(plant);
    }

    public void SetPlantType(Plant plant) {
        switch(plant.plantID) {
            case 1 :
                plant.plantType = Plant.PlantType.Berry;
                break;
            case 2 :
                plant.plantType = Plant.PlantType.Oak;
                break;
            case 3 :
                plant.plantType = Plant.PlantType.DryGrass;
                break;
            case 4 :
                plant.plantType = Plant.PlantType.GreenGrass;
                break;
            default :
                break;
        }
    }

    private void SetPlant(Plant plant) {
        this.plant = plant;
    }
}

