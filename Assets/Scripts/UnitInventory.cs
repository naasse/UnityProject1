using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : MonoBehaviour {
    public GameObject inventoryTemplate;
    private GameObject inventory = null;
    public bool inventoryOpen = false;

    public List<ItemScript> itemList;

    public Armor Helm = null;
    public Weapon Weapon1 = null;
    public Weapon Weapon2 = null;
    public Armor Chestpiece = null;
    public Armor LegPiece = null;
    public Armor Boots = null;
    public Armor Gloves = null;
    public Active Active = null;

    private int numHands=2;
    public UnitScript unit;
    public bool hasChanged = false;
    // Use this for initialization
    void Start() {
        inventory = Instantiate(inventoryTemplate) as GameObject;
        inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().playerInv = this;
        itemList = new List<ItemScript>();
    }

    // Update is called once per frame
    void Update() {
        if (hasChanged && inventoryOpen) inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().UpdateInventory();
    }
    public void ChangeInventoryState()
    {
        if (inventoryOpen)
        {
            CloseInventory();

        }
        else OpenInventory();
    }
    private  void OpenInventory()
    {
        inventory.SetActive(true);
        inventoryOpen = true;
        if(hasChanged) inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().UpdateInventory();
    }
    private  void CloseInventory()
    {
        inventory.SetActive(false);
        inventoryOpen = false;
    }

    public GameObject getInventory()
    {
        return inventory;
    }

    public void pickup(ItemScript item)
    {
        itemList.Add(item);
        item.gameObject.SetActive(false);
        hasChanged = true;
        inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().pickup(item);
        print("added " + item.name + " to inventory");

    }

    public ItemScript drop(Vector3 location, ItemScript item)
    {
        ItemScript temp= itemList.Find(item.Equals);
        itemList.Remove(item);
        itemList.TrimExcess();
        temp.transform.position = location;
        temp.gameObject.SetActive(true);
        hasChanged = true;
        //inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().UpdateInventory();
        return temp;

    }

    public void dequipt(string slot)
    {
        print("dequipting " + slot);
            switch (slot)
            {
                case "Weapon1Slot":
                    if(Weapon1!=null) itemList.Add(Weapon1);
                    Weapon1 = null;
                    break;
                case "Weapon2Slot":
                if (Weapon2 != null) itemList.Add(Weapon2);
                    Weapon2 = null;
                    break;
                case "HelmSlot":

                if (Helm != null) itemList.Add(Helm);
                    Helm = null;
                    break;
                case "ChestPieceSlot":
                if (Chestpiece != null) itemList.Add(Chestpiece);
                    Chestpiece = null;
                    break;
                case "LegPieceSlot":
                if (LegPiece != null) itemList.Add(LegPiece);
                    LegPiece = null;
                    break;
                case "BootsSlot":
                if (Boots != null) itemList.Add(Boots);
                Boots = null;
                    break;
                case "GloveSlot":
                if (Gloves != null) itemList.Add(Gloves);
                    Gloves = null;
                    break;
                case "ActiveSlot":
                if (Active != null) itemList.Add(Active);
                    Active = null;
                    break;
            default:
                break;

            }


            hasChanged = true;
        
    }
    public void setUnit(UnitScript unit)
    {
        this.unit = unit;
    }

    public bool equipt(ItemScript item, string slotname)
    {
        bool changed = false;

        switch (((Equipment)item).type) { 
            case Equipment.EquipType.Weapon:
                Weapon temp = (Weapon)item;
                if (temp.requiredHands == 1)
                {
                    if (slotname.Equals("Weapon1Slot"))
                    {
                        dequipt("Weapon1Slot");
                        Weapon1 = temp;
                        changed = true;
                    }
                    else  if (slotname.Equals("Weapon2Slot"))
                    {
                        dequipt("Weapon2Slot");
                        Weapon2 = temp;
                        changed = true;
                    }
                }
                else if (temp.requiredHands == 2)
                {
                    dequipt("Weapon1Slot");
                    dequipt("Weapon2Slot");
                    Weapon1 = temp;
                    changed = true;
                }
                break;
            case Equipment.EquipType.Helm:
                dequipt("HelmSlot");
                Helm = (Armor)item;
                changed = true;
                break;
            case Equipment.EquipType.Glove:
                dequipt("GloveSlot");
                Gloves = (Armor)item;
                changed = true;
                break;
            case Equipment.EquipType.ChestPiece:
                dequipt("ChestPieceSlot");
                Chestpiece = (Armor)item;
                changed = true;
                break;
            case Equipment.EquipType.Legpiece:
                dequipt("LegPieceSlot");
                LegPiece = (Armor)item;
                changed = true;
                break;
            case Equipment.EquipType.Boots:
                dequipt("BootsSlot");
                Boots = (Armor)item;
                changed = true;
                break;
            case Equipment.EquipType.Active:
                dequipt("ActiveSlot");
                Active = (Active)item;
                changed = true;
                break;
        }
        
        
        if (changed)
        {
            itemList.Remove(item);
            unit.UpdateStats();
            //inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().UpdateInventory();
        }
            return changed;
            }

    public bool meetsRequirements(ItemScript item)
    {
        if (((Weapon)item))
        {
            return ((Weapon)item).requiredHands <= numHands;
        }
        return true;
    }
}
