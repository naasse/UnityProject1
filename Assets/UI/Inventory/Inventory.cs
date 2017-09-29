using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

    public ItemScript Helm;
    public ItemScript Sword;

    public PlayerInventory playerInv;

    [SerializeField]
   public GameObject ItemContent;
    private int maxItemSlotsPerRow = 5;

    private List<GameObject> slots = new List<GameObject>();

    public void AddItemSlot(ItemScript item)
    {
        GameObject slot = Instantiate(ItemContent) as GameObject;
        slot.SetActive(true);
        slot.GetComponent<ItemSlotScript>().setItem(item);
        slot.transform.SetParent(ItemContent.transform.parent, false);
        slots.Add(slot);
 
    }


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pickup(ItemScript newItem)
    {
        AddItemSlot(newItem);
    }

    public ItemScript drop(ItemScript removeItem,Vector3 dropPos)
    {

        return null;
    }

    public void ViewInventory()
    {

    }
    public void UpdateInventory(PlayerInventory inv)
    {
        int slotscounter = 0;
        clearInventory();
        playerInv = inv;
        foreach (ItemScript item in inv.itemList)
        {
            AddItemSlot(item);
            slotscounter++;
        }
        for(int i = 0; i < maxItemSlotsPerRow-(slotscounter % maxItemSlotsPerRow); i++){
            AddItemSlot(null);
        }
        if (inv.Helm != null)
        {
            this.Helm = inv.Helm;
            transform.Find("HelmSlot").GetComponent<ItemSlotScript>().setItem(inv.Helm);
            
        }
        if (inv.Sword != null)
        {
            this.Sword = this.Helm;
            transform.Find("WeaponSlot").GetComponent<ItemSlotScript>().setItem(inv.Sword);
        }
        inv.hasChanged = false;
    }
    public void closeInventory()
    {
        

    }
    public void clearInventory()
    {
        foreach (GameObject slot in slots)
        {
            Destroy(slot);
            slots.Clear();
        }
    }

}
