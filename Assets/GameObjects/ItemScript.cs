using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemScript : MonoBehaviour {

    public enum ItemType {NonEquipable, OneHandedWeapon, TwoHandedWeapon, Helm, ChestPiece, Legpiece, Glove, Boots, Active} ;
    public String name;
    public ItemType EquiptType;
    public bool pickupable;
    public bool isPickedUp;
    public float health;
    public Sprite sprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void pickedUp()
    {
        isPickedUp = true;
        gameObject.SetActive(false);
    }
    public void dropped(Vector3 newPos)
    {
        isPickedUp = false;
        gameObject.transform.position = newPos;
        gameObject.SetActive(true);
    }
}
