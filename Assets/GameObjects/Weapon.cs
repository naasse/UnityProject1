using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment {
    public bool isRanged;
    public float range;
    public float damage;

    public int requiredHands;

    public bool hasAmmo;
    public float maxAmmo;
    public float curAmmo;
	// Use this for initialization
	void Start () {
        type = EquipType.Weapon;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
