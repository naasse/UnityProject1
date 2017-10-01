using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : MonoBehaviour {
    public GameObject inventoryTemplate;
    private GameObject inventory = null;
    public bool inventoryOpen = false;

    public List<ItemScript> itemList;

    public ItemScript Helm;
    public ItemScript Weapon1;
    public ItemScript Weapon2;
    public ItemScript Chestpiece;
    public ItemScript LegPiece;
    public ItemScript Boots;
    public ItemScript Gloves;
    public ItemScript Active;

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
        inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().UpdateInventory();
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
        inventory.transform.Find("Inventory").GetComponent<InventoryGuiScript>().UpdateInventory();
        return temp;

    }

    private void dequipt(ItemScript item)
    {
        if (item != null)
        {
            itemList.Add(item);
        }
    }
    public void setUnit(UnitScript unit)
    {
        this.unit = unit;
    }

    public void equipt(ItemScript item, string slot)
    {
        bool changed = false;
        switch(slot)
        {
            case "Weapon1":
                if (item.EquiptType.Equals(ItemScript.ItemType.OneHandedWeapon))
                {
                    ItemScript temp = Weapon1;
                    Weapon1 = item;
                    dequipt(temp);
                    changed = true;
                }
                else if (item.EquiptType.Equals(ItemScript.ItemType.TwoHandedWeapon))
                {
                    dequipt(Weapon1);
                    dequipt(Weapon2);
                    Weapon1 = item;
                    changed = true;
                }
                break;
            case "Weapon2":
                if (item.EquiptType.Equals(ItemScript.ItemType.OneHandedWeapon))
                {
                    ItemScript temp = Weapon2;
                    Weapon2 = item;
                    dequipt(temp);
                    changed = true;
                }
                else if (item.EquiptType.Equals(ItemScript.ItemType.TwoHandedWeapon))
                {
                    dequipt(Weapon1);
                    dequipt(Weapon2);
                    Weapon1 = item;
                    changed = true;
                }
                break;
            case "Helm":
                if (item.EquiptType.Equals(ItemScript.ItemType.Helm))
                {
                    dequipt(Helm);
                    Helm = item;
                    changed = true;
                
                }
                break;
            case "ChestPiece":
                if (item.EquiptType.Equals(ItemScript.ItemType.ChestPiece))
                {
                    dequipt(Chestpiece);
                    Chestpiece = item;
                    changed = true;
                }
                break;
            case "LegPiece":
                if (item.EquiptType.Equals(ItemScript.ItemType.Legpiece))
                {
                    dequipt(LegPiece);
                    LegPiece = item;
                    changed = true;
                }
                break;
            case "Boots":
                if (item.EquiptType.Equals(ItemScript.ItemType.Boots))
                {
                    dequipt(Boots);
                    Boots = item;
                    changed = true;
                }
                break;
            case "Gloves":
                if (item.EquiptType.Equals(ItemScript.ItemType.Glove))
                {
                    dequipt(Gloves);
                    Gloves = item;
                    changed = true;
                }
                break;
            case "Active":
                if (item.EquiptType.Equals(ItemScript.ItemType.Active))
                {
                    dequipt(Active);
                    Active = item;
                    changed = true;
                }
                break;

        }
        if (changed) unit.UpdateStats();
            }

}
