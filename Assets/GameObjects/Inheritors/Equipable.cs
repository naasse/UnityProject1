using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : ItemTag {
    // Use this for initialization
    public enum EquipType { none,Weapon, Helm, ChestPiece, Legpiece, Glove, Boots, Active };
    public EquipType type;
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
