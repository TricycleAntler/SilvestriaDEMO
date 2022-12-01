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

    public static ItemTemplate SpawnItem(Vector3 position, Item item) {
        Transform transform = Instantiate(ItemAssets.Instance.itemTemplatePrefab,position, Quaternion.identity);

        ItemTemplate itemTemplate = transform.GetComponent<ItemTemplate>();
        itemTemplate.SetItem(item);
        return itemTemplate;
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
