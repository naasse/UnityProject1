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

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedItem = gameObject;
        print(gameObject);
        if (draggedItem.transform.parent.GetComponent<ItemSlotScript>().item != null) {
            item = gameObject.transform.parent.GetComponent<ItemSlotScript>().item;
        print(item.name);
        startPosition = gameObject.transform.position;
        startParent = gameObject.transform.parent;
        gameObject.transform.SetParent(transform.root);
        draggedItem.transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        draggedItem.transform.position = startPosition;
        draggedItem.transform.SetParent(startParent);
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
        draggedItem.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        draggedItem = null;
        print("Clearingitem");
        item = null;


    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = Input.mousePosition;
    }
    public static void returnParent()
    {
        draggedItem.transform.position = startPosition;
        draggedItem.transform.SetParent(startParent);
    }
}
