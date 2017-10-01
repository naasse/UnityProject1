using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler {

    public ItemScript item=null;
    public ItemScript.ItemType accepting;
    public bool equiptSlot = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setItem(ItemScript setItem)
    {
        item = setItem;
        if (setItem!= null)
        {
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
            if (gameObject.name.Equals("HelmSlot")){
                gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(item, "Helm");
            }
            else if (gameObject.name.Equals("Weapon1Slot")){
                gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(item, "Weapon1");
            }
            else if (gameObject.name.Equals("LegPieceSlot"))
            {
                gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(item, "LegPiece");
            }
            else if (gameObject.name.Equals("ChestPieceSlot"))
            {
                gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(item, "ChestPiece");
            }
            else if (gameObject.name.Equals("GloveSlot"))
            {
                gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(item, "Gloves");
            }
            else if (gameObject.name.Equals("Weapon2Slot"))
            {
                gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(item, "Weapon2");
            }
            else if (gameObject.name.Equals("ActiveSlot"))
            {
                gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(item, "Active");
            }
            else if (gameObject.name.Equals("BootsSlot"))
            {
                gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(item, "Boots");
            }

        }
        else {
            print("null setItem");
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
        }


        
    }
 
    public void OnDrop (PointerEventData eventData)
    {
        Draghandler.dropSuccesful = true;
        print("got in OnDrop ");
        if (item==null)
        {
            print(Draghandler.item.name + " onDrop on " +this.name);
  
            setItem(Draghandler.item);
            Draghandler.returnParent();
            Draghandler.draggedItem.transform.parent.GetComponent<ItemSlotScript>().setItem(null);
            print("null onDrop on " + Draghandler.draggedItem.transform.parent.GetComponent<ItemSlotScript>().name);
        }
    }


}

