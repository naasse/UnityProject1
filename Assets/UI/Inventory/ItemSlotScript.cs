using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler {
    public ItemScript item=null;

    public bool equiptSlot = false;
    public Equipable.EquipType slotType =Equipable.EquipType.none;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool setItem(ItemScript setItem)
    {
        //if (setItem) print("Trying to set " + setItem);
        //else print("Tying to set Empty");
        bool canSet = true;
        if (equiptSlot)
        {
            if (setItem)
            {
                if (slotType.Equals(((Equipable)setItem.tags[ItemTag.TagTypes.Equipable]).type))
                {
                    canSet = gameObject.transform.root.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv.equipt(setItem, gameObject.name);
                    print("Correct Slot Type/ Able To Equipt:" + canSet);


                }
                else
                {
                    canSet = false;
                    print("Incorrect Slot Type");
                }
            }
        }
        if (canSet)
        {
            item = setItem;
            UpdateImage();
        }
        return canSet;

        
    }
 
    public void UpdateImage()
    {

        if (item)
        {
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled =true;
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
        }
    }
    public void forceset(ItemScript item)
    {
        this.item = item;
        UpdateImage();
    }
    public void OnDrop (PointerEventData eventData)
    {
        if (equiptSlot)
        {
            handleItemSet();
        }
        else handleItemSet();

    }
    private void handleItemSet()
    {
        Draghandler.droppedOnSlot = true;
            print(Draghandler.item.name + " onDrop on " + this.name);
        if (item) print("Currently hodling " + item.name);
        else print("Currently Holding Nothing");
            ItemScript temp = item;
            Draghandler.returnParent();
        if (setItem(Draghandler.item))
        {
            print(Draghandler.draggedItem.transform.parent.name +" and set item =true");
            Draghandler.replaceItem = true;
            if (temp)
            {
                Draghandler.draggedItem.transform.parent.GetComponent<ItemSlotScript>().setItem(temp);
                print("assinging temp to old slot");
            }
            else
            {
                Draghandler.draggedItem.transform.parent.GetComponent<ItemSlotScript>().setItem(null);
                print("assinging null to old slot");
            }
            }
        //print("null onDrop on " + Draghandler.draggedItem.transform.parent.GetComponent<ItemSlotScript>().name);
    }


}

