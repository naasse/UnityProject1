using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public GameObject inventoryTemplate;
    private GameObject inventory = null;
    private bool inventoryOpen = false;

    public List<ItemScript> itemList;

    public ItemScript Helm;
    public ItemScript Sword;
    public bool hasChanged = false;
    // Use this for initialization
    void Start() {
        inventory = Instantiate(inventoryTemplate) as GameObject;
        itemList = new List<ItemScript>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                CloseInventory();

            }
            else OpenInventory();
        }
    }
    void OpenInventory()
    {
        inventory.SetActive(true);
        inventoryOpen = true;
        if(hasChanged) inventory.transform.GetChild(0).GetComponent<Inventory>().UpdateInventory(this);
    }
    void CloseInventory()
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

    }
    public ItemScript drop(Vector3 location, ItemScript item)
    {
        ItemScript temp= itemList.Find(item.Equals);
       itemList.Remove(item);
        temp.transform.position = location;
        temp.gameObject.SetActive(false);
        return temp;

    }
    public void equipt(string slotname, ItemScript item)
    {
        if (slotname.Equals("Helm"))
        {
            Helm = item;
            itemList.Remove(item);
        }
        else if (slotname.Equals("Sword"))
        {
            Sword = item;
            itemList.Remove(item);
        }
    }
}
