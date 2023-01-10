using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTemplate : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public static void SpawnItem(Vector3 position, Item item) {
        Transform transform = Instantiate(ItemAssets.Instance.itemTemplatePrefab,position, Quaternion.identity);
        ItemTemplate itemTemplate = transform.GetComponent<ItemTemplate>();
        itemTemplate.SetItemType(item);
        itemTemplate.SetItem(item);
        //return itemTemplate;
    }

    public void SetItemType(Item item) {
        switch(item.itemID){
            case 1 :
                item.itemType = Item.ItemType.Berry;
                break;
            case 2 :
                item.itemType = Item.ItemType.Oak;
                break;
            case 3 :
                item.itemType = Item.ItemType.DryGrass;
                break;
            case 4 :
                item.itemType = Item.ItemType.GreenGrass;
                break;
            default :
                break;
        }
    }

    public void SetItem(Item item) {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
   }

   public void SetItemOnly(Item item) {
        this.item = item;
   }

   public Item GetItem() {
    return item;
   }

   public void DestroyItemTemplate() {
        Destroy(this.gameObject);
   }
}
