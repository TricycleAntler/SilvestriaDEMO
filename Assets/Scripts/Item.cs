using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
  public enum ItemType {
    Berry,
    GreenGrass,
    DryGrass,
    Oak
    //Pine
  }

  public ItemType itemType;
  public int amount;

   public Sprite GetSprite() {
        switch(itemType) {
          default :
          case ItemType.Berry :
            return ItemAssets.Instance.berrySprite;
          case ItemType.DryGrass :
            return ItemAssets.Instance.dryGrassSprite;
          case ItemType.GreenGrass :
            return ItemAssets.Instance.greenGrassSprite;
          case ItemType.Oak :
            return ItemAssets.Instance.oakSprite;
        }
    }

  public bool IsStackable() {
    switch(itemType) {
      default:
      case ItemType.Berry :
      case ItemType.Oak :
        return true;
      case ItemType.DryGrass :
      case ItemType.GreenGrass:
        return false;
    }
  }
}
