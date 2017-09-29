using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler {

    public ItemScript item=null;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setItem(ItemScript item)
    {
        print(gameObject.name);
        this.item = item;
        if (item != null)
        {
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = item.sprite;
            gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
            if (gameObject.name.Equals("HelmSlot")){
                gameObject.transform.root.Find("Inventory").GetComponent<Inventory>().playerInv.equipt("Helm", item);
            }
            else if (gameObject.name.Equals("WeaponSlot")){
                gameObject.transform.root.Find("Inventory").GetComponent<Inventory>().playerInv.equipt("Sword", item);
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
        print("got in OnDrop ");
        if (item==null)
        {
            print(Draghandler.item.name + " onDrop");
            setItem(Draghandler.item);
            Draghandler.returnParent();
            Draghandler.draggedItem.transform.parent.GetComponent<ItemSlotScript>().setItem(null);
        }
    }


}

