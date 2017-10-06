using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : MonoBehaviour {
    public GameObject inventoryTemplate;
    private GameObject inventory = null;
    public bool inventoryOpen = false;

    public List<ItemScript> itemList;

    public ItemScript Helm = null;
    public ItemScript Weapon1 = null;
    public ItemScript Weapon2 = null;
    public ItemScript Chestpiece = null;
    public ItemScript LegPiece = null;
    public ItemScript Boots = null;
    public ItemScript Gloves = null;
    public ItemScript Active = null;

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
        if (hasChanged) inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().UpdateInventory();
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
        //inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().UpdateInventory();
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

    private void dequipt(string slot)
    {
            switch (slot)
            {
                case "Weapon1":
                    if(Weapon1!=null) itemList.Add(Weapon1);
                    Weapon1 = null;
                    break;
                case "Weapon2":
                if (Weapon2 != null) itemList.Add(Weapon2);
                    Weapon2 = null;
                    break;
                case "Helm":

                if (Helm != null) itemList.Add(Helm);
                    Helm = null;
                    break;
                case "ChestPiece":
                if (Chestpiece != null) itemList.Add(Chestpiece);
                    Chestpiece = null;
                    break;
                case "LegPiece":
                if (LegPiece != null) itemList.Add(LegPiece);
                    LegPiece = null;
                    break;
                case "Boots":
                if (Boots != null) itemList.Add(Boots);
                Boots = null;
                    break;
                case "Gloves":
                if (Gloves != null) itemList.Add(Gloves);
                    Gloves = null;
                    break;
                case "Active":
                if (Active != null) itemList.Add(Active);
                    Active = null;
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
        bool isArmor = item.hasTag(new Wearable());
        bool isWeapon = item.hasTag(new Weaponizable());
        switch (((Equipable)item.tags[ItemTag.TagTypes.Equipable]).type)
        {
            case Equipable.EquipType.Weapon:
                Weaponizable newItemTag = ((Weaponizable)item.tags[ItemTag.TagTypes.Weaponizable]);
                if (newItemTag.requiredHands == 1)
                {
                    if (slotname.Equals("Weapon1Slot"))
                    {
                        dequipt("Weapon1");
                        Weapon1 = item;
                        changed = true;
                    }
                    else  if (slotname.Equals("Weapon2Slot"))
                    {
                        dequipt("Weapon2");
                        Weapon2 = item;
                        changed = true;
                    }
                }
                else if (newItemTag.requiredHands == 2)
                {
                    dequipt("Weapon1");
                    dequipt("Weapon2");
                    Weapon1 = item;
                    changed = true;
                }
                break;
            case Equipable.EquipType.Helm:
                dequipt("Helm");
                Helm = item;
                changed = true;
                break;
            case Equipable.EquipType.Glove:
                dequipt("Gloves");
                Gloves = item;
                changed = true;
                break;
            case Equipable.EquipType.ChestPiece:
                dequipt("ChestPiece");
                Chestpiece = item;
                changed = true;
                break;
            case Equipable.EquipType.Legpiece:
                dequipt("LegPiece");
                LegPiece = item;
                changed = true;
                break;
            case Equipable.EquipType.Boots:
                dequipt("Boots");
                Boots = item;
                changed = true;
                break;
            case Equipable.EquipType.Active:
                dequipt("Active");
                Active = item;
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
        if (item.hasTag(new Weaponizable()))
        {
            return ((Weaponizable)item.tags[ItemTag.TagTypes.Weaponizable]).requiredHands <= numHands;
        }
        return true;
    }
}
