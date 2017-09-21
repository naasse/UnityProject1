using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public ItemScript Helm;
    public ItemScript Sword;
    public ItemScript[] items= new ItemScript[numItemSlots];
    public Image[] itemImages = new Image[numItemSlots];
    public const int numItemSlots= 4;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pickup(ItemScript newItem)
    {


        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                itemImages[i].sprite = newItem.sprite;
                itemImages[i].enabled = true;
                newItem.pickedUp();
                return;
            }
        }
    }

    public ItemScript drop(ItemScript removeItem,Vector3 dropPos)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Equals(removeItem))
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                removeItem.dropped(dropPos);
                return removeItem;

            }
        }
        return null;
    }
}
