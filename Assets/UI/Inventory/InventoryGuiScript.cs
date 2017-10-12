using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryGuiScript : MonoBehaviour {


    public UnitInventory playerInv;

    [SerializeField]
    public GameObject ItemContent;
    private int extraItems = 5;

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
       //UpdateInventory();
	}

    public void pickup(ItemScript newItem)
    {
        bool set = false;
        print(gameObject.transform.root.Find("Inventory/ItemList/Scroll View/Viewport/Content").transform.childCount);
        for (int k = 0; k < gameObject.transform.root.Find("Inventory/ItemList/Scroll View/Viewport/Content").transform.childCount; k++)
        {
            ItemSlotScript script = gameObject.transform.root.Find("Inventory/ItemList/Scroll View/Viewport/Content").transform.GetChild(k).GetComponent<ItemSlotScript>();
            if (script.item == null)
            {
                print("found a slot");
                script.setItem(newItem);
                set = true;
                break;
            }
        }
        if(!set)AddItemSlot(newItem);
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
        int slotscounter =playerInv.itemList.Count;
        bool twoHander = false;
        for(int k =0;k< gameObject.transform.root.Find("Inventory/ItemList/Scroll View/Viewport/Content").transform.childCount; k++)
        {
            gameObject.transform.root.Find("Inventory/ItemList/Scroll View/Viewport/Content").transform.GetChild(k).GetComponent<ItemSlotScript>().UpdateImage();
        }
        for (int i = gameObject.transform.root.Find("Inventory/ItemList/Scroll View/Viewport/Content").transform.childCount-1; i < slotscounter+extraItems; i++){
            AddItemSlot(null);
        }
        transform.Find("HelmSlot").GetComponent<ItemSlotScript>().UpdateImage();
        transform.Find("Weapon1Slot").GetComponent<ItemSlotScript>().UpdateImage();
        transform.Find("Weapon2Slot").GetComponent<ItemSlotScript>().UpdateImage();
        transform.Find("ChestPieceSlot").GetComponent<ItemSlotScript>().UpdateImage();
        transform.Find("GloveSlot").GetComponent<ItemSlotScript>().UpdateImage();
        transform.Find("LegPieceSlot").GetComponent<ItemSlotScript>().UpdateImage();
        transform.Find("BootsSlot").GetComponent<ItemSlotScript>().UpdateImage();
        transform.Find("ActiveSlot").GetComponent<ItemSlotScript>().UpdateImage();

        transform.parent.Find("ArmorText").GetComponent<Text>().text = "Armor=" + playerInv.unit.armor;
        transform.parent.Find("DamageText").GetComponent<Text>().text = "Damage=" + playerInv.unit.damage;
        playerInv.hasChanged = false;
    }
    public void closeInventory()
    {
        

    }
    private void findEquipSlots()
    {
        for(int i =1; i< gameObject.transform.childCount; i++){
            if (gameObject.transform.GetChild(i).tag.Equals("Slot"))
            {
                equipSlots.Add(gameObject.transform.GetChild(i).name, gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}
