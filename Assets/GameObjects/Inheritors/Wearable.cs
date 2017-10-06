using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wearable : ItemTag {
    public enum ItemType { none, Helm, ChestPiece, Legpiece, Glove, Boots, Active };
    public ItemType wearType;
    public int physicalDef;

public Wearable()
{
    requireTags = new ItemTag[] { new Equipable() };
}
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
