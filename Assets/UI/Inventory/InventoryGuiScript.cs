using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryGuiScript : MonoBehaviour {


    public UnitInventory playerInv;

    [SerializeField]
    public GameObject ItemContent;
    private int maxItemSlotsPerRow = 5;

    private List<GameObject> slots = new List<GameObject>();

    public void AddItemSlot(ItemScript item)
    {
        GameObject slot = Instantiate(ItemContent) as GameObject;
        slot.SetActive(true);
        slot.GetComponent<ItemSlotScript>().setItem(item);
        slot.transform.SetParent(transform.Find("ItemList/Scroll View/Viewport/Content"),false);
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

    public ItemScript drop(ItemScript removeItem)
    {

        playerInv.drop(playerInv.unit.transform.position, removeItem);
        return removeItem;
    }

    public void ViewInventory()
    {

    }
    public void UpdateInventory()
    {
        int slotscounter = 0;
        clearInventory();
        foreach (ItemScript item in playerInv.itemList)
        {
            print(item.name + " added to gui");   
            AddItemSlot(item);
            slotscounter++;
        }
        for(int i = 0; i < maxItemSlotsPerRow-(slotscounter % maxItemSlotsPerRow); i++){
            AddItemSlot(null);
        }
        if (playerInv.Helm != null)
        {
            transform.Find("HelmSlot").GetComponent<ItemSlotScript>().setItem(playerInv.Helm);
            
        }
        if (playerInv.Weapon1 != null)
        {
            transform.Find("Weapon1Slot").GetComponent<ItemSlotScript>().setItem(playerInv.Weapon1);
        }
        if (playerInv.Weapon2 != null)
        {
            transform.Find("Weapon2Slot").GetComponent<ItemSlotScript>().setItem(playerInv.Weapon2);
        }
        if (playerInv.Chestpiece!= null)
        {
            transform.Find("ChestPieceSlot").GetComponent<ItemSlotScript>().setItem(playerInv.Chestpiece);

        }
        if (playerInv.Gloves != null)
        {
            transform.Find("GlovesSlot").GetComponent<ItemSlotScript>().setItem(playerInv.Gloves);
        }
        if (playerInv.LegPiece != null)
        {
            transform.Find("LegPiecelot").GetComponent<ItemSlotScript>().setItem(playerInv.LegPiece);

        }
        if (playerInv.Boots != null)
        {
            transform.Find("BootsSlot").GetComponent<ItemSlotScript>().setItem(playerInv.Boots);
        }
        if (playerInv.Active != null)
        {
            transform.Find("ActiveSlot").GetComponent<ItemSlotScript>().setItem(playerInv.Active);
        }
        playerInv.hasChanged = false;
    }
    public void closeInventory()
    {
        

    }
    public void clearInventory()
    {
        foreach (GameObject slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();
    }

}
