using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draghandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static GameObject draggedItem;
    private static Vector3 startPosition;
    private static Transform startParent;
    public static ItemScript item;
    public static bool replaceItem=false;
    public static bool droppedOnSlot = false;
    public static bool equipmentSlot = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        replaceItem = false;
        droppedOnSlot = false;
        draggedItem = gameObject;
        if (draggedItem.transform.parent.GetComponent<ItemSlotScript>().item != null) {
            item = gameObject.transform.parent.GetComponent<ItemSlotScript>().item;
            equipmentSlot = gameObject.transform.parent.GetComponent<ItemSlotScript>().equiptSlot;
            print("Dragging"+item.name);
        startPosition = gameObject.transform.position;
        startParent = gameObject.transform.parent;
        gameObject.transform.SetParent(transform.root);
        draggedItem.transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!droppedOnSlot)returnParent();
        
        draggedItem = null;
        item = null;
        print("enddrag");

    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = Input.mousePosition;
    }
    public static void returnParent()
    {
        draggedItem.transform.position = startPosition;
        draggedItem.transform.SetParent(startParent);
        draggedItem.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
