using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropDetector : MonoBehaviour, IDropHandler
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDrop(PointerEventData eventData)
    {
        Draghandler.replaceItem = true;
        print("DroppingItem");
        Draghandler.returnParent();
        gameObject.transform.parent.transform.Find("Inventory").GetComponent<InventoryGuiScript>().drop(Draghandler.draggedItem.transform.parent.GetComponent<ItemSlotScript>().item);
        Draghandler.draggedItem.transform.parent.GetComponent<ItemSlotScript>().setItem(null);
    }
}
