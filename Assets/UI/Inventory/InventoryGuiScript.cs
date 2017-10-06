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
    private Dictionary<string, GameObject> equipSlots = new Dictionary<string, GameObject>();

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
        findEquipSlots();

    }
	
	// Update is called once per frame
	void Update () {
       // UpdateInventory();
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
        bool twoHander = false;
        foreach (ItemScript item in playerInv.itemList)
        {
           // print(item.name + " added to gui");   
            AddItemSlot(item);
            slotscounter++;
        }
        for(int i = 0; i < maxItemSlotsPerRow-(slotscounter % maxItemSlotsPerRow); i++){
            AddItemSlot(null);
        }transform.Find("HelmSlot").GetComponent<ItemSlotScript>().forceset(playerInv.Helm);
         transform.Find("Weapon1Slot").GetComponent<ItemSlotScript>().forceset(playerInv.Weapon1);
         transform.Find("Weapon2Slot").GetComponent<ItemSlotScript>().forceset(playerInv.Weapon2);
         transform.Find("ChestPieceSlot").GetComponent<ItemSlotScript>().forceset(playerInv.Chestpiece);
         transform.Find("GloveSlot").GetComponent<ItemSlotScript>().forceset(playerInv.Gloves);
         transform.Find("LegPieceSlot").GetComponent<ItemSlotScript>().forceset(playerInv.LegPiece);
        transform.Find("BootsSlot").GetComponent<ItemSlotScript>().forceset(playerInv.Boots);
        transform.Find("ActiveSlot").GetComponent<ItemSlotScript>().forceset(playerInv.Active);

        playerInv.hasChanged = false;
    }
    public void closeInventory()
    {
        

    }
    public void clearInventory()
    {
        //print("destroyingslots");

        foreach (GameObject slot in slots)
        {
           // print("destroyingslots");
            Destroy(slot);
        }
        slots.Clear();
        slots = new List<GameObject>();
    }
    private void findEquipSlots()
    {
        for(int i =0; i< gameObject.transform.childCount; i++){
            if (gameObject.transform.GetChild(i).tag.Equals("Slot"))
            {
                equipSlots.Add(gameObject.transform.GetChild(i).name, gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}
