using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static event Action<bool> OnUIActionStart;
    public static event Action<Vector3> OnSeedDrop;
    private RectTransform rectTransform;
    private Transform itemPosition;
    private float itemSpawnPosY = 4.29f;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Player player;
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        itemPosition = GetComponent<Transform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        OnUIActionStart?.Invoke(true);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
       rectTransform.anchoredPosition += eventData.delta / uiCanvas.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On End Drag");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 10f,layerMask)){
            Debug.Log("Hit the Ground");
            Inventory inventory = player.GetPlayerInventory();
            //drop item from inventory
            //create planting tree game object and place it here
            //float x = hit.point.x;
            //float z = hit.point.z;
            //Vector3 spawnPosition = new Vector3(x,itemSpawnPosY,z);
            //ItemTemplate.SpawnItem(spawnPosition,GetComponent<ItemTemplate>().GetItem()); //change this to the tree object/anim that should be spawned
            OnSeedDrop?.Invoke(hit.point);
            inventory.DropItem(GetComponent<ItemTemplate>().GetItem());
        }
        else {
            //should work when the object is placed on a different layer other than ground
            transform.localPosition = Vector3.zero;
        }
        OnUIActionStart?.Invoke(false);
    }
}
